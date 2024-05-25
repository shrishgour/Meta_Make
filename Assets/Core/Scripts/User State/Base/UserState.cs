using Core.Data;
using Game.Data;
using Newtonsoft.Json;
using UnityEngine;

namespace Core.Services
{
    public class UserState : BaseService, IUser
    {
        public const string ExampleInteger = "example_int";
        public const string CustomState = "custom_State";

        public int ExampleInt
        {
            get { return GetValue(ExampleInteger, 0); }
            set { SetValue<int>(ExampleInteger, value); }
        }

        public ProgressionUserState ProgressionState
        {
            get { return GetValue(ProgressionUserState.key, new ProgressionUserState()); }
            set { SetValue<int>(ProgressionUserState.key, value); }
        }

        public EconomyUserState EconomyState
        {
            get { return GetValue(EconomyUserState.key, new EconomyUserState()); }
            set { SetValue<int>(EconomyUserState.key, value); }
        }

        public CharacterUserState CharacterState
        {
            get { return GetValue(CharacterUserState.key, new CharacterUserState()); }
            set { SetValue<int>(CharacterUserState.key, value); }
        }

        private T GetValue<T>(string key, T defaultValue)
        {
            return PlayerPrefs.HasKey(key) ? JsonConvert.DeserializeObject<T>(PlayerPrefs.GetString(key)) : defaultValue;
        }

        private void SetValue<T>(string key, object value)
        {
            string data = JsonConvert.SerializeObject(value, Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            PlayerPrefs.SetString(key, data);

            //Update Server Database here
        }

        public void ClearUserState()
        {
            //ExampleCustomState.Reset();
            //StateHandler.OnClearAllStates?.Invoke();
        }
    }
}
