using Core.Events;
using Core.UI;
using Game.Events;
using UnityEngine;
using UnityEngine.UI;

public class VariantButton : UIButton
{
    [SerializeField] private Image iconImg;
    private string buttonID;

    public void Init(string ID, Sprite sprite)
    {
        iconImg.sprite = sprite;
        buttonID = ID;
    }

    public override void OnPressed()
    {
        base.OnPressed();
        EventManager.instance.TriggerEvent(new VariantSelectionEvent(buttonID));
    }
}
