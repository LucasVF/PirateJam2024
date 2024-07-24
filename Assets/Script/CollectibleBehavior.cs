using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleBehavior : MonoBehaviour
{
    LevelManager _levelManager;
    [SerializeField]
    Sprite _symbol;

    private void OnTriggerEnter(Collider other)
    {
        if (transform.parent.gameObject.activeSelf && other.tag == "Player")
        {
            Debug.Log("Collectible Collected");
            AudioEventManager.Instance.TriggerCollectibleAudio();
            _levelManager?.CollectibleCollected(this);
        }           
    }

    public void SetLevelManager(LevelManager levelManager)
    {
        _levelManager = levelManager;
    }

    public void SetUpSymbol(Sprite symbolSprite)
    {
        _symbol = symbolSprite;
    }
}
