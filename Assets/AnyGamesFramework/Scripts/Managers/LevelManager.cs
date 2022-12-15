using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class LevelManager<TBase,TLevel> : Singleton<TBase> where TBase : MonoBehaviour where TLevel : Level
{
    [SerializeField]
    private List<TLevel> levels;
    
    private TLevel _currentLevel;
    private int _currentLevelIndex;

    public virtual TLevel LoadLevel(int index)
    {
        if (index >= 0 && index < levels.Count)
        {
            _currentLevelIndex = index;
            _currentLevel = levels[_currentLevelIndex];
            return _currentLevel;
        }
        // else
        Debug.LogError("Invalid Level Index");
        return null;
    }

    public int LevelCount()
    {
        return levels.Count;
    }

    public TLevel LoadRandomLevel()
    {
        return LoadLevel(Random.Range(0, levels.Count));
    }

    public TLevel RestartLevel()
    {
        return LoadLevel(_currentLevelIndex);
    }

    public TLevel NextLevel()
    {
        if (_currentLevelIndex < levels.Count - 1)
        {
            return LoadLevel(_currentLevelIndex + 1);
        }
        // else
        Debug.LogWarning("End Of Level Count");
        return LoadLevel(0);
    }

    public TLevel CurrentLevel()
    {
        if (_currentLevel)
            return _currentLevel;
        // else
        Debug.LogError("Level is null");
        return null;
    }

    public int CurrentLevelIndex ()
    {
        return _currentLevelIndex;
    }
}