using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script is used for displaying the results to the user
public class DisplayResults : MonoBehaviour
{
    [SerializeField] private Text title;
    [SerializeField] private Text message;
    [SerializeField] private Text orbsText; //Displays number of orbs collected to the user
    private int orbsCollected;
    
    [SerializeField] private Text timeText; // Displays time taken to the user
    private string timeTaken;   // Stores time from PlayerPrefs API

    // Start is called before the first frame update
    void Start()
    {
        // Use PlayerPrefs API to get the number of orbs collected from the previous scene
        orbsCollected = PlayerPrefs.GetInt("orbsCollected");
        timeTaken = PlayerPrefs.GetString("timeTaken");

        if (orbsCollected <=7)
        {
            title.text = "LEVEL FAILED";
            message.text = "Not enough orbs were collected.";
        }
    }

    // Update is called once per frame
    void Update()
    {
        orbsText.text = "x " + orbsCollected.ToString();
        timeText.text = "Time Taken: " + timeTaken;
    }
}
