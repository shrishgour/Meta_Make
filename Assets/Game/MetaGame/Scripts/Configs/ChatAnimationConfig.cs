using Game.Attribute;
using UnityEngine;

namespace Core.Config
{
    public class ChatAnimationConfig : BaseMultiConfig<ChatAnimationConfigData, ChatAnimationConfig>
    {

    }

    [System.Serializable]
    public class ChatAnimationConfigData : IConfigData
    {
        public string ID => screenOrient;
        [ScreenOrientation]
        public string screenOrient;
        public Vector3 charStartPos;
        public Vector3 charEndPos;
        public Vector3 chatStartPos;
        public Vector3 chatEndPos;
        public float animDuration;
    }
}