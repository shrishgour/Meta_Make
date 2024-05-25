using Core.Services;
using Game.Config;

namespace Game.Progression
{
    public class ProgressionService : BaseService
    {
        private ProgressionManager progressionManager;
        public override void Initialize()
        {
            base.Initialize();
            progressionManager = new ProgressionManager();
        }

        public void StartChapter(string chapterID)
        {
            progressionManager.StartChapter(chapterID);
        }

        public void MarkQuestComplete(string questID)
        {
            progressionManager.MarkQuestComplete(questID);
        }

        public void GrantQuestReward(QuestData questData)
        {
            progressionManager.GrantQuestReward(questData);
        }
    }
}
