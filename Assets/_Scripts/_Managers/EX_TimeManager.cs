using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EX_TimeManager : Singleton<EX_TimeManager>
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
