using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartView : View
{
    public override void DisplayView()
    {
        AudioEventManager.Instance.TriggerTitleThemeAudio();
        ChangeViewDisplay(true);
    }

    public override void SetUpView()
    {
        Debug.Log("Setting Up Start View");
    }
}
