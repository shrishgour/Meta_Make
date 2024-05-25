using Core.Events;
using Core.UI;
using Game.Events;
using UnityEngine;
using UnityEngine.UI;

public class GroupButton : UIButton
{
    [SerializeField] private Image iconImg;
    private string buttonID;

    public void Init(string ID, Sprite sprite)
    {
        buttonID = ID;
        iconImg.sprite = sprite;
    }

    public override void OnPressed()
    {
        base.OnPressed();
        EventManager.instance.TriggerEvent(new GroupSelectionEvent(buttonID));
    }
}