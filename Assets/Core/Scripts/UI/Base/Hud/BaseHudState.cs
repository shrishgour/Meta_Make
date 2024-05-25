using System.Collections.Generic;

namespace Core.UI
{
    public interface IHudState
    {
        public string StateName { get; }
        public List<HudElement> HudElements { get; }
    }

    [System.Serializable]
    public abstract class BaseHudState : IHudState
    {
        public abstract string StateName { get; }
        public abstract List<HudElement> HudElements { get; }
    }
}
