using Core.Config;
using UnityEngine;

namespace Game.Config
{
    [CreateAssetMenu(fileName = "CharacterGroupConfig", menuName = "Configs/Attributes/CharacterGroupConfig", order = 100)]
    public class CharacterGroupConfig : BaseMultiConfig<CharacterGroupConfigData, CharacterGroupConfig>, NonConfig
    {

    }

    [System.Serializable]
    public class CharacterGroupConfigData : IConfigData
    {
        public string ID => groupID;
        public string groupID;
    }
}