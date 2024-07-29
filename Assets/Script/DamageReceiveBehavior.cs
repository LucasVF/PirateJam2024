using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiveBehavior : MonoBehaviour
{
    public LifeManager lifeManager;
    public int fireBallDamageValue = 1;
    public bool isShadow = false;
    public PlayerBehavior playerBehaviorScript;
    public SpriteRenderer _sprite;
    [Header ("Invincible Parameters")]
    public float invencibilityTime = 3f;
    public int playerLayer = 1;
    public int fireballLayer = 2;

    [Header("Players Animators")]
    public Animator playerTopAnimator;
    public Animator playerBottomAnimator;


    // Start is called before the first frame update
    void Start()
    {
        lifeManager = GameObject.Find("LifeManager").GetComponent<LifeManager>();
        
    }

    private void OnDisable()
    {
        RemoveInvincibility();
        Color color = Color.white;
        color.a = 1;
        _sprite.color = color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "FireBall")
        {
            FireBallController fireBallController = collision.gameObject.transform.parent.GetComponent<FireBallController>();
            if (fireBallController is null || this.isShadow == fireBallController.isShadow)
            {
                if (lifeManager.playerLife <= 1)
                {
                    lifeManager.TakeDamage(fireBallDamageValue);
                    playerBehaviorScript.enabled = false;//
                    StartCoroutine(DeathAnimation());
                    //playerBehaviorScript.enabled = true;

                    //
                    //lifeManager.DestroyPlayer();
                    //playerTopAnimator.SetBool("isDead", false);//
                    Debug.Log("No life remaining...");
                }
                else
                {
                    lifeManager.TakeDamage(fireBallDamageValue);
                    StartCoroutine(ApplyInvencibility());
                    lifeManager.ShowLife();
                }
            }            
        }

    }

    IEnumerator ApplyInvencibility()
    {
        Physics.IgnoreLayerCollision(playerLayer, fireballLayer);
        ActivatePlayerInvincibleAnimation();
        yield return new WaitForSeconds(invencibilityTime);
        RemoveInvincibility();
    }

    public void ActivatePlayerInvincibleAnimation()
    {
        playerTopAnimator.SetLayerWeight(1, 1);
        playerBottomAnimator.SetLayerWeight(1, 1);
    }

    public void RemoveInvincibility()
    {
        Physics.IgnoreLayerCollision(playerLayer, fireballLayer, false);
        DeactivatePlayerInvincibleAnimation();
    }

    public void DeactivatePlayerInvincibleAnimation()
    {
        playerTopAnimator.SetLayerWeight(1, 0);
        playerBottomAnimator.SetLayerWeight(1, 0);
    }

    IEnumerator DeathAnimation()
    {
        playerTopAnimator.SetTrigger("isDead");
        playerBottomAnimator.SetTrigger("isDead");
        
        Debug.Log("isDead active");
        yield return new WaitForSeconds(1.5f);
        
        lifeManager.DestroyPlayer();
        //
    }

    
}
