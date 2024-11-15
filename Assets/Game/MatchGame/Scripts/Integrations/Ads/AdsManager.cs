 // This asset was uploaded by https://unityassetcollection.com

// // ©2015 - 2021 Candy Smith
// // All rights reserved
// // Redistribution of this software is strictly not allowed.
// // Copy of this software can be obtained from unity asset store only.
// 
// // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// // IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// // FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// // AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// // LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// // OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// // THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace POPBlocks.Scripts.Integrations.Ads
{
    public class AdsManager : MonoBehaviour
    {
        private AdsSettings adsSettings;
        public List<AdEvents> adsEvents = new List<AdEvents>();
        public static AdsManager THIS;
        private static AdsHandler rewardedAdsManager;

        private void Awake()
        {
            if (THIS == null) THIS = this;
            else if (THIS != this)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(this);
            adsSettings = Resources.Load<AdsSettings>("Settings/AdsSettings");
            var adsSettingsAd = adsSettings.ads.First(i=>i.enable);
            AdsHandler adsHandler = null;
            var adNetworkNetwork = adsSettingsAd.adNetwork.network;
            if (adNetworkNetwork == AdNetworks.UnityAds)
            {
                adsHandler = null;
                #if UNITY_ADS
                    adsHandler = new UnityAdsHandler(GetPlatformID(adsSettingsAd));
                #endif
            }
            else if (adNetworkNetwork == AdNetworks.Admob)
            {
                var admobController = gameObject.AddComponent<AdmobController>();
                admobController.adsSettingsAd = adsSettingsAd;
                adsHandler = new AdmobAdsHandler(GetPlatformID(adsSettingsAd), admobController);
            }
            if (adsSettingsAd.showRewardedAds)
            {
                rewardedAdsManager = adsHandler;
                if (rewardedAdsManager != null) rewardedAdsManager.OnRewarded += OnRewardedShown;
            }

            foreach (var adTrigger in adsSettingsAd.adTriggers)
            {
                var adEvents = new AdEvents {gameEvent = adTrigger.trigger, adType = adTrigger.type, everyLevel = adTrigger.frequency};
                adEvents.adsHandler = adsHandler;
                adsEvents.Add(adEvents);
            }
        }

        private void OnEnable()
        {
            LevelManager.OnGamestateChanged += CheckAdsEvents;
        }

        
        private void OnDisable()
        {
            LevelManager.OnGamestateChanged -= CheckAdsEvents;
        }

        private string GetPlatformID(AdItem adsSettingsAd)
        {
#if UNITY_EDITOR

            
            switch (EditorUserBuildSettings.activeBuildTarget)
            {
                case BuildTarget.Android:
                    return adsSettingsAd.adsId.First(i => i.platform == Platforms.Android && i.type == AdType.Interstitial).id;
                case BuildTarget.iOS:
                    return adsSettingsAd.adsId.First(i => i.platform == Platforms.iOS && i.type == AdType.Interstitial).id;

            }
#else
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                     return adsSettingsAd.adsId.First(i => i.platform == Platforms.Android && i.type == AdType.Interstitial).id;
                case RuntimePlatform.IPhonePlayer:
                    return adsSettingsAd.adsId.First(i => i.platform == Platforms.iOS && i.type == AdType.Interstitial).id;


            }

#endif
            return String.Empty;
        }

        private void OnRewardedShown()
        {
            OnRewardedShownEvent?.Invoke();
        }

        public void CheckAdsEvents(GameState state)
        {
            // Debug.Log("Check ads " + state);
            foreach (var item in adsEvents)
            {
                if (item.gameEvent == state )
                {
                    Debug.Log("ads state " + item.gameEvent);
                    item.calls++;
                    if (item.adsHandler != null && item.calls % item.everyLevel == 0 && item.adsHandler.IsAvailable())
                        item.adsHandler.ShowAds();
                }
            }
        }

        public void ShowInterstitial()
        {
            throw new NotImplementedException();
        }

        public delegate void RewEvent();

        public static event RewEvent OnRewardedShownEvent;

        public void ShowRewarded()
        {
            rewardedAdsManager.ShowRewardedAds();
        }

        public void OnRewardedAdRewarded()
        {
        }

        public bool IsRewardedAvailable()
        {
            return false;
        }
    }

    /// <summary>
    /// Ad event
    /// </summary>
    [Serializable]
    public class AdEvents
    {
        public GameState gameEvent;
        public AdType adType;
        public int everyLevel;
        public int calls;
        public AdsHandler adsHandler;
    }
}
