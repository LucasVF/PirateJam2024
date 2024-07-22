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
            //Destroy(other.gameObject); // Disable Game Object!!
            GameObject shadowPlayer = GameObject.Find("PlayerBottom");
            //Destroy(shadowPlayer); //Disable Game Object
            Debug.Log("Death by pitfall!");
            GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
            GameManager gameManagerComponent = gameManager.GetComponent<GameManager>();
            gameManagerComponent.EndGame(false);


            TranslocatePlayerToOriginalPosition(other);
        }
    }

    private void TranslocatePlayerToOriginalPosition(Collider playerTopCollider)
    {
        playerTopCollider.transform.position = new Vector3(-8f, 0.9f, playerTopCollider.transform.position.z);
        lifeManager.playerLife = 3;
    }

    
}
