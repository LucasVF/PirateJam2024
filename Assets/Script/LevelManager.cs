using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    DragonController _dragonController;
    [SerializeField]
    DragonController _shadowDragonController;
    [SerializeField]
    Text _collectibleUI;

    [SerializeField]
    PlayerBehavior playerBehaviorScript;
    [SerializeField]
    Animator playerTopAnimator;
    [SerializeField]
    Animator playerBottomAnimator;

    public int playerLayer = 6;
    public int fireballLayer = 7;

    int _collectiblesCollected;
    int _nCollectiblesToWin;
    int _currentLevelID =-1;

    private float elapsedTime = 0f;  // To track the elapsed time
    private float nextUpdateTime = 0f;

    public void SetUpLevel(LevelScriptableObject levelConfig)
    {
        Debug.Log("Set Up Level " + levelConfig.levelID);
        //if (levelConfig.levelID != _currentLevelID)
        //{
            _currentLevelID = levelConfig.levelID;            
            _nCollectiblesToWin = levelConfig.trueCollectibleSpawnPoint.Count;

            _obstacleSpawner.ResetObstacles();
            foreach (ObstacleScriptableObject obstacleConfig in levelConfig.obstacles)
            {
                _obstacleSpawner.SpawnObstacles(obstacleConfig);
            }
        //}
        _player.transform.position = levelConfig.playerStartPoint;
        _dragonController.transform.parent.position = levelConfig.dragonStartPoint;
        _dragonController.fireBallType = levelConfig.dragonFireBallType;
        _dragonController.animationSpeedFactor = levelConfig.dragonAnimationSpeedFactor;
        _shadowDragonController.animationSpeedFactor = levelConfig.dragonAnimationSpeedFactor;
       _collectiblesCollected = 0;
        UpdateUI();
        _collectibleSpawner.ResetCollectibles();
        foreach (Vector3 point in levelConfig.trueCollectibleSpawnPoint)
        {
            _collectibleSpawner.SpawnCollectibleAt(point, this, true);
        }
        foreach (Vector3 point in levelConfig.fakeCollectibleSpawnPoint)
        {
            _collectibleSpawner.SpawnCollectibleAt(point, this);
        }  
    }

    public void LevelStarted()
    {
        elapsedTime = 0f;
        nextUpdateTime = 0f;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        
        if (Time.time >= nextUpdateTime)
        {
            nextUpdateTime = Time.time + 1f;
        }
    }

    public void CollectibleCollected(CollectibleBehavior collectible)
    {
        _collectibleSpawner.Collect(collectible);
        _collectiblesCollected++;
        Debug.Log("Collected: " + _collectiblesCollected);
        UpdateUI();
        if (_collectiblesCollected == _nCollectiblesToWin)
        {
            
            
            
            StartCoroutine(WinGameCoroutine());
        }
    }

    public void UpdateUI()
    {
        _collectibleUI.text = "x " + _collectiblesCollected + "/" +  _nCollectiblesToWin;
    }

    IEnumerator WinGameCoroutine()
    {
        Rigidbody playerRb = GameObject.Find("PlayerTop").GetComponent<Rigidbody>();
        playerRb.isKinematic = true;
        Physics.IgnoreLayerCollision(playerLayer, fireballLayer);

        playerTopAnimator.SetTrigger("victory");
        playerBottomAnimator.SetTrigger("victory");
        AudioEventManager.Instance.StopTheme();
        AudioEventManager.Instance.TriggerCharacterCommemorationAudio();

        yield return new WaitForSeconds(1.8f);

        Debug.Log("EndGame");
        playerRb.isKinematic = false;
        Physics.IgnoreLayerCollision(playerLayer, fireballLayer, false);
        _collectibleSpawner.ResetCollectibles();
        _gameManager.EndGame(true, elapsedTime);
    }
}
