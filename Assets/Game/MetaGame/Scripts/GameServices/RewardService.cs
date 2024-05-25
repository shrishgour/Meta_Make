using Core.Services;
using Game.Economy;
using System.Collections.Generic;

namespace Game.Rewards
{
    public class RewardService : BaseService
    {
        public bool GrantReward(RewardBundle rewardBundle)
        {
            var econmomyService = ServiceRegistry.Get<EconomyService>();
            foreach (var reward in rewardBundle.rewardList)
            {
                econmomyService.AddCurrency(reward);
            }
            return true;
        }
    }

    [System.Serializable]
    public class RewardBundle
    {
        public List<Currency> rewardList = new List<Currency>();
        public bool GrantReward => ServiceRegistry.Get<RewardService>().GrantReward(this);
    }
}
