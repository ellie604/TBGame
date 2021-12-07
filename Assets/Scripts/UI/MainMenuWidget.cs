using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuWidget : BaseWidget
{
    public Button Btn_StartGame;
    public Button Btn_Option;
    public Button Btn_QuitGame;

    public override void OnOpenPage()
    {
        Btn_QuitGame.onClick.AddListener(GameManager.Instance.QuitGame);
    }

    public override void OnClosePage()
    {

    }
}
