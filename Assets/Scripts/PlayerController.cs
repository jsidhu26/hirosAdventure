using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

// This script will handle controlling the main character
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb; // Provides Physics elements like gravity, mass
    private Animator anim; // Used for triggering animations
    private Collider2D coll; // Used for collision detection
    [SerializeField] private LayerMask ground; // Used for collision detection
    [SerializeField] private float speed = 5f; // Used for configuring player speed
    [SerializeField] private float jumpForce = 10f; // Used for configuring player jump force
    [SerializeField] private float hurtForce = 7f; // Used for player displacement after colliding into obstacles
    private float hDirection; // Used for getting input from player
    [SerializeField] private int numOrbs = 0; // Keeps track of orbs collected
    [SerializeField] private Text orbsText; //Displays number of orbs collected to the user

    // Used for determing the state of the player
    private enum State { idle, running, jumping, falling, hurt }
    private State state = State.idle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        orbsText.text = "x 0"; 
    }

    // Update is called once per frame
    void Update()
    {
        if (state != State.hurt)
        {
            Movement();
        }
        AnimationState();
        anim.SetInteger("state", (int)state);
    }

    // Manages the input from the player, translates it to movement
    private void Movement()
    {
        // Get the input from the player
        hDirection = CrossPlatformInputManager.GetAxis("Horizontal");

        // If the player hits the 'left arrow' key
        if (hDirection < 0)
        {
            // Change the velocity of the player to move to the left by configured speed variable
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            // Flip the sprite so its facing the left
            transform.localScale = new Vector2(-1f, 1f);
        }
        
        // If the player hits the 'right arrow' key
        else if (hDirection > 0)
        {
            // Change the velocity of the player to move to the right by configured speed variable
            rb.velocity = new Vector2(speed, rb.velocity.y);
            // Flip the sprite so its facing the right
            transform.localScale = new Vector2(1f, 1f);
        }

        // If the player hits the 'up arrow' key and the player is touching the ground
        if (CrossPlatformInputManager.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            // Change the velocity of the player to move up by 10
            // This mimics a jumping motion
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            // Set state to jumping
            state = State.jumping;
        }
    }

    // Switches the animation of the player based on the state
    private void AnimationState()
    {
        if (state == State.jumping)
        {
            // Player is jumping

            // If player's vertical velocity is less than .1f
            if (rb.velocity.y < .1f)
            {
                // Player is on their way down from jump
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            // Player is falling
            if(coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if (Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            // Player is moving
            state = State.running;

        }
        else 
        {
            // Player is not moving
            state = State.idle;
        }

    }

    // Detects whether player is colliding with collectable items (like orbs)
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.tag == "Collectable")
        {
            Destroy(collision.gameObject);
            numOrbs += 1;
            orbsText.text = "x " + numOrbs.ToString(); 
        }
    }

    // Detects whether player is colliding with obstacles
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Obstacle")
        {
            // Collision with obstacle has been detected
            state = State.hurt;
            // Check if collision was on player's right side
            if (other.gameObject.transform.position.x > transform.position.x)
            {
                // Obstacle was on player's right side
                // Player must fall leftwards
                rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
            }
            else 
            {
                // Obstacle was on player's left side
                // Player must fall rightwards
                rb.velocity = new Vector2(hurtForce, rb.velocity.y);
            }

        }
    }
}
