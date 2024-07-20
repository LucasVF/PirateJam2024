using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FireBallType
{
    LINEAR,
    FOLLOW_PlAYER
}

public class FireBallController : MonoBehaviour
{
    public FireBallType fireBallType;
    public float horizontalSpeed = 5.0f, verticalSpeed = 0.5f;
    Action behavior;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        switch (fireBallType)
        {
            case FireBallType.LINEAR:
                behavior = linearBehavior;
                break;
            case FireBallType.FOLLOW_PlAYER:
                player = GameObject.FindGameObjectWithTag("Player");
                behavior = followPlayerBehavior;
                break; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        behavior.Invoke();
    }
    void linearBehavior()
    {
        transform.Translate(new Vector2(-horizontalSpeed * Time.deltaTime, 0));
    }

    void followPlayerBehavior()
    {
        float playerHorizontalDirection = transform.position.y - player.transform.position.y;
        transform.Translate(new Vector2(-horizontalSpeed * Time.deltaTime, -playerHorizontalDirection * verticalSpeed * Time.deltaTime));
    }
}
