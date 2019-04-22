using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementControlles : MonoBehaviour {


    [SerializeField]
    private float horizontalVelocityForce;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private int jumpAmounts;
    private int currentJumps;
    [SerializeField]
    private float superJumpForce;
    [SerializeField]
    private int superJumpAmount;


    private Vector2 PlayerInputVelocity { get; set; }
    private Rigidbody2D playerRigBdy;

    [SerializeField]
    private bool directionRight;


    private bool isPlayerOnJumpAbleSurface;
    [SerializeField]
    private Transform surfaceCheckerPos;
    [SerializeField]
    private LayerMask jumpeAbleSurfaceLayer;
    [SerializeField]
    private float surfaceCheckerRadius;

    private Animator playerAnimator;
    private SpriteRenderer playerSprite;

    private Vector3 SuperJumpStart;
    // Use this for initialization
    void Start ()
    {
        playerRigBdy = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
	}
	

    void Update()
    {


        flipSprite();


        if(isPlayerOnJumpAbleSurface)
        {
            currentJumps = jumpAmounts;
            playerAnimator.SetFloat("Jump", playerRigBdy.velocity.y);
        }

        if (transform.position.y >= SuperJumpStart.y +10f)
        {
            playerRigBdy.mass = 10f;
            playerRigBdy.gravityScale = 10f;
            Debug.Log("Stop SUper Jump");
        }
       
        if (Input.GetKeyDown(KeyCode.Space) && !Input.GetKey(KeyCode.LeftShift) && currentJumps > 0) 
        {
            playerRigBdy.velocity = new Vector2(playerRigBdy.velocity.x, playerRigBdy.velocity.y + jumpForce);
            playerAnimator.SetFloat("Jump", playerRigBdy.velocity.y);
            currentJumps--;

        }else if((Input.GetKeyDown(KeyCode.Space) && !Input.GetKey(KeyCode.LeftShift) && currentJumps == 0) && isPlayerOnJumpAbleSurface)
        {
            //Debug.Log("STD JUMP");
            playerRigBdy.velocity = new Vector2(playerRigBdy.velocity.x, playerRigBdy.velocity.y + jumpForce);
           // Debug.Log("VELOCITY Y " + playerRigBdy.velocity.y);
            playerAnimator.SetFloat("Jump", playerRigBdy.velocity.y);
        }

        // Super jump needs Tweeking --> to powerfull
        //if((Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Space)) && superJumpAmount > 0)
        //{
        //    // transform.position = new Vector2(transform.position.x, transform.position.y + Time.deltaTime * 100); 
        //    playerRigBdy.mass = 300;
        //    playerRigBdy.gravityScale = 0.5f;
        //    playerRigBdy.velocity = new Vector2(playerRigBdy.velocity.x, playerRigBdy.velocity.y + superJumpForce);
        //    Debug.Log("SUPER JUMP");
        //    superJumpAmount--;
        //    SuperJumpStart = transform.position;
        //}
    }

    void FixedUpdate()
    {
        isPlayerOnJumpAbleSurface = Physics2D.OverlapCircle(surfaceCheckerPos.position, surfaceCheckerRadius, jumpeAbleSurfaceLayer);
        BasicMove();
    }


    private void flipSprite()
    {
       
        if( PlayerInputVelocity.x > 0)
        {
            playerSprite.flipX = false;
        }
        else if( PlayerInputVelocity.x < 0)
        {
            playerSprite.flipX = true;
        }
    }


    private void floteStyle()
    {
        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        playerRigBdy.AddForce(movement * horizontalVelocityForce);

    }


    private void BasicMove()
    {
        PlayerInputVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        playerRigBdy.velocity = new Vector2(PlayerInputVelocity.x * horizontalVelocityForce, playerRigBdy.velocity.y);
        playerAnimator.SetFloat("Run", Mathf.Abs( PlayerInputVelocity.x));
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(surfaceCheckerPos.position, surfaceCheckerRadius);
    }

}
