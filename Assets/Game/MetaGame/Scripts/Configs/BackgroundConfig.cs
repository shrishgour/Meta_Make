using UnityEngine;

namespace Core.Config
{
	public class BackgroundConfig : BaseMultiConfig<BackgroundConfigData, BackgroundConfig>
	{

	}

	[System.Serializable]
	public class BackgroundConfigData : IConfigData
	{
		public string ID => backgroundID;
		public string backgroundID;
		public Sprite backgroundSprite;
	}
}