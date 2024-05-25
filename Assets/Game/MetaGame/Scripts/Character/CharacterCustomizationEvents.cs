using Core.Events;

namespace Game.Events
{
    public class GroupSelectionEvent : GameEvent
    {
        public string groupID;
        public bool isRewarded;
        public GroupSelectionEvent(string groupID, bool isRewarded = false)
        {
            this.groupID = groupID;
            this.isRewarded = isRewarded;
        }
    }

    public class VariantSelectionEvent : GameEvent
    {
        public string variantID;

        public VariantSelectionEvent(string variantID)
        {
            this.variantID = variantID;
        }
    }
}
