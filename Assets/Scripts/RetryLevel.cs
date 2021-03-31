using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadScene(int level)
    {
        // Because the game is in Pause mode,
        // we must reset the timer
        GameObject player = GameObject.FindWithTag("Player");
        //player.SendMessage("Resume");

        // Reload the level
        Application.LoadLevel(level);
    }
}
