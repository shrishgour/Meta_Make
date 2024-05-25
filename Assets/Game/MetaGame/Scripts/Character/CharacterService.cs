using Core.Services;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Character
{
    public class CharacterService : BaseService
    {
        private CharacterManager manager;
        public override void Initialize()
        {
            base.Initialize();

            manager = new CharacterManager();
            SpawnCharacter("Lilly", "Scene1");
            SpawnCharacter("Ethan", "Scene1");
        }

        public void SpawnCharacter(string charID, string eventID)
        {
            manager.SpawnCharacter(charID, eventID);
        }

        public CharacterData GetCharacterData(string charID)
        {
            return manager.GetCharacterData(charID);
        }

        public void UpdateCharacter(string charID, string groupID, string variantID)
        {
            manager.UpdateCharacter(charID, groupID, variantID);
        }

        public RenderTexture GetCharacterRenderTexture(string charID)
        {
            return manager.GetCharacterRenderTexture(charID);
        }

        public void SaveCharacterGroup(string charID, string groupID, string variantID)
        {
            manager.SaveCharacterGroup(charID, groupID, variantID);
        }

        public void LoadCharacterGroup(string charID, string groupID)
        {
            manager.LoadCharacterGroup(charID, groupID);
        }

        public void LoadCharacter(string charID)
        {
            manager.LoadCharacter(charID);
        }

        public List<string> GetUnlockedGroups(string charID)
        {
            return manager.GetUnlockedGroups(charID);
        }
    }
}
