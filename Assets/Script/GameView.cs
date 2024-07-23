using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameView : View
{
    int _collectiblesCollected;
    //TODO: Change after Level Spawner is done
    public int _nCollectiblesToWin;

    [SerializeField]
    GameManager _gameManager;

    public override void DisplayView()
    {
        AudioEventManager.Instance.TriggerGameplayThemeAudio();
        ChangeViewDisplay(true);
    }

    public override void SetUpView()
    {
        Debug.Log("Setting Up Game View");
        _collectiblesCollected = 0;
        _nCollectiblesToWin = 5;
    }

    public void CollectibleCollected()
    {
        _collectiblesCollected++;
        Debug.Log("Collected: "+ _collectiblesCollected);
        if (_collectiblesCollected == _nCollectiblesToWin)
        {
            Debug.Log("EndGame");
            _gameManager.EndGame(true);
        }
    }
}
