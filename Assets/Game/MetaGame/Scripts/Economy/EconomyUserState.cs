using Core.Data;
using System.Collections.Generic;

namespace Game.Data
{
    public class EconomyUserState : StateHandler
    {
        public const string key = nameof(EconomyUserState);

        public override string Key => key;
        public Dictionary<string, int> currencyMap = new Dictionary<string, int>();

        public override void Reset()
        {
            currencyMap.Clear();
            base.Reset();
        }
    }
}