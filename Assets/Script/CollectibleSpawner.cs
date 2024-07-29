using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    [SerializeField]
    List<Sprite> _realCollectibleSprite;
    [SerializeField]
    List<Sprite> _fakeCollectibleSprite;
    [SerializeField]
    GameObject _collectiblePrefab;

    int _realIndexSprite = 0;
    int _fakeIndexSprite = 0;

    List<GameObject> _activeRealCollectiblesSpawned = new List<GameObject>(); 
    List<GameObject> _activeFakeCollectiblesSpawned = new List<GameObject>();
    List<GameObject> _inactiveRealCollectiblesSpawned = new List<GameObject>();
    List<GameObject> _inactiveFakeCollectiblesSpawned = new List<GameObject>();

    public void SpawnCollectibleAt(Vector3 position, LevelManager levelManager, bool isReal = false)
    {
        List<GameObject> listToCheck = isReal ? _inactiveRealCollectiblesSpawned : _inactiveFakeCollectiblesSpawned;
        GameObject collectibleToSpawn;
        CollectibleBehavior collectibleBehavior;
        if (listToCheck.Count > 0)
        {
            Debug.Log("Activate Collectible");
            collectibleToSpawn = listToCheck[listToCheck.Count - 1];
            listToCheck.Remove(collectibleToSpawn);
            collectibleToSpawn.SetActive(true);
            collectibleToSpawn.transform.position = position;
            _activeRealCollectiblesSpawned.Add(collectibleToSpawn);
        }
        else
        {
            Debug.Log("New Collectible");
            collectibleToSpawn = Instantiate(_collectiblePrefab, position, Quaternion.identity);
            collectibleToSpawn.transform.parent = gameObject.transform;
            collectibleBehavior = collectibleToSpawn.GetComponentInChildren<CollectibleBehavior>();
            collectibleBehavior.SetLevelManager(levelManager);
            collectibleToSpawn.SetActive(true);
            _activeRealCollectiblesSpawned.Add(collectibleToSpawn);
        }
        collectibleBehavior = collectibleToSpawn.GetComponentInChildren<CollectibleBehavior>();
        int spriteIndex = isReal ? _realIndexSprite : _fakeIndexSprite;
        collectibleBehavior.SetUpSymbol(_realCollectibleSprite[spriteIndex]);

        if (isReal)
        {
            _realIndexSprite++;
            if (_realIndexSprite > _realCollectibleSprite.Count-1)
            {
                _realIndexSprite = 0;
            }
        }
        else
        {
            _fakeIndexSprite++;
            if (_fakeIndexSprite > _fakeCollectibleSprite.Count-1)
            {
                _fakeIndexSprite = 0;
            }
        }
    }

    public void ResetCollectibles()
    {
        if (_activeRealCollectiblesSpawned.Count > 0)
        {
            _activeRealCollectiblesSpawned.ForEach(collectible =>
            {
                collectible.SetActive(false);
                _inactiveRealCollectiblesSpawned.Add(collectible);
            });

            _activeRealCollectiblesSpawned.Clear();
        }
        if (_activeFakeCollectiblesSpawned.Count > 0)
        {
            _activeFakeCollectiblesSpawned.ForEach(collectible =>
            {
                collectible.SetActive(false);
                _inactiveFakeCollectiblesSpawned.Add(collectible);
            });

            _activeFakeCollectiblesSpawned.Clear();
        }
        _realIndexSprite = 0;
    }

    public void Collect(CollectibleBehavior collectible)
    {
        GameObject collectibleGameObject = collectible.transform.parent.gameObject;
        collectibleGameObject.SetActive(false);
        List<GameObject> activeList = collectible.IsFake ? _activeFakeCollectiblesSpawned : _activeRealCollectiblesSpawned;
        List<GameObject> inactiveList = collectible.IsFake ? _inactiveFakeCollectiblesSpawned : _inactiveRealCollectiblesSpawned;

        if (activeList.Remove(collectibleGameObject))
        {
            inactiveList.Add(collectibleGameObject);
        }     
    }
}
