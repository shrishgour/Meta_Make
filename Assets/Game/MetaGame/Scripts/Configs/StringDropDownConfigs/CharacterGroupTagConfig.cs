using Core.Config;
using UnityEngine;

namespace Game.Config
{
    [CreateAssetMenu(fileName = "CharacterGroupTagConfig", menuName = "Configs/Attributes/CharacterGroupTagConfig", order = 100)]
    public class CharacterGroupTagConfig : BaseMultiConfig<CharacterGroupTagConfigData, CharacterGroupTagConfig>, NonConfig
    {

    }

    [System.Serializable]
    public class CharacterGroupTagConfigData : IConfigData
    {
        public string ID => groupTagID;
        public string groupTagID;
    }
}