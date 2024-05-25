using Core.SceneHandler;
using Core.Sequencer;
using Core.Services;
using Game.Character;
using Game.Economy;
using Game.Progression;
using Game.Services;

public class MainMenuSceneHandler : BaseSceneHandler
{
    protected override void OnInitialize()
    {
        base.OnInitialize();
        ServiceRegistry.Get<EconomyService>().Initialize();
        ServiceRegistry.Get<EconomyService>().AddCurrency(new Currency(CurrencyType.Coins, 1000, string.Empty));

        ServiceRegistry.Get<SequenceService>().Initialize();
        ServiceRegistry.Get<CharacterService>().Initialize();
        ServiceRegistry.Get<BackgroundService>().Initialize();

        ServiceRegistry.Get<ProgressionService>().Initialize();
        ServiceRegistry.Get<ProgressionService>().StartChapter("FirstChapter");
    }

    protected override void PostInitialize()
    {
        base.PostInitialize();
    }
}
