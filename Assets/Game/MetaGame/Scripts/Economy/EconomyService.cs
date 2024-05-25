using Core.Services;

namespace Game.Economy
{
    public class EconomyService : BaseService
    {
        private UserState userState;
        private EconomyManager manager;

        public override void Initialize()
        {
            base.Initialize();
            manager = new EconomyManager();
        }

        public void AddCurrency(Currency currency)
        {
            manager.AddCurrency(currency);
        }

        public bool SubtractCurrency(string key, int amount)
        {
            return manager.SubtractCurrency(key, amount);
        }

        public int GetCurrencyAmount(string key)
        {
            return manager.GetCurrencyAmount(key);
        }
    }
}