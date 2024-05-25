using Core.Config;
using Core.Events;
using Core.Services;
using Core.UI;
using Game.Events;
using Game.UI;
using UnityEngine;
using UserState = Core.Services.UserState;

namespace Game.Economy
{
    public class EconomyManager
    {
        public void AddCurrency(Currency currency)
        {
            var userState = ServiceRegistry.Get<UserState>();
            var economyState = userState.EconomyState;
            var currencyKey = currency.currencyType.ToString();
            var amount = currency.value;

            if (!economyState.currencyMap.TryAdd(currencyKey, amount))
            {
                economyState.currencyMap[currencyKey] += amount;

                Debug.Log($"{currencyKey} = {economyState.currencyMap[currencyKey]}");
            }
            else
            {
                economyState.currencyMap[currencyKey] = amount;
            }

            ExecuteMetaData(currency.currencyType, currency.metaData);
            userState.EconomyState = economyState;
            EventManager.instance.TriggerEvent(new UpdateCurrencyEvent(currencyKey));
        }

        public bool SubtractCurrency(string currencyKey, int amount)
        {
            var result = false;
            var userState = ServiceRegistry.Get<UserState>();
            var economyState = userState.EconomyState;

            if (economyState.currencyMap.ContainsKey(currencyKey) && economyState.currencyMap[currencyKey] >= amount)
            {
                result = true;
                economyState.currencyMap[currencyKey] -= amount;
                userState.EconomyState = economyState;
                EventManager.instance.TriggerEvent<UpdateCurrencyEvent>(new UpdateCurrencyEvent(currencyKey));
                Debug.Log($"{currencyKey} = {economyState.currencyMap[currencyKey]}");
            }

            return result;
        }

        public int GetCurrencyAmount(string currencyKey)
        {
            return ServiceRegistry.Get<UserState>().EconomyState.currencyMap[currencyKey];
        }

        private void ExecuteMetaData(CurrencyType currencyType, string metaData)
        {
            switch (currencyType)
            {
                case CurrencyType.Coins:
                    break;
                case CurrencyType.Gems:
                    break;
                case CurrencyType.Life:
                    break;
                case CurrencyType.Star:
                    break;
                case CurrencyType.CharCustomization:
                    UiHudManager.Instance.OpenHud(HudList.WardrobeHud, WardrobeHudStateList.VariantOnly);
                    EventManager.instance.TriggerEvent(new GroupSelectionEvent(metaData, true));
                    break;
            }
        }
    }

    [System.Serializable]
    public class Currency
    {
        public CurrencyType currencyType;
        public int value;
        public string metaData;

        public Currency(CurrencyType currencyType, int value, string metaData)
        {
            this.currencyType = currencyType;
            this.value = value;
            this.metaData = metaData;
        }
    }

    public enum CurrencyType
    {
        Coins,
        Gems,
        Life,
        Star,
        CharCustomization
    }
}