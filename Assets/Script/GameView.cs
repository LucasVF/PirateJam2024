using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameView : View
{
    public override void DisplayView()
    {
        AudioEventManager.Instance.TriggerGameplayThemeAudio();
        ChangeViewDisplay(true);
    }

    public override void SetUpView()
    {
        Debug.Log("Setting Up Game View");
    }
}
