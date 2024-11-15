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

using UnityEngine;

namespace POPBlocks.Scripts.Popups
{
    public class AnyPopupActivation : MonoBehaviour
    {
        public GameObject panel;
        public PopupManager popupManager;
        private void OnEnable()
        {
            Popup.OnShowPopup += OnShowPopup;
            Popup.OnHidePopup += OnHidePopup;
        }

        private void OnHidePopup(string popupname)
        {
            if (!popupManager.IsAnyPopupOpen()) panel.SetActive(true);
        }

        private void OnDisable()
        {
            Popup.OnShowPopup -= OnShowPopup;
            Popup.OnHidePopup -= OnHidePopup;
        }

        private void OnShowPopup(string popupname)
        {
            panel.SetActive(false);
        }
    }
}
