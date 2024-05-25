using Game.Attribute;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Character
{
    [System.Serializable]
    public class CharacterData
    {
        public List<GroupData> groupDataList;
    }

    [System.Serializable]
    public class GroupData
    {
        [CharacterGroup]
        public string groupID;
        [CharacterGroupTag]
        public string groupTag;
        public List<VariantData> variantDataList;
    }

    [System.Serializable]
    public class VariantData
    {
        public string variantID => variant.name;
        public GameObject variant;
        public Sprite icon;
    }
}
