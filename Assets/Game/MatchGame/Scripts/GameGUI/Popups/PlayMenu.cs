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
using System.Linq;
using POPBlocks.Scripts.Boosts;
#if EPSILON
using POPBlocks.Server.Network.Leadboard;
#endif
using UnityEngine;

namespace POPBlocks.Scripts.Popups
{
    public class PlayMenu : Popup
    {
        #if EPSILON
        public LeadboardManager LeadboardManager;
        #endif

        protected override void AfterShowAnimation()
        {
            ShowLeadboard();
            base.AfterShowAnimation();
        }

        public void StartLevel()
        {
            var boostButtons = FindObjectsOfType<BoostButton>().Where(i => i.selected);
            foreach (var boostButton in boostButtons)
            {
                boostButton.Count--;
                for (int i = 0; i < boostButton.parameters.countItems; i++)
                {
                    GameManager.Instance.boostTypes.Add(boostButton.boostType);
                }
            }
            OpenScene("game");
        }

        void ShowLeadboard()
        {
            #if EPSILON
            LeadboardManager.level = GameManager.Instance._mapProgressManager.CurrentLevel;
            LeadboardManager.gameObject.SetActive(true);
            #endif
        }
    }
}
