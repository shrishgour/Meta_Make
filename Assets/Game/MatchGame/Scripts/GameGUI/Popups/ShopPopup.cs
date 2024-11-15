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

using System.Linq;
#if UNITY_INAPP
using POPBlocks.Scripts.Integrations;
using UnityEngine.Purchasing;
#endif
using POPBlocks.Scripts.Scriptables;
using UnityEngine;

namespace POPBlocks.Scripts.Popups
{
    public class ShopPopup : Popup
    {
        public ShopItem[] shopItems;
        private GameSettings gameSettings;
        private ShopSettings shopSettings;

        private void Start()
        {
            gameSettings = Resources.Load<GameSettings>("Settings/GameSettings");

            shopSettings = Resources.Load<ShopSettings>("Settings/ShopSettings");
            var list = shopSettings.shopItems.OrderByDescending(i => i.coins).ToArray();
            for (var i = 0; i < list.Count(); i++)
            {
                var shopSetting = list[i];
                shopItems[i].productID = shopSettings.shopItems.OrderByDescending(shopItemEditor => shopItemEditor.coins).ToArray()[i].productID;
                shopItems[i].coinsCounter.SetValue(shopSetting.coins);
            }
        }

        private void OnEnable()
        {
            #if UNITY_INAPP
            UnityInAppsIntegration.OnPurchaseSucceed += OnPurchased;
            #endif
        }

        private void OnDisable()
        {
#if UNITY_INAPP

            UnityInAppsIntegration.OnPurchaseSucceed -= OnPurchased;
#endif

        }

#if UNITY_INAPP
        void OnPurchased(PurchaseEventArgs args)
        {
            GameManager.Instance.coins.IncrementValue(shopSettings.shopItems.First(i => i.productID == args.purchasedProduct.definition.id).coins);
            Hide();
        }
#endif

        public void OnRewarded()
        {
            GameManager.Instance.coins.IncrementValue(gameSettings.coinsReward);
            var instanceRewardPopup = (RewardPopup)PopupManager.Instance.rewardPopup.Show();
            instanceRewardPopup.SetReward(RewardTypes.Coins);
            Hide();
        }
    }
}
