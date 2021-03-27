using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script loads the results page view/scene
public class SceneChange : MonoBehaviour
{
    [SerializeField] private string sceneName;  // Used for loading scene

    private PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Inform player that level is completed (for timing)
            GameObject.Find("Player").SendMessage("flagCollected");
            // Remember number of orbs collected
            PlayerPrefs.SetInt("orbsCollected", pc.getOrbsCollected());
            // Remember time taken
            PlayerPrefs.SetString("timeTaken", pc.getTimeTaken());
            // Load next scene
            SceneManager.LoadScene(sceneName);
        }
    }
}
