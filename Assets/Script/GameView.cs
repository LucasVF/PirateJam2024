using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameView : View
{
    [SerializeField]
    GameManager _gameManager;
    [SerializeField]
    LevelManager _levelManager;
    LevelScriptableObject _levelConfig;

    public override void DisplayView()
    {
        AudioEventManager.Instance.TriggerGameplayThemeAudio();
        ChangeViewDisplay(true);
        _levelManager.LevelStarted();
    }

    public override void SetUpView()
    {
        Debug.Log("Setting Up Game View");
        _levelManager.SetUpLevel(_levelConfig);
    }

    public void SetLevelConfig(LevelScriptableObject levelConfig)
    {
        _levelConfig = levelConfig;
    }
}
