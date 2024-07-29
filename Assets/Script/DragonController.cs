using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DragonController : MonoBehaviour
{

    GameObject player;
    public GameObject fireBall, fireBallRef;

    public bool shouldFollowPlayer;
    public Transform fireBallSpawner;
    public Transform fireBallParent;
    public float fireBallCooldown = 2.0f ;
    public float fireBallTimer;
    public float animationSpeedFactor = 1f;
    public bool isShadow = false;
    public FireBallType fireBallType = FireBallType.THREE_SHOTS_LINEAR;

    Animator anim;

    // Start is called before the first frame update
    void Awake()
    {   
        anim = GetComponent<Animator>();
        fireBallTimer = 0;
        player = GameObject.Find("PlayerTop");

        anim.speed = animationSpeedFactor;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            followPlayer();
        }
    }

    private void spawFireBall()
    {
        fireBallRef = Instantiate(fireBall, fireBallParent);
        fireBallRef.transform.position = fireBallSpawner.transform.position;
        FireBallController fireballController = fireBallRef.GetComponent<FireBallController>();
        fireballController.fireBallType = fireBallType;
        fireBallTimer = 0;
        if (isShadow)
        {
            fireballController.setAsShadow();
        }
        
        
        
    }

    private void followPlayer()
    {
        if (shouldFollowPlayer)
        {
            transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        }
    }
}
