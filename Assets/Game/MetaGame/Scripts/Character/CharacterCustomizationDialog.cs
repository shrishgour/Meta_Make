using Core.Services;
using Core.UI;
using Game.Character;
using Game.Services;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCustomizationDialog : BaseDialog
{
    public const string DialogID = nameof(CharacterCustomizationDialog);
    [SerializeField] private RawImage characterTexture;
    [SerializeField] private string charID;
    public override void OnDialogOpen()
    {
        base.OnDialogOpen();
        if (ServiceRegistry.Get<BackgroundService>().GetCurrentBackgroundName() != "Room")
        {
            ServiceRegistry.Get<BackgroundService>().SetBackground("Room");
        }

        SetupCharacterTexture();
    }

    public override void OnDialogClosed()
    {
        base.OnDialogClosed();
    }

    private void SetupCharacterTexture()
    {
        characterTexture.texture = ServiceRegistry.Get<CharacterService>().GetCharacterRenderTexture(charID);
    }

}
