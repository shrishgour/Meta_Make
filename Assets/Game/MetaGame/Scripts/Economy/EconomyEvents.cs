using Core.Events;

namespace Game.Events
{
    public class UpdateCurrencyEvent : GameEvent
    {
        public string currencyType;

        public UpdateCurrencyEvent(string currencyType)
        {
            this.currencyType = currencyType;
        }
    }
}
