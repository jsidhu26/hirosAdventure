using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This controller is in charge of following the player
public class CameraController : MonoBehaviour
{

    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // When each frame updates, the camera will find the player's position and "lock onto" the player.
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
