using Core.Data;
using System.Collections.Generic;

namespace Game.Data
{
    public class CharacterUserState : StateHandler
    {
        public const string key = nameof(CharacterUserState);

        public override string Key => key;

        public Dictionary<string, Dictionary<string, string>> customizationStateData;

        public CharacterUserState()
        {
            customizationStateData = new Dictionary<string, Dictionary<string, string>>();
        }

        public override void Reset()
        {
            customizationStateData.Clear();
            base.Reset();
        }
    }
}