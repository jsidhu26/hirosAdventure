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
    [SerializeField] private AudioSource jump; // Sound played when playaer jumps
    [SerializeField] private AudioSource hurt; // Sound played when player is hurt
    [SerializeField] private AudioSource orbCollect; // Sound played when player collects orb
    [SerializeField] private AudioSource levelComplete; // Sound played when player completes level

    // For keeping track of whether game is paused or not
    public static bool isPaused = false;
    public GameObject pauseMenuUI;

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
            // Play jump noise
            Jump();
        }

        // If the player hits the 'pause' key and the player is touching the ground
        if (CrossPlatformInputManager.GetButtonDown("Pause"))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        isPaused = true;
    }

    // Switches the animation of the player based on the state
    private void AnimationState()
    {
        if (state == State.jumping)
        {
            // Player is jumping

            // If player's vertical velocity is less than .1f, then they are on their way down from jump
            if (rb.velocity.y < .1f)
            {
                // Player state is falling
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
            // Player is hurt
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
        // If the item is a collectable
        if(collision.tag == "Collectable")
        {
            // Destroy the item upon contact
            Destroy(collision.gameObject);
            // Increment the number of orbs collected
            numOrbs += 1;
            // Display updated orbs collected
            orbsText.text = "x " + numOrbs.ToString(); 
            // Play orb collection sound
            OrbCollect();
        }
    }

    // Detects whether player is colliding with obstacles
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Obstacle")
        {
            // Collision with obstacle has been detected
            state = State.hurt; // Player is hurt
            if (numOrbs > 0)
            {
                numOrbs -= 1; // Player loses a collected orb
                orbsText.text = "x " + numOrbs.ToString(); // Display updated orbs collected

            }
            else
            {
                // Player already has 0 orbs
                numOrbs = 0;
                orbsText.text = "x " + numOrbs.ToString(); // Display updated orbs collected
            }

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

            // Play hurt noise
            Hurt();

        }
    }

    // Method for triggering jump noise
    private void Jump()
    {
        jump.Play();
    }

    // Method for triggering hurt noise
    private void Hurt()
    {
        hurt.Play();
    }

    // Method for triggering orb collection noise
    private void OrbCollect()
    {
        orbCollect.Play();
    }

    // Method for triggering level completion noise
    private void LevelComplete()
    {
        levelComplete.Play();
    }

    public int getOrbsCollected()
    {
        return numOrbs;
    }

}
