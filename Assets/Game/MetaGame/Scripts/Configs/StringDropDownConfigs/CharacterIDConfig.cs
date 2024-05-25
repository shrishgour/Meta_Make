using UnityEngine;

namespace Core.Config
{
    [CreateAssetMenu(fileName = "CharacterIDConfig", menuName = "Configs/Attributes/CharacterIDConfig", order = 100)]
    public class CharacterIDConfig : BaseMultiConfig<CharacterIDConfigData, CharacterIDConfig>, NonConfig
    {

    }

    [System.Serializable]
    public class CharacterIDConfigData : IConfigData
    {
        public string ID => characterID;
        public string characterID;
    }
}