using Core.Config;
using Core.Sequencer;
using Core.Services;
using Core.UI;
using Game.Config;
using Game.UI;
using UnityEngine;

namespace Game.Progression
{
    public class ProgressionManager
    {
        private ProgressionConfig progressConfig;
        private string chapterID;

        public ProgressionManager()
        {
            progressConfig = ConfigRegistry.GetConfig<ProgressionConfig>();

            var progressionState = ServiceRegistry.Get<UserState>().ProgressionState;
            chapterID = progressionState.activeChapterData.chapterID;
        }

        public void StartChapter(string chapterID)
        {
            var chapterData = progressConfig.Data[chapterID];

            this.chapterID = (chapterData != null) ? chapterID : this.chapterID;

            if (chapterData != null)
            {
                var startRequirement = chapterData.startRequirementList;
                if (startRequirement == null && startRequirement.Count > 0)
                {
                    foreach (var item in startRequirement)
                    {
                        if (!item.IsComplete)
                        {
                            return;
                        }
                    }
                }

                var progressionState = ServiceRegistry.Get<UserState>().ProgressionState;
                progressionState.activeChapterData.chapterID = chapterID;
                progressionState.activeChapterData.totalQuestCount = chapterData.questList.Count;
                Debug.Log($"Chapter {chapterID} started!");
                if (string.IsNullOrEmpty(chapterData.startSequenceID) || progressionState.activeChapterData.startSequenceCompleted)
                {
                    SetupCharacterDialog(chapterData);
                }
                else
                {
                    ServiceRegistry.Get<SequenceService>().StartSequence(chapterData.startSequenceID, 0, () =>
                    {
                        SetupCharacterDialog(chapterData);
                    });
                    progressionState.activeChapterData.startSequenceCompleted = true;
                }
                ServiceRegistry.Get<UserState>().ProgressionState = progressionState;
            }
        }

        private void SetupCharacterDialog(ChapterData chapterData)
        {
            StartAvaliableQuests(chapterData);
            UiManager.Instance.OpenDialog<CharacterCustomizationDialog>(CharacterCustomizationDialog.DialogID, false, null);
            UiHudManager.Instance.OpenHud(HudList.MainHud, MainHudStateList.Wardrobe);
        }

        public void ResumeChapter()
        {
            var chapterData = progressConfig.Data[chapterID];
            StartAvaliableQuests(chapterData);
        }

        private void StartAvaliableQuests(ChapterData chapterData)
        {
            var progressionState = ServiceRegistry.Get<UserState>().ProgressionState;

            foreach (var quest in chapterData.questList)
            {
                bool canStartQuest = true;
                foreach (var requirement in quest.startRequirementList)
                {
                    if (!requirement.IsComplete)
                    {
                        canStartQuest = false;
                        break;
                    }
                }

                Debug.Log($"Quest {quest.questID} started!");
                if (canStartQuest && !progressionState.activeChapterData.activeQuestList.Contains(quest.questID)
                && !progressionState.activeChapterData.completedQuestList.Contains(quest.questID))
                {
                    progressionState.activeChapterData.activeQuestList.Add(quest.questID);
                }
            }

            ServiceRegistry.Get<UserState>().ProgressionState = progressionState;
        }

        private bool CheckForChapterComplete()
        {
            var progressionState = ServiceRegistry.Get<UserState>().ProgressionState;
            return progressionState.activeChapterData.activeQuestList.Count >= progressionState.activeChapterData.completedQuestList.Count;
        }

        private void MarkChapterComplete()
        {
            var progressionState = ServiceRegistry.Get<UserState>().ProgressionState;
            progressionState.completedChapterList.Add(chapterID);
            ServiceRegistry.Get<UserState>().ProgressionState = progressionState;
        }

        public void MarkQuestComplete(string questID)
        {
            var progressionState = ServiceRegistry.Get<UserState>().ProgressionState;
            progressionState.activeChapterData.activeQuestList.Remove(questID);
            progressionState.activeChapterData.completedQuestList.Add(questID);
            ServiceRegistry.Get<UserState>().ProgressionState = progressionState;

            Debug.Log($"Quest {questID} Completed!");

            if (CheckForChapterComplete())
            {
                MarkChapterComplete();
            }
            else
            {
                ResumeChapter();
            }
        }

        public void GrantQuestReward(QuestData questData)
        {
            var result = questData.completionReward.GrantReward;
        }
    }
}
