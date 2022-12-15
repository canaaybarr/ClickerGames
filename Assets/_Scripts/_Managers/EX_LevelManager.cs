using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class EX_LevelManager : LevelManager<EX_LevelManager,EX_Level>
{
    public int currentLevelNumber;

    private void Awake()
    {

        currentLevelNumber = PlayerPrefs.GetInt("level");
        if (currentLevelNumber >= LevelCount())
        {
            LoadLevel(Random.Range(0,LevelCount())).InitiliazeLevel();
        }
        else
        {
            LoadLevel(currentLevelNumber).InitiliazeLevel();
        }
        Time.timeScale = 1f;
    }

    public void LevelCompleted()
    {
        
    }
    
    public override EX_Level LoadLevel(int index)
    {
        return base.LoadLevel(index);
    }
}
