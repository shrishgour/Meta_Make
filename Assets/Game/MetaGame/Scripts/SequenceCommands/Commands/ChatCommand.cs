using Core.UI;
using Game.Attribute;

namespace Core.Sequencer.Commands
{
    [System.Serializable]
    public class ChatCommand : Command
    {
        public ChatData chatData;
        public override bool IsCommandDone { get; protected set; }
        public override bool IsSkippable { get; protected set; }

        public override void Start()
        {
            UiManager.Instance.OpenDialog<ChatDialog>(ChatDialog.DialogID, true, (dialog) =>
            {
                dialog.Init(chatData, OnClose);
            });
        }

        protected override void OnSkip()
        {

        }

        private void OnClose()
        {
            UiManager.Instance.CloseDialog(ChatDialog.DialogID);
            IsCommandDone = true;
        }
    }

    [System.Serializable]
    public class ChatData
    {
        [CharacterID]
        public string charID;
        public string chatString;
        [ScreenOrientation]
        public string screenOrient;
    }
}