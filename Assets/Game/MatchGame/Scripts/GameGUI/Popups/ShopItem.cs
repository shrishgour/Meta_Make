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

using POPBlocks.Scripts.GameGUI;
using POPBlocks.Scripts.GUI;
#if UNITY_INAPP
using POPBlocks.Scripts.Integrations;
#endif
using UnityEngine;

namespace POPBlocks.Scripts.Popups
{
    public class ShopItem : MonoBehaviour
    {
        public string productID;
        public GUICounter coinsCounter;


        public void Buy()
        {
#if UNITY_INAPP
            UnityInAppsIntegration.THIS.BuyProductID(productID);
            SoundBase.Instance.PlayOneShot(SoundBase.Instance.cash);
#endif
        }
    }
}
