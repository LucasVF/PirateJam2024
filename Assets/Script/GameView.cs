using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameView : View
{
    [SerializeField]
    GameManager _gameManager;
    [SerializeField]
    LevelManager _levelManager;
    [SerializeField]
    List<LevelScriptableObject> _levelList;

    public override void DisplayView()
    {
        AudioEventManager.Instance.TriggerGameplayThemeAudio();
        ChangeViewDisplay(true);
    }

    public override void SetUpView()
    {
        Debug.Log("Setting Up Game View");
        _levelManager.SetUpLevel(_levelList[0]);
    }
}
