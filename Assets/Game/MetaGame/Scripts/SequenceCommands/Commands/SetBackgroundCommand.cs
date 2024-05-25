using Core.Services;
using Game.Services;
using UnityEngine;

namespace Core.Sequencer.Commands
{
	[System.Serializable]
	public class SetBackgroundCommand : Command
	{
		public string backgroundID;
		public override bool IsCommandDone { get; protected set; }
		public override bool IsSkippable { get; protected set; }

		public override void Start()
		{
			ServiceRegistry.Get<BackgroundService>().SetBackground(backgroundID, () => IsCommandDone = true);
		}

		protected override void OnSkip()
		{

		}
	}
}