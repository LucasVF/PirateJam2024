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
    public float fireBallCooldown = 2.0f ;
    public float fireBallTimer;

    // Start is called before the first frame update
    void Start()
    {
        fireBallTimer = 0;
        player = GameObject.Find("PlayerTop");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            followPlayer();
            spawFireBall();
        }
    }

    private void spawFireBall()
    {
        fireBallTimer += Time.deltaTime;
        if (fireBallTimer > fireBallCooldown)
        {
            fireBallRef = Instantiate(fireBall);
            fireBallRef.transform.position = fireBallSpawner.transform.position;
            fireBallRef.GetComponent<FireBallController>().fireBallType = FireBallType.THREE_SHOTS_LINEAR;
            fireBallTimer = 0;
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
