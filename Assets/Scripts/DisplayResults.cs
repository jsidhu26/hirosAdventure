using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script is used for displaying the results to the user
public class DisplayResults : MonoBehaviour
{
    [SerializeField] private Text orbsText; //Displays number of orbs collected to the user
    private int orbsCollected;

    // Start is called before the first frame update
    void Start()
    {
        // Use PlayerPrefs API to get the number of orbs collected from the previous scene
        orbsCollected = PlayerPrefs.GetInt("orbsCollected");
    }

    // Update is called once per frame
    void Update()
    {
        orbsText.text = "x " + orbsCollected.ToString();
    }
}
