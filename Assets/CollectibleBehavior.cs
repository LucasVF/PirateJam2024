using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleBehavior : MonoBehaviour
{
    GameView _gameView;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collectible Collected");
        AudioEventManager.Instance.TriggerCollectibleAudio();
        //TODO: Remove when spawner is made
        GameView gameView = GameObject.FindAnyObjectByType<GameView>();
        gameView.CollectibleCollected();
        //========
        //_gameView?.CollectibleCollected();
        gameObject.transform.parent.gameObject.SetActive(false);
    }

    public void SetGameView(GameView gameView)
    {
        _gameView = gameView;
    }
}
