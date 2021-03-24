using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

// This script will handle controlling the main character
public class PlayerController : MonoBehaviour
{
    [SerializeField]private Rigidbody2D rb;
    [SerializeField]private Animator anim;
    
    private float hDirection;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        hDirection = CrossPlatformInputManager.GetAxis("Horizontal");

        // If the player hits the 'A' key
        if (hDirection < 0)
        {
            // Change the velocity of the player to move to the left by 5
            rb.velocity = new Vector2(-5, rb.velocity.y);
            // Flip the sprite so its facing the left
            transform.localScale = new Vector2(-1f, 1f);
            // Start the running animation
            anim.SetBool("running", true);
        }
        
        // If the player hits the 'D' key
        else if (hDirection > 0)
        {
            // Change the velocity of the player to move to the right by 5
            rb.velocity = new Vector2(5, rb.velocity.y);
            // Flip the sprite so its facing the right
            transform.localScale = new Vector2(1f, 1f);
            // Start the running animation
            anim.SetBool("running", true);
        }

        // If no key is pressed
        else{
            // Stop running animation
            anim.SetBool("running", false);
        }

        // If the player hits the 'Space' key
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            // Change the velocity of the player to move up by 10
            // This mimics a jumping motion
            rb.velocity = new Vector2(rb.velocity.x, 10f);
        }
    }
}
