using Core.Services;
using Core.UI;
using Game.Config;
using Game.Economy;
using Game.Progression;
using TMPro;
using UnityEngine;

public class Quest : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questTitle;
    [SerializeField] private UIButton priceBtn;

    private QuestData questData;

    public void Initialize(QuestData questData)
    {
        this.questData = questData;
        questTitle.SetText(questData.questTitle);
        priceBtn.SetText(questData.price.value.ToString());
        priceBtn.AddPressedListener(OnPriceButtonPress);
    }

    private void OnDestroy()
    {
        priceBtn.RemoveAllPressedListeners();
    }

    private void OnPriceButtonPress()
    {
        var price = questData.price;
        if (ServiceRegistry.Get<EconomyService>().SubtractCurrency(price.currencyType.ToString(), price.value))
        {
            UiManager.Instance.CloseDialog(ProgressionDialog.DialogID);
            ServiceRegistry.Get<ProgressionService>().MarkQuestComplete(questData.questID);
            ServiceRegistry.Get<ProgressionService>().GrantQuestReward(questData);
        }
        else
        {
            //TODO: put some logic here for, when user don't have enough amount of currency 
        }
    }
}
