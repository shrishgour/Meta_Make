using Core.Config;
using Game.Attribute;
using UnityEngine;

namespace Game.Config
{
    public class CharacterGroupIconConfig : BaseMultiConfig<CharacterGroupIconConfigData, CharacterGroupIconConfig>
    {

    }

    [System.Serializable]
    public class CharacterGroupIconConfigData : IConfigData
    {
        public string ID => groupID;
        [CharacterGroup]
        public string groupID;
        public Sprite icon;
    }
}