﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadScene(int level)
    {
        Application.LoadLevel(level);
    }
}
