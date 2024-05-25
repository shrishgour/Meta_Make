using Core.Config;
using Core.Events;
using Core.Services;
using Core.UI;
using Game.Economy;
using Game.Events;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class MainHud : BaseHud<MainHudState>
    {
        [SerializeField] private GameObject hudHolder;
        [SerializeField] private UIButton progressionButton;
        [SerializeField] private UIButton playButton;
        [SerializeField] private UIButton wardrobeButton;
        [SerializeField] private TextMeshProUGUI coinText;

        // Start is called before the first frame update
        public override void OnHUDOpen()
        {
            base.OnHUDOpen();
            RegisterListener();
        }

        private void RegisterListener()
        {
            progressionButton.AddPressedListener(() =>
            {
                UiHudManager.Instance.OpenHud(HudList.MainHud, MainHudStateList.TopBar);
                UiManager.Instance.OpenDialog<ProgressionDialog>(ProgressionDialog.DialogID, true, null);
            });

            playButton.AddPressedListener(() => HideHud(new HideHudEvent()));
            wardrobeButton.AddPressedListener(() => UiHudManager.Instance.OpenHud(HudList.WardrobeHud, WardrobeHudStateList.Group));

            EventManager.instance.AddListener<ShowHudEvent>(ShowHud);
            EventManager.instance.AddListener<HideHudEvent>(HideHud);
            EventManager.instance.AddListener<UpdateCurrencyEvent>(UpdateCurrencyText);
        }

        private void UnregisterListener()
        {
            EventManager.instance.RemoveListener<ShowHudEvent>(ShowHud);
            EventManager.instance.RemoveListener<HideHudEvent>(HideHud);
            EventManager.instance.RemoveListener<UpdateCurrencyEvent>(UpdateCurrencyText);
        }

        private void ShowHud(ShowHudEvent e)
        {
            hudHolder.SetActive(true);
        }

        private void HideHud(HideHudEvent e)
        {
            hudHolder.SetActive(false);
        }

        private void UpdateCurrencyText(UpdateCurrencyEvent e)
        {
            if (e.currencyType == "Coins")
            {
                coinText.SetText(ServiceRegistry.Get<EconomyService>().GetCurrencyAmount(e.currencyType).ToString());
            }
        }

        public override void OnHUDClosed()
        {
            base.OnHUDClosed();
            UnregisterListener();
        }
    }
}