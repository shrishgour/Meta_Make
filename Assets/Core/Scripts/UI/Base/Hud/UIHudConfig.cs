using Game.Attribute;
using UnityEngine;

namespace Core.Config
{
    public class UIHudConfig : BaseMultiConfig<UIHudConfigData, UIHudConfig>
    {
    }

    [System.Serializable]
    public class UIHudConfigData : IConfigData
    {
        public string ID => hudName;
        [StringListDropdown("Core.Config.HudList")]
        public string hudName;
        public GameObject hudPrefab;
    }

    public class HudList
    {
        public static readonly string MainHud = "MainHud";
        public static readonly string WardrobeHud = "WardrobeHud";
    }
}