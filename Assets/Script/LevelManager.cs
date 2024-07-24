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
    ObstacleSpawner _obstacleSpawner;
    [SerializeField]
    Collider _player;
    [SerializeField]
    GameObject _dragon;

    int _collectiblesCollected;
    int _nCollectiblesToWin;
    int _currentLevelID =-1;

    public void SetUpLevel(LevelScriptableObject levelConfig)
    {
        Debug.Log("Set Up Level " + levelConfig.levelID);
        if (levelConfig.levelID != _currentLevelID)
        {
            _currentLevelID = levelConfig.levelID;            
            _nCollectiblesToWin = levelConfig.trueCollectibleSpawnPoint.Count;

            _obstacleSpawner.ResetObstacles();
            foreach (ObstacleScriptableObject obstacleConfig in levelConfig.obstacles)
            {
                _obstacleSpawner.SpawnObstacles(obstacleConfig);
            }
        }
        _player.transform.position = levelConfig.playerStartPoint;
        _dragon.transform.position = levelConfig.dragonStartPoint;
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
