using Core.UI;
using UnityEngine;

namespace Core.Sequencer.Commands
{
	[System.Serializable]
	public class NarrativeCommand : Command
	{
		public string chatString;
		public override bool IsCommandDone { get; protected set; }
		public override bool IsSkippable { get; protected set; }

		public override void Start()
		{
			UiManager.Instance.OpenDialog<NarrativeDialog>(NarrativeDialog.DialogID, true, (dialog) =>
			{
				dialog.Init(chatString, OnClose);
			});
		}

		protected override void OnSkip()
		{

		}

		private void OnClose()
		{
			UiManager.Instance.CloseDialog(NarrativeDialog.DialogID);
			IsCommandDone = true;
		}
	}
}