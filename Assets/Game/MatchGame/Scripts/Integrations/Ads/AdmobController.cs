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
#if ADMOB
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
#endif
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace POPBlocks.Scripts.Integrations.Ads
{
    public class AdmobController : MonoBehaviour
    {
        public AdItem adsSettingsAd;
        #if ADMOB
        private BannerView bannerView;
        public InterstitialAd interstitialAd;
        private RewardedAd rewardedAd;
        private RewardedInterstitialAd rewardedInterstitialAd;

        public UnityEvent OnAdLoadedEvent;
        public UnityEvent OnAdFailedToLoadEvent;
        public UnityEvent OnAdOpeningEvent;
        public UnityEvent OnAdFailedToShowEvent;
        public UnityEvent OnUserEarnedRewardEvent = new UnityEvent();
        public UnityEvent OnAdClosedEvent;
        public UnityEvent OnAdLeavingApplicationEvent;

        #region UNITY MONOBEHAVIOR METHODS

        public void Start()
        {
            MobileAds.SetiOSAppPauseOnBackground(true);

            List<String> deviceIds = new List<String>() { AdRequest.TestDeviceSimulator };

            // Add some test device IDs (replace with your own device IDs).
#if UNITY_IPHONE
        deviceIds.Add("96e23e80653bb28980d3f40beb58915c");
#elif UNITY_ANDROID
            deviceIds.Add("75EF8D155528C04DACBBA6F36F433035");
            deviceIds.Add("0A9574CB5EECDBBEA2098B3C398DBAAE");
#endif

            // Configure TagForChildDirectedTreatment and test device IDs.
            RequestConfiguration requestConfiguration =
                new RequestConfiguration.Builder()
                    .SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.Unspecified)
                    .SetTestDeviceIds(deviceIds).build();

            MobileAds.SetRequestConfiguration(requestConfiguration);

            // Initialize the Google Mobile Ads SDK.
            MobileAds.Initialize(HandleInitCompleteAction);
        }

        private void HandleInitCompleteAction(InitializationStatus initstatus)
        {
            // Callbacks from GoogleMobileAds are not guaranteed to be called on
            // main thread.
            // In this example we use MobileAdsEventExecutor to schedule these calls on
            // the next Update() loop.
            // MobileAdsEventExecutor.ExecuteInUpdate(() => {
                // RequestBannerAd();
            // });
        }

        #endregion

        #region HELPER METHODS

        private AdRequest CreateAdRequest()
        {
            return new AdRequest.Builder()
                .AddTestDevice(AdRequest.TestDeviceSimulator)
                .AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
                .AddKeyword("unity-admob-sample")
                .TagForChildDirectedTreatment(false)
                .AddExtra("color_bg", "9B30FF")
                .Build();
        }

        #endregion

        #region BANNER ADS

        public void RequestBannerAd()
        {
            // Clean up banner before reusing
            if (bannerView != null)
            {
                bannerView.Destroy();
            }

            // Create a 320x50 banner at top of the screen
            bannerView = new BannerView(adsSettingsAd.adsId.First(i=>i.type==AdType.Banner).id, AdSize.SmartBanner, AdPosition.Top);

            // Add Event Handlers
            bannerView.OnAdLoaded += (sender, args) => OnAdLoadedEvent?.Invoke();
            bannerView.OnAdFailedToLoad += (sender, args) => OnAdFailedToLoadEvent?.Invoke();
            bannerView.OnAdOpening += (sender, args) => OnAdOpeningEvent?.Invoke();
            bannerView.OnAdClosed += (sender, args) => OnAdClosedEvent?.Invoke();
            bannerView.OnAdLeavingApplication += (sender, args) => OnAdLeavingApplicationEvent?.Invoke();

            // Load a banner ad
            bannerView.LoadAd(CreateAdRequest());
        }

        public void DestroyBannerAd()
        {
            if (bannerView != null)
            {
                bannerView.Destroy();
            }
        }

        #endregion

        #region INTERSTITIAL ADS

        public void RequestAndLoadInterstitialAd()
        {

            // Clean up interstitial before using it
            if (interstitialAd != null)
            {
                interstitialAd.Destroy();
            }

            interstitialAd = new InterstitialAd(adsSettingsAd.adsId.First(i=>i.type==AdType.Interstitial).id);

            // Add Event Handlers
            interstitialAd.OnAdLoaded += (sender, args) => OnAdLoadedEvent?.Invoke();
            interstitialAd.OnAdFailedToLoad += (sender, args) => OnAdFailedToLoadEvent?.Invoke();
            interstitialAd.OnAdOpening += (sender, args) => OnAdOpeningEvent?.Invoke();
            interstitialAd.OnAdClosed += (sender, args) => OnAdClosedEvent?.Invoke();
            interstitialAd.OnAdLeavingApplication += (sender, args) => OnAdLeavingApplicationEvent?.Invoke();

            // Load an interstitial ad
            interstitialAd.LoadAd(CreateAdRequest());
        }

        public void ShowInterstitialAd()
        {
            if (interstitialAd.IsLoaded())
            {
                interstitialAd.Show();
            }
            else
            {
            }
        }

        public void DestroyInterstitialAd()
        {
            if (interstitialAd != null)
            {
                interstitialAd.Destroy();
            }
        }
        #endregion

        #region REWARDED ADS

        public void RequestAndLoadRewardedAd()
        {
            Debug.Log("request rewarded ads");
            // create new rewarded ad instance
            rewardedAd = new RewardedAd(adsSettingsAd.adsId.First(i=>i.type==AdType.RewardedAd).id);

            // Add Event Handlers
            rewardedAd.OnAdLoaded += (sender, args) => OnAdLoadedEvent?.Invoke();
            rewardedAd.OnAdFailedToLoad += (sender, args) => OnAdFailedToLoadEvent?.Invoke();
            rewardedAd.OnAdOpening += (sender, args) => OnAdOpeningEvent?.Invoke();
            rewardedAd.OnAdFailedToShow += (sender, args) => OnAdFailedToShowEvent?.Invoke();
            rewardedAd.OnAdClosed += (sender, args) => OnAdClosedEvent?.Invoke();
            rewardedAd.OnUserEarnedReward += (sender, args) => OnUserEarnedRewardEvent?.Invoke();

            // Create empty ad request
            rewardedAd.LoadAd(CreateAdRequest());
        }

        public void ShowRewardedAd()
        {
            if (rewardedAd != null)
            {
                rewardedAd.Show();
            }
            else
            {
            }
        }

        public void RequestAndLoadRewardedInterstitialAd()
        {


            // Create an interstitial.
            RewardedInterstitialAd.LoadAd(adsSettingsAd.adsId.First(i=>i.type==AdType.RewardedAd).id, CreateAdRequest(), (rewardedInterstitialAd, error) =>
            {

                if (error != null)
                {
                    MobileAdsEventExecutor.ExecuteInUpdate(() => {
                    });
                    return;
                }

                this.rewardedInterstitialAd = rewardedInterstitialAd;
                MobileAdsEventExecutor.ExecuteInUpdate(() => {
                });
                // Register for ad events.
                this.rewardedInterstitialAd.OnAdDidPresentFullScreenContent += (sender, args) =>
                {
                    MobileAdsEventExecutor.ExecuteInUpdate(() => {
                    });
                };
                this.rewardedInterstitialAd.OnAdDidDismissFullScreenContent += (sender, args) =>
                {
                    MobileAdsEventExecutor.ExecuteInUpdate(() => {
                    });
                    this.rewardedInterstitialAd = null;
                };
                this.rewardedInterstitialAd.OnAdFailedToPresentFullScreenContent += (sender, args) =>
                {
                    MobileAdsEventExecutor.ExecuteInUpdate(() => {
                    });
                    this.rewardedInterstitialAd = null;
                };
            });
        }

        public void ShowRewardedInterstitialAd()
        {
            if (rewardedInterstitialAd != null)
            {
                rewardedInterstitialAd.Show((reward) => {
                    MobileAdsEventExecutor.ExecuteInUpdate(() => {
                    });
                });
            }
            else
            {
            }
        }

        #endregion
        #endif
    }
}
