using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitfallBehavior : MonoBehaviour
{
    public LifeManager lifeManager;

    private void Start()
    {
        lifeManager = GameObject.Find("LifeManager").GetComponent<LifeManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            lifeManager.playerLife = 0;
            Destroy(other.gameObject);
            GameObject shadowPlayer = GameObject.Find("PlayerBottom");
            Destroy(shadowPlayer);
            Debug.Log("Death by pitfall!");
            GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
            GameManager gameManagerComponent = gameManager.GetComponent<GameManager>();
            gameManagerComponent.EndGame(false);
        }
    }

    
}
