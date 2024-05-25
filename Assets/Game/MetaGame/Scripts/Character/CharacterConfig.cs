using Core.Config;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Config
{
    public class CharacterConfig : BaseMultiConfig<CharacterConfigData, CharacterConfig>
    {

    }

    public class CharacterConfigData : IConfigData
    {
        public string ID => characterID;
        public string characterID;
        public Vector3 spawnPosition;
        public Dictionary<string, GameObject> characterPrefabMap = new Dictionary<string, GameObject>();
    }
}