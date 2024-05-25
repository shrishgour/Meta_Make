using Sirenix.OdinInspector;
using System;

namespace Core.Config
{
    public class BaseSingleConfig<T, U> : SerializedScriptableObject, IConfig where T : IConfigData, new() where U : IConfig
    {
        public Type ConfigType => typeof(U);

        public T data;
    }
}

