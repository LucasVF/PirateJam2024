using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultView : View
{
    bool _isWinner = false;
    [SerializeField]
    Text _resultsText;

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
        _resultsText.text = isWinner ? "Yay! You are rich" : "Boohoo... You are poor =(";
        _isWinner = isWinner;
    }
}
