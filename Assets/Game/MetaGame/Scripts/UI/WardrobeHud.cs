using Core.Config;
using Core.Events;
using Core.Services;
using Core.UI;
using Game.Character;
using Game.Config;
using Game.Events;
using UnityEngine;

namespace Game.UI
{
    public class WardrobeHud : BaseHud<WardrobeHudState>
    {
        [SerializeField] private string charID;
        [SerializeField] private GameObject groupPrefab;
        [SerializeField] private Transform groupParant;
        [SerializeField] private GameObject variantPrefab;
        [SerializeField] private Transform variantParant;
        [SerializeField] private UIButton getButton;
        [SerializeField] private UIButton backButton;
        [SerializeField] private UIButton variantBackButton;

        private CharacterData characterData;
        private string currentGroupID = string.Empty;
        private string currentVariantID = string.Empty;
        private CharacterGroupIconConfig groupIconConfig;

        public override void OnHUDOpen()
        {
            base.OnHUDOpen();

            characterData = ServiceRegistry.Get<CharacterService>().GetCharacterData(charID);
            groupIconConfig = ConfigRegistry.GetConfig<CharacterGroupIconConfig>();
            RegisterListeners();
            SetupGroupLayout();
        }

        public override void OnHUDClosed()
        {
            base.OnHUDClosed();
            UnRegisterListeners();
        }

        private void RegisterListeners()
        {
            EventManager.instance.AddListener<GroupSelectionEvent>(OnGroupSelection);
            EventManager.instance.AddListener<VariantSelectionEvent>(OnVariantSelection);
            getButton.AddPressedListener(OnGetButtonPressed);
            backButton.AddPressedListener(() => UiHudManager.Instance.OpenHud(HudList.MainHud, MainHudStateList.Wardrobe));
            variantBackButton.AddPressedListener(() => UiHudManager.Instance.OpenHud(HudList.WardrobeHud, WardrobeHudStateList.Group));
        }

        private void UnRegisterListeners()
        {
            EventManager.instance.RemoveListener<GroupSelectionEvent>(OnGroupSelection);
            EventManager.instance.RemoveListener<VariantSelectionEvent>(OnVariantSelection);
            getButton?.RemovePressedListener(OnGetButtonPressed);
        }

        private void OnGroupSelection(GroupSelectionEvent e)
        {
            currentGroupID = e.groupID;
            SetupVariantLayout();
            if (e.isRewarded)
            {
                UiHudManager.Instance.OpenHud(HudList.WardrobeHud, WardrobeHudStateList.VariantOnly);
            }
            else
            {
                UiHudManager.Instance.OpenHud(HudList.WardrobeHud, WardrobeHudStateList.Variant);
            }
        }

        private void OnVariantSelection(VariantSelectionEvent e)
        {
            currentVariantID = e.variantID;

            Debug.Log("currentGroupID = " + currentGroupID);
            Debug.Log("currentVariantID = " + currentVariantID);

            ServiceRegistry.Get<CharacterService>().UpdateCharacter(charID, currentGroupID, currentVariantID);
        }

        private void OnGetButtonPressed()
        {
            ServiceRegistry.Get<CharacterService>().SaveCharacterGroup(charID, currentGroupID, currentVariantID);
            variantParant.parent.gameObject.SetActive(false);

            if (currentState.StateName == "VariantOnly")
            {
                UiHudManager.Instance.OpenHud(HudList.MainHud, MainHudStateList.Wardrobe);
            }
            else
            {
                UiHudManager.Instance.OpenHud(HudList.WardrobeHud, WardrobeHudStateList.Group);
            }
        }

        private void SetupGroupLayout()
        {
            CleanUpParant(groupParant);

            var unlockedGroups = ServiceRegistry.Get<CharacterService>().GetUnlockedGroups(charID);

            if (characterData != null)
            {
                foreach (var item in characterData.groupDataList)
                {
                    if (unlockedGroups.Contains(item.groupID))
                    {
                        var groupBtn = Instantiate(groupPrefab, groupParant, false).GetComponent<GroupButton>();
                        groupBtn.Init(item.groupID, groupIconConfig.Data[item.groupID].icon);
                    }
                }
            }
        }

        private void SetupVariantLayout()
        {
            CleanUpParant(variantParant);

            if (!string.IsNullOrEmpty(currentGroupID))
            {
                var groupData = characterData.groupDataList.Find(x => x.groupID == currentGroupID);

                foreach (var item in groupData.variantDataList)
                {
                    var variantBtn = Instantiate(variantPrefab, variantParant, false).GetComponent<VariantButton>();
                    variantBtn.Init(item.variantID, item.icon);
                }
            }

            variantParant.parent.gameObject.SetActive(true);
        }

        private void CleanUpParant(Transform parant)
        {
            foreach (Transform item in parant)
            {
                Destroy(item.gameObject);
            }
        }
    }
}