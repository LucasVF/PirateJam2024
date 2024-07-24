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

    List<GameObject> _activeRealCollectiblesSpawned = new List<GameObject>(); 
    List<GameObject> _activeFakeCollectiblesSpawned = new List<GameObject>();
    List<GameObject> _inactiveRealCollectiblesSpawned = new List<GameObject>();
    List<GameObject> _inactiveFakeCollectiblesSpawned = new List<GameObject>();

    public void SpawnCollectibleAt(Vector3 position, LevelManager levelManager, bool isReal = false)
    {
        List<GameObject> listToCheck = isReal ? _inactiveRealCollectiblesSpawned : _inactiveFakeCollectiblesSpawned;
        if (listToCheck.Count > 0)
        {
            Debug.Log("Activate Collectible");
            GameObject inactiveCollectible = listToCheck[listToCheck.Count - 1];
            listToCheck.Remove(inactiveCollectible);
            inactiveCollectible.transform.position = position;
            inactiveCollectible.SetActive(true);
            _activeRealCollectiblesSpawned.Add(inactiveCollectible);
        }
        else
        {
            Debug.Log("New Collectible");
            GameObject newCollectible = Instantiate(_collectiblePrefab, position, Quaternion.identity);
            newCollectible.transform.parent = gameObject.transform;
            CollectibleBehavior collectibleBehavior = newCollectible.GetComponentInChildren<CollectibleBehavior>();
            collectibleBehavior.SetLevelManager(levelManager);
            newCollectible.SetActive(true);
            _activeRealCollectiblesSpawned.Add(newCollectible);
        }
    }

    public void ResetCollectibles()
    {
        while (_activeRealCollectiblesSpawned.Count > 0)
        {
            _activeRealCollectiblesSpawned[0].SetActive(false);
            _inactiveRealCollectiblesSpawned.Add(_activeRealCollectiblesSpawned[0]);
            _activeRealCollectiblesSpawned.Remove(_activeRealCollectiblesSpawned[0]);
        }
        while (_activeRealCollectiblesSpawned.Count > 0)
        {
            _activeFakeCollectiblesSpawned[0].SetActive(false);
            _inactiveFakeCollectiblesSpawned.Add(_activeFakeCollectiblesSpawned[0]);
            _activeFakeCollectiblesSpawned.Remove(_activeFakeCollectiblesSpawned[0]);
        }
    }

    public void Collect(CollectibleBehavior collectible)
    {
        GameObject collectibleGameObject = collectible.transform.parent.gameObject;
        collectibleGameObject.SetActive(false);
        if (_activeRealCollectiblesSpawned.Remove(collectibleGameObject))
        {
            _inactiveRealCollectiblesSpawned.Add(collectibleGameObject);
        }
        else
        {
            _activeFakeCollectiblesSpawned.Remove(collectibleGameObject);
            _inactiveFakeCollectiblesSpawned.Add(collectibleGameObject);
        }        
    }
}
