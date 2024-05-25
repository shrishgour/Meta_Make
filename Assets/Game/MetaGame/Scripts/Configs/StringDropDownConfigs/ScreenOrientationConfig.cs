using Core.Config;
using UnityEngine;

namespace Game.Config
{
	[CreateAssetMenu(fileName = "ScreenOrientationConfig", menuName = "Configs/Attributes/ScreenOrientationConfig", order = 100)]
	public class ScreenOrientationConfig : BaseMultiConfig<ScreenOrientationConfigData, ScreenOrientationConfig>, NonConfig
	{

	}

	[System.Serializable]
	public class ScreenOrientationConfigData : IConfigData
	{
		public string ID => screenOrient;
		public string screenOrient;
	}
}