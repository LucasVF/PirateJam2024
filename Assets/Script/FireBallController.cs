using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FireBallType
{
    LINEAR
}

public class FireBallController : MonoBehaviour
{
    public FireBallType fireBallType;
    public float speed = 5.0f;
    Action behavior;

    // Start is called before the first frame update
    void Start()
    {
        switch (fireBallType)
        {
            case FireBallType.LINEAR:
                behavior = linearBehavior;
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
        transform.Translate(new Vector2(-speed * Time.deltaTime, 0));
    }
}
