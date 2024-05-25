using Core.UI;
using Game.Attribute;
using System.Collections.Generic;

namespace Game.UI
{
    [System.Serializable]
    public class MainHudState : BaseHudState
    {
        [StringListDropdown("Game.UI.MainHudStateList")]
        public string stateName;
        public List<HudElement> hudElements;

        public override string StateName => stateName.ToString();
        public override List<HudElement> HudElements => hudElements;
    }

    [System.Serializable]
    public class WardrobeHudState : BaseHudState
    {
        [StringListDropdown("Game.UI.WardrobeHudStateList")]
        public string stateName;
        public List<HudElement> hudElements;

        public override string StateName => stateName.ToString();
        public override List<HudElement> HudElements => hudElements;
    }
}