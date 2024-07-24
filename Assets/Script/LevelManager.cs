using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    GameManager _gameManager;
    [SerializeField]
    CollectibleSpawner _collectibleSpawner;
    [SerializeField]
    Collider _player;

    int _collectiblesCollected;
    int _nCollectiblesToWin;

    public void SetUpLevel(LevelScriptableObject levelConfig)
    {
        Debug.Log("Set Up Level");        
        _player.transform.position = levelConfig.playerStartPoint;
        _nCollectiblesToWin = levelConfig.trueCollectibleSpawnPoint.Count;
        _collectiblesCollected = 0;
        _collectibleSpawner.ResetCollectibles();
        foreach (Vector3 point in levelConfig.trueCollectibleSpawnPoint)
        {
            _collectibleSpawner.SpawnCollectibleAt(point, this, true);
        }
        foreach (Vector3 point in levelConfig.fakeCollectibleSpawnPoint)
        {
            _collectibleSpawner.SpawnCollectibleAt(point, this, true);
        }

    }

    public void CollectibleCollected(CollectibleBehavior collectible)
    {
        _collectibleSpawner.Collect(collectible);
        _collectiblesCollected++;
        Debug.Log("Collected: " + _collectiblesCollected);
        if (_collectiblesCollected == _nCollectiblesToWin)
        {
            Debug.Log("EndGame");
            _collectibleSpawner.ResetCollectibles();
            _gameManager.EndGame(true);
        }
    }
}
