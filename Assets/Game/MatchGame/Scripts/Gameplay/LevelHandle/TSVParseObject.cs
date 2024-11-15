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

namespace POPBlocks.Scripts.Gameplay.LevelHandle
{
    public class TSVParseObject
    {
        public int id;
        public int score;
        public int stars;
        public int level;
        public int colors;
        public int moves;
        public bool fail;
        public int winChance;

        public void JsonLevelSettingsToInt(JsonLevelSettings levelSettings)
        {
            level = Int32.Parse(levelSettings.level);
            colors = Int32.Parse(levelSettings.colors);
            moves = Int32.Parse(levelSettings.moves);
        }
    }
}
