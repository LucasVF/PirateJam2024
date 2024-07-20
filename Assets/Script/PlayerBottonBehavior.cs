using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBottonBehavior : MonoBehaviour
{
    public Transform playerTopTransform;
    private Rigidbody rb;
    public Animator playerAnimator;
    public Animator playerBottomAnimator;
    public SpriteRenderer playerSprite;
    public SpriteRenderer playerBottomSprite;
    public int jumpForce = 5;
    private bool jumpButtonPressed = false;
    private bool hasJumping = false;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerTopTransform = GameObject.Find("PlayerTop").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayerTop();

        jumpButtonPressed |= Input.GetKeyDown(KeyCode.Space);
    }

    private void FixedUpdate()
    {
        Jump();
    }

    private void FollowPlayerTop()
    {
        transform.position = new Vector3(playerTopTransform.position.x, this.transform.position.y, 0.0f);
    }

    private void Jump()
    {
        if (jumpButtonPressed)
        {
            if (!hasJumping)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

                playerAnimator.SetBool("jumpLeft", true);
                playerBottomAnimator.SetBool("jumpLeft", true);
                
            }
            
        }

        jumpButtonPressed = false;
        


    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            hasJumping = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            hasJumping = false;
        }

        playerAnimator.SetBool("jumpLeft", false);
        playerBottomAnimator.SetBool("jumpLeft", false);
    }


}
