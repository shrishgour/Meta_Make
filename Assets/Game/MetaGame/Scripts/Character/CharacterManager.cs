using Core.Config;
using Core.Services;
using Game.Config;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Character
{
    public class CharacterManager
    {
        private CharacterConfig characterConfig;
        private Dictionary<string, Character> characterMap;

        public CharacterManager()
        {
            characterConfig = ConfigRegistry.GetConfig<CharacterConfig>();
            characterMap = new Dictionary<string, Character>();
        }

        public void SpawnCharacter(string charID, string eventID)
        {
            if (characterConfig.Data.ContainsKey(charID))
            {
                var characterObj = GameObject.Instantiate(characterConfig.Data[charID].characterPrefabMap[eventID]);
                characterObj.name = charID;
                characterObj.transform.position = characterConfig.Data[charID].spawnPosition;
                characterMap[charID] = characterObj.GetComponent<Character>();
                characterMap[charID].Init(charID);
                LoadCharacter(charID);
            }
        }

        public CharacterData GetCharacterData(string charID)
        {
            if (characterMap.TryGetValue(charID, out var character))
            {
                return character.GetCharacterData();
            }

            return null;
        }

        public void UpdateCharacter(string charID, string groupID, string variantID)
        {
            if (characterMap.TryGetValue(charID, out var character))
            {
                character.UpdateCharacter(groupID, variantID);
            }
        }

        public RenderTexture GetCharacterRenderTexture(string charID)
        {
            if (characterMap.TryGetValue(charID, out var character))
            {
                return character.GetUpdatedRenderTexture();
            }

            return null;
        }

        public void SaveCharacterGroup(string charID, string groupID, string variantID)
        {
            if (characterMap.TryGetValue(charID, out var character))
            {
                character.SaveCharacterGroup(groupID, variantID);
            }
        }

        public void LoadCharacterGroup(string charID, string groupID)
        {
            if (characterMap.TryGetValue(charID, out var character))
            {
                character.LoadCharacterGroup(groupID);
            }
        }

        public void LoadCharacter(string charID)
        {
            if (characterMap.TryGetValue(charID, out var character))
            {
                character.LoadCharacter();
            }
        }

        public List<string> GetUnlockedGroups(string charID)
        {
            List<string> groupIDs = new List<string>();
            var customizationStateData = ServiceRegistry.Get<UserState>().CharacterState.customizationStateData;
            if (customizationStateData.ContainsKey(charID))
            {
                groupIDs = customizationStateData[charID].Keys.ToList();
            }
            return groupIDs;
        }
    }
}