using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitfallBehavior : MonoBehaviour
{
    public LifeManager lifeManager;
    public Animator playerTopAnimator;
    public Animator playerBottomAnimator;
    public PlayerBehavior playerBehaviorScript;

    GameObject _playerShadow;

    private void Start()
    {
        lifeManager = GameObject.Find("LifeManager").GetComponent<LifeManager>();
        playerTopAnimator = GameObject.Find("PlayerTop").GetComponentInChildren<Animator>();
        playerBottomAnimator = GameObject.Find("PlayerBottom").GetComponentInChildren<Animator>();
        _playerShadow = GameObject.Find("Shadow");
        //playerBehaviorScript = GameObject.Find("Player").GetComponentInChildren<PlayerBehavior>();


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Rigidbody playerRb = GameObject.Find("PlayerTop").GetComponent<Rigidbody>();
            playerRb.isKinematic = true;
            _playerShadow.SetActive(false);
            //playerBehaviorScript.enabled = false;
            StartCoroutine(PlayerFall(other));

            
        }
    }

    private void TranslocatePlayerToOriginalPosition(Collider playerTopCollider)
    {
        playerTopCollider.transform.position = new Vector3(-8f, 0.9f, playerTopCollider.transform.position.z);
        lifeManager.playerLife = 3;
        _playerShadow.SetActive(true);
    }

    IEnumerator PlayerFall(Collider other)
    {
        AudioEventManager.Instance.TriggerCharacterFallingAudio();
        playerTopAnimator.SetTrigger("isFalling");
        playerBottomAnimator.SetTrigger("isFall");
        yield return new WaitForSeconds(1.5f);

        lifeManager.DestroyPlayer();

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
