using Core.Sequencer;
using Game.Character;
using Game.Economy;
using Game.Progression;
using Game.Requirements;
using Game.Rewards;
using Game.Services;
using UnityEngine;
namespace Core.Services
{
    public class ServiceBinder : MonoBehaviour
    {
        public static bool IsInitialized { get; private set; }

        void Awake()
        {
            ServiceRegistry.Bind<UserState>(new UserState());
            ServiceRegistry.Bind<SequenceService>(new SequenceService());
            ServiceRegistry.Bind<ProgressionService>(new ProgressionService());
            ServiceRegistry.Bind<RequirementService>(new RequirementService());
            ServiceRegistry.Bind<EconomyService>(new EconomyService());
            ServiceRegistry.Bind<RewardService>(new RewardService());
            ServiceRegistry.Bind<CharacterService>(new CharacterService());
            ServiceRegistry.Bind<BackgroundService>(new BackgroundService());
            ServiceRegistry.Bind<TypingService>(new TypingService());

            IsInitialized = true;
        }

        private void Start()
        {

        }

        private void Update()
        {
            ServiceRegistry.Get<SequenceService>().Update();
        }
    }
}
