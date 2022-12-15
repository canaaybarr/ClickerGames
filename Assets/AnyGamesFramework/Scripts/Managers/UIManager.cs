using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts._Managers;
using DG.Tweening;
using MoreMountains.NiceVibrations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public abstract class UIManager<T> : Singleton<T> where T : MonoBehaviour
{
    [Header("Revive Panel")]
    public GameObject revivePanel;
    public Image fillbar;
    [Header("GameOver Panel")]
    public GameObject gameOverPanel;
    [Header("LevelComplete")]
    public GameObject levelCompletePanel;
    [Header("PlayButton")] 
    public GameObject playButton;
    [Header("Stars")]
    public GameObject[] stars;
    [Header("StarParticles")]
    public GameObject[] starParticles;
    [Header("LevelTexts")]
    public TextMeshProUGUI[] levelTxts;
    [Header("Confetti")]
    public GameObject confetti;
    [Header("ProgressionSlider")]
    public Slider progressionSlider;

    int starValue;

    public GameObject nextLevelButton;

    public GameObject playUi;

    //ProgressionBar
    float firstDistance;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextLevelButtonForLevelDesign();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreviousLevelButtonForLevelDesign();
        }
    }


    public void ShowLevelCompletePanel(int value)
    {
        starValue = value;
       StartCoroutine(SetStarSize());
       
       playUi.SetActive(false);
       
        levelCompletePanel.SetActive(true);
        EX_TimeManager.Instance.transform.DOMoveX(0, 1).OnComplete(() =>
        {
            nextLevelButton.SetActive(true);
        });
    }

    public void ShowRevivePanel()
    {
        fillbar.DOFillAmount(0, 0f);
        revivePanel.SetActive(true);
        fillbar.DOFillAmount(1, 3f).SetId(1).OnComplete(() =>
        {
            HideRevivePanel();
            ShowGameOverPanel();
        });

    }

    public void PreviousLevelButtonForLevelDesign()
    {
        if (EX_LevelManager.Instance.currentLevelNumber - 1 >= 0)
        {
            PlayerPrefs.SetInt("level", EX_LevelManager.Instance.currentLevelNumber - 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public void NextLevelButtonForLevelDesign()
    {
        if (EX_LevelManager.Instance.currentLevelNumber + 1 < EX_LevelManager.Instance.LevelCount())
        {
            PlayerPrefs.SetInt("level", EX_LevelManager.Instance.currentLevelNumber + 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void HideRevivePanel()
    {
        revivePanel.SetActive(false);
    }

    public void ShowGameOverPanel()
    {
        playUi.SetActive(false);

        
        gameOverPanel.SetActive(true);
    }

    public void SkipButton()
    {
        
        DOTween.Kill(1);
        HideRevivePanel();
        ShowGameOverPanel();
    }
    
    public void HomeButton()
    {
        SceneManager.LoadScene(0);
    }

    public virtual void NextLevelButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public virtual void ReviveButton()
    {
        //TODO:ShowAd
        DOTween.Kill(1);
        HideRevivePanel();
    }

    public virtual void ReplayButton()
    {
        if (EX_GameManager.Instance.state == GameManager<EX_GameManager>.STATE.Win)
        {
            PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") - 1);
        }
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public virtual void PlayButton()
    {
        Time.timeScale = 1;
        playButton.SetActive(false);
    }
    IEnumerator  SetStarSize()
    {
        yield return new WaitForSeconds(0.3f);

        for (int i = 0; i < starValue; i++)
        {
            stars[i].gameObject.SetActive(true);
            stars[i].gameObject.GetComponent<Image>().DOFade(0, 0);
            stars[i].gameObject.GetComponent<Image>().DOFade(1, 0.2f);
            stars[i].transform.DOScale(1, 0.4f).SetEase(Ease.InQuad).OnComplete(()=> {
                starParticles[i].SetActive(true);
                
                MMVibrationManager.Haptic(HapticTypes.RigidImpact);
            });
            yield return new WaitForSeconds(0.2f);
        }
    }
    public void SetLevelNumber()
    {
        int levelcount = EX_LevelManager.Instance.currentLevelNumber + 1;
        for (int i = 0; i < levelTxts.Length; i++)
        {
            levelTxts[i].text = "LEVEL " + levelcount;
        }
    }

    public void ConfettiEnable()
    {
        confetti.SetActive(true);
    }
   public void UpdateProgressBar(float player,float finish)
    {

        progressionSlider.value = player / finish;

    }
}