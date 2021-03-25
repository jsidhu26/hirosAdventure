using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] private float leftCap; // Represents left waypoint
    [SerializeField] private float rightCap; // Represents right waypoint
    [SerializeField] private float movingSpeed = 10f; // Represents how fast the saw moves back and forth
    private Collider2D coll; // Used for detecting collisions with player
    private Rigidbody2D rb; // Used for saw movement
    private bool facingLeft = true; // Used for determining which way saw will move

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       // Check if saw is facing left
       if (facingLeft)
       {
           // Check to make sure saw has not exceeded waypoint
           if(transform.position.x > leftCap)
           {
               // Make sure saw is facing correct direction
               if (transform.localScale.x != 1)
               {
                   transform.localScale = new Vector3(1,1);
               }

               // Move to the left
               rb.velocity = new Vector2(-movingSpeed, transform.position.y);
           }
           else
           {
               // Waypoint exceeded, saw must turn around
               facingLeft = false;
           }
       }
       // Saw is facing right
       else
       {
           // Check to make sure saw has not exceeded waypoint
           if (transform.position.x < rightCap)
           {
               // Make sure saw is facing correct direction
               if (transform.localScale.x != -1)
               {
                   transform.localScale = new Vector3(-1,1);
               }

               // Move to the right
               rb.velocity = new Vector2(movingSpeed, transform.position.y);
           }
           else
           {
               // Waypoint exceeded, saw must turn around
               facingLeft = true;
           }
       }
    }
}
