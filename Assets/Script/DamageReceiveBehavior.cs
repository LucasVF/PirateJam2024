using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiveBehavior : MonoBehaviour
{
    public LifeManager lifeManager;
    public int fireBallDamageValue = 1;
    [Header ("Invincible Parameters")]
    public float invencibilityTime = 3f;
    public int playerLayer = 1;
    public int fireballLayer = 2;


    // Start is called before the first frame update
    void Start()
    {
        lifeManager = GameObject.Find("LifeManager").GetComponent<LifeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "FireBall")
        {
            if (lifeManager.playerLife <= 1)
            {
                lifeManager.TakeDamage(fireBallDamageValue);
                lifeManager.DestroyPlayer();
                Debug.Log("No life remaining...");
            }
            else
            {
                lifeManager.TakeDamage(fireBallDamageValue);
                //StartCoroutine(ApplyInvencibility());
                lifeManager.ShowLife();
            }
        }

    }

    IEnumerator ApplyInvencibility()
    {
        Physics.IgnoreLayerCollision(playerLayer, fireballLayer);
        yield return new WaitForSeconds(invencibilityTime);
        Physics.IgnoreLayerCollision(playerLayer, fireballLayer, false);
    }
}
