using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

// This script will handle controlling the main character
public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    
    
    // private float dirX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If the player hits the 'A' key
        if (Input.GetKey(KeyCode.A))
        {
            // Change the velocity of the player to move to the left by 5
            rb.velocity = new Vector2(-5, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        
        // If the player hits the 'D' key
        if (Input.GetKey(KeyCode.D))
        {
            // Change the velocity of the player to move to the right by 5
            rb.velocity = new Vector2(5, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }

        // If the player hits the 'Space' key
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Change the velocity of the player to move up by 10
            // This mimics a jumping motion
            rb.velocity = new Vector2(rb.velocity.x, 10f);
        }
    }
}
