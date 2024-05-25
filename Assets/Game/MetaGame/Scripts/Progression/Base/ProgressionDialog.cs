using Core.Config;
using Core.Events;
using Core.Services;
using Core.UI;
using Game.Config;
using Game.Events;
using Game.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionDialog : BaseDialog
{
    public const string DialogID = nameof(ProgressionDialog);

    [SerializeField] private GameObject questPrefab;
    [SerializeField] private Transform questHolder;
    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI progressBarText;
    [SerializeField] private UIButton closeButton;

    private ProgressionConfig progressionConfig;
    private Dictionary<string, GameObject> populatedQuestMap;

    public override void OnDialogOpen()
    {
        if (progressionConfig == null)
        {
            progressionConfig = ConfigRegistry.GetConfig<ProgressionConfig>();
        }

        populatedQuestMap ??= new Dictionary<string, GameObject>();
        closeButton.AddPressedListener(() => CloseDialog());
        PopulateAvaliableQuests();
        Invoke("RemoveCompletedQuests", 1);
        Invoke("UpdateProgressBar", 1);
    }

    public override void OnDialogClosed()
    {
        base.OnDialogClosed();
        UiHudManager.Instance.OpenHud(HudList.MainHud, MainHudStateList.Wardrobe);
        EventManager.instance.TriggerEvent<ShowHudEvent>(new ShowHudEvent());
    }

    private void PopulateAvaliableQuests()
    {
        var activeChapterData = ServiceRegistry.Get<UserState>().ProgressionState.activeChapterData;

        foreach (var questID in activeChapterData.activeQuestList)
        {
            var chapterData = progressionConfig.Data[activeChapterData.chapterID];
            var questData = chapterData.questList.Find(x => x.questID == questID);

            if (questData != null && !populatedQuestMap.ContainsKey(questID))
            {
                var questObject = Instantiate(questPrefab, questHolder);
                questObject.GetComponent<Quest>().Initialize(questData);
                populatedQuestMap.Add(questID, questObject);
            }
        }
    }

    private void RemoveCompletedQuests()
    {
        var activeChapterData = ServiceRegistry.Get<UserState>().ProgressionState.activeChapterData;

        foreach (var questID in activeChapterData.completedQuestList)
        {
            if (populatedQuestMap.ContainsKey(questID))
            {
                var questObject = populatedQuestMap[questID];
                populatedQuestMap.Remove(questID);
                Destroy(questObject);
            }
        }
    }

    private void UpdateProgressBar()
    {
        var activeChapterData = ServiceRegistry.Get<UserState>().ProgressionState.activeChapterData;
        progressBar.value = (activeChapterData.completedQuestList.Count * 1f / activeChapterData.totalQuestCount);
        progressBarText.SetText(((int)((progressBar.value) * 100)).ToString() + " %");
    }
}
