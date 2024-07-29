using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum FireBallType
{
    LINEAR,
    FOLLOW_PlAYER,
    THREE_SHOTS,
    THREE_SHOTS_LINEAR

}

public class FireBallController : MonoBehaviour
{
    public FireBallType fireBallType;
    public bool isShadow = false;
    public float horizontalSpeed = 5.0f, verticalFollowSpeed = 0.5f, threeLinearTimer, 
        threeLinearTargetTimer = 0.2f, threeLinearMaxMovTimer = 1.0f, scatterSpeed = 1.5f;
    Action behavior;
    GameObject player;
    public GameObject upperCopy, downCopy, middleCopy;
    public Transform upperCopyPosition, downCopyPosition; 

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
            case FireBallType.THREE_SHOTS:
                upperCopy.SetActive(true);
                downCopy.SetActive(true);
                threeLinearTimer = 0;   
                behavior = threeShots;
                break;
            case FireBallType.THREE_SHOTS_LINEAR:
                upperCopy.SetActive(true);
                downCopy.SetActive(true);
                threeLinearTimer = 0;
                behavior = threeShotsLinear;
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
        transform.Translate(new Vector2(-horizontalSpeed * Time.deltaTime, -playerHorizontalDirection * verticalFollowSpeed * Time.deltaTime));
    }

    void threeShots()
    {
        if (threeLinearTimer > threeLinearTargetTimer)
        {
            upperCopy.transform.Translate( Vector2.up * scatterSpeed * Time.deltaTime);
            downCopy.transform.Translate(Vector2.down * scatterSpeed * Time.deltaTime);
        }
        threeLinearTimer += Time.deltaTime;
        transform.Translate(new Vector2(-horizontalSpeed * Time.deltaTime, 0));
    }

    void threeShotsLinear()
    {
        if (threeLinearTimer < threeLinearMaxMovTimer)
        {
            upperCopy.transform.Translate(Vector2.up * scatterSpeed * Time.deltaTime);
            downCopy.transform.Translate(Vector2.down * scatterSpeed * Time.deltaTime); ;
        }
        threeLinearTimer += Time.deltaTime;
        transform.Translate(new Vector2(-horizontalSpeed * Time.deltaTime, 0));
    }

    public void setAsShadow()
    {
        isShadow = true;
        //upperCopy.GetComponent<Animator>().SetBool("isShadow", true);
        middleCopy.GetComponent<Animator>().SetBool("isShadow", true);
        //downCopy.GetComponent<Animator>().SetBool("isShadow", true);
        SpriteRenderer[] spriteRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.sortingLayerName = "ShadowRealm";
            spriteRenderer.color = Color.black;
        }
    }
}
