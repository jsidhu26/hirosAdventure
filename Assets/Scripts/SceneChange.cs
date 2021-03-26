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
            PlayerPrefs.SetInt("orbsCollected", pc.getOrbsCollected());
            SceneManager.LoadScene(sceneName);
        }
    }
}
