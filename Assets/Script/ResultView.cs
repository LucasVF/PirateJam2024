using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultView : View
{
    bool _isWinner = false;

    public override void DisplayView()
    {
        AudioEventManager.Instance.TriggerResultThemeAudio(_isWinner);
        ChangeViewDisplay(true);
    }

    public override void SetUpView()
    {
        Debug.Log("Setting Up Result View");
    }

    public void SetWinner(bool isWinner)
    {
        _isWinner = isWinner;
    }
}
