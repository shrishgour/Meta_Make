using Core.Services;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Character
{
    public class Character : SerializedMonoBehaviour
    {
        [SerializeField] private CharacterData characterData;
        private string charID;
        private Camera cam;
        private RenderTexture renderTexture;

        public CharacterData GetCharacterData()
        {
            return characterData;
        }

        public void Init(string charID)
        {
            this.charID = charID;
            CreateCamera();
        }

        private void CreateCamera()
        {
            cam = new GameObject("CharacterCamera").AddComponent<Camera>();
            cam.transform.SetParent(transform, false);
            cam.orthographic = true;
            cam.orthographicSize = 9;
            cam.cullingMask = 1 << LayerMask.NameToLayer("Character");
            cam.clearFlags = CameraClearFlags.Skybox;
            cam.transform.localPosition = new Vector3(0, 7, -10);

            renderTexture = new RenderTexture(Screen.width, Screen.height, 16, RenderTextureFormat.ARGB32);
            renderTexture.Create();
            cam.targetTexture = renderTexture;
        }

        public RenderTexture GetUpdatedRenderTexture()
        {
            return renderTexture;
        }


        public void UpdateCharacter(string groupID, string variantID)
        {
            // foreach (var item in characterData.groupDataList.Find(x => x.groupID == groupID).variantDataList)
            // {
            //     if (item.variantID == variantID)
            //     {
            //         item.variant.SetActive(true);
            //     }
            // }

            var newVariant = characterData.groupDataList.Find(x => x.groupID == groupID).variantDataList.Find(y => y.variantID == variantID).variant;
            foreach (Transform item in newVariant.transform.parent)
            {
                item.gameObject.SetActive(false);
            }
            newVariant.SetActive(true);
        }

        public void SaveCharacterGroup(string groupID, string variantID)
        {
            var characterState = ServiceRegistry.Get<UserState>().CharacterState;

            if (!characterState.customizationStateData.ContainsKey(charID))
            {
                characterState.customizationStateData[charID] = new Dictionary<string, string>();
            }

            characterState.customizationStateData[charID][groupID] = variantID;
            ServiceRegistry.Get<UserState>().CharacterState = characterState;
        }

        public void LoadCharacterGroup(string groupID)
        {
            var customizationStateData = ServiceRegistry.Get<UserState>().CharacterState.customizationStateData;

            UpdateCharacter(groupID, customizationStateData[charID][groupID]);
        }

        public void LoadCharacter()
        {
            var customizationStateData = ServiceRegistry.Get<UserState>().CharacterState.customizationStateData;
            if (customizationStateData.ContainsKey(charID))
            {
                var charData = customizationStateData[charID];
                foreach (var item in charData)
                {
                    UpdateCharacter(item.Key, item.Value);
                }
            }
        }
    }
}
