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
    Vector3 characterStartZone = new Vector3(-7.030001f, 1f, 0f);
    Vector3 collectibleSpawnPoint = new Vector3(5.78999996f, 4.48000002f, 0.834488809f);

    //TODO: Change after Level Spawner is done
    public int _nCollectiblesToWin = 1;

    public void SetUpLevel()
    {
        Debug.Log("Set Up Level");        
        _player.transform.position = characterStartZone;
        _collectiblesCollected = 0;
        _collectibleSpawner.ResetCollectibles();
        _collectibleSpawner.SpawnCollectibleAt(collectibleSpawnPoint, this, true);
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
