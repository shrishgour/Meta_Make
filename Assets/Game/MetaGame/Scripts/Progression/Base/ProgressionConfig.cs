using Core.Config;
using Game.Economy;
using Game.Requirements;
using Game.Rewards;
using System.Collections.Generic;

namespace Game.Config
{
    public class ProgressionConfig : BaseMultiConfig<ChapterData, ProgressionConfig>
    {

    }

    [System.Serializable]
    public class ChapterData : IConfigData
    {
        public string ID => chapterID;
        public string chapterID;
        public List<Requirement> startRequirementList;
        public string startSequenceID;
        public string endSequenceID;
        public RewardBundle completionReward;
        public List<QuestData> questList;
    }

    [System.Serializable]
    public class QuestData
    {
        public string questID;
        public string questTitle;
        public List<Requirement> startRequirementList;
        public Currency price;
        public RewardBundle completionReward;
    }
}