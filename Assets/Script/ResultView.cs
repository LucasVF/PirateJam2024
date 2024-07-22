using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultView : View
{
    bool isWinner = false;

    public override void DisplayView()
    {
        AudioEventManager.Instance.TriggerResultThemeAudio(isWinner);
        ChangeViewDisplay(true);
    }

    public override void SetUpView()
    {
        Debug.Log("Setting Up Result View");
    }
}
