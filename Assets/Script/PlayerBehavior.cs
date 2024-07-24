using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public int speedMovement = 5;
    private Rigidbody rb;
    public Animator playerAnimator;
    public Animator playerBottomAnimator;
    public SpriteRenderer playerSprite;
    public SpriteRenderer playerBottomSprite;
    private float horizontalInput;
    private float verticalInput;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerAnimator = GetComponentInChildren<Animator>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0.0f);
        rb.velocity = movement * speedMovement * Time.deltaTime;

        playerAnimator.SetFloat("speed", Mathf.Abs(horizontalInput));

        CheckPlayerSpriteHorizontalOrientation();

        CheckPlayerUpOrientation();

        CheckPlayerDownOrientation();
    }

    private void CheckPlayerSpriteHorizontalOrientation()
    {
        if (horizontalInput > 0.01f)
        {
            playerAnimator.SetBool("isIdling", false);
            playerSprite.flipX = true;

            playerBottomAnimator.SetBool("isMoving", true);
            playerBottomSprite.flipX = true;

        }
        else if (horizontalInput < -0.01f)
        {
            playerAnimator.SetBool("isIdling", false);
            playerSprite.flipX = false;

            playerBottomAnimator.SetBool("isMoving", true);
            playerBottomSprite.flipX = false;
        }
        else
        {
            playerAnimator.SetBool("isIdling", true);

            playerBottomAnimator.SetBool("isIdling", true);
            playerBottomAnimator.SetBool("isMoving", false);
        }
    }

    private void CheckPlayerUpOrientation()
    {
        if (verticalInput > 0.01f)
        {
            playerAnimator.SetBool("isFaceUp", true);

            playerBottomAnimator.SetBool("isFaceUp", true);
        }
        else
        {
            playerAnimator.SetBool("isFaceUp", false);

            playerBottomAnimator.SetBool("isFaceUp", false);
        }
    }

    private void CheckPlayerDownOrientation()
    {
        if (verticalInput < -0.01f)
        {
            playerAnimator.SetBool("isFaceDown", true);

            playerBottomAnimator.SetBool("isFaceDown", true);
        }
        else
        {
            playerAnimator.SetBool("isFaceDown", false);

            playerBottomAnimator.SetBool("isFaceDown", false);
        }
    }
}
