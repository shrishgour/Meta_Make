using Core.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : UIButton
{
    public void Init(string ID, Sprite sprite)
    {

    }

    public override void OnPressed()
    {
        base.OnPressed();
        UiManager.Instance.CloseAllDialogs();
        SceneManager.LoadScene("Map");
    }
}
