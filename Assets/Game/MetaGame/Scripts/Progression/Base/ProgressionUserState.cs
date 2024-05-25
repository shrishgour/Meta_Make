using Core.Data;
using System.Collections.Generic;

namespace Game.Data
{
    public class ProgressionUserState : StateHandler
    {
        public const string key = nameof(ProgressionUserState);

        public override string Key => key;

        public List<string> completedChapterList;
        public ChapterStateData activeChapterData;

        public ProgressionUserState()
        {
            completedChapterList = new List<string>();
            activeChapterData = new ChapterStateData();
        }

        public override void Reset()
        {
            completedChapterList = new List<string>();
            activeChapterData = new ChapterStateData();
            base.Reset();
        }
    }

    [System.Serializable]
    public class ChapterStateData
    {
        public string chapterID;
        public int totalQuestCount;
        public List<string> activeQuestList;
        public List<string> completedQuestList;
        public bool startSequenceCompleted;
        public bool endSequenceCompleted;

        public ChapterStateData()
        {
            completedQuestList = new List<string>();
            activeQuestList = new List<string>();
        }
    }
}