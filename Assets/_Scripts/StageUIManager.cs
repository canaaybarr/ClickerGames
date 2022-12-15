using System.Collections;
using System.Collections.Generic;
using _Scripts._Managers;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageUIManager : Singleton<StageUIManager>
{

    private int stageCount;

    public GameObject stageIndicatorPrefab;

    public Sprite completedSprite, currentSprite;

    private int stageNumber;
    
    private List<GameObject> stageInds = new List<GameObject>();

    public void StartStageUIManager()
    {
        stageCount = EX_GameManager.Instance.stageCount;

        float spawnPosX = 0;
        
        for (int i = 0; i < stageCount; i++)
        {
            GameObject stageInd = Instantiate(stageIndicatorPrefab);
            
            stageInd.transform.SetParent(transform, false);

            RectTransform tr = stageInd.GetComponent<RectTransform>();
            
            tr.anchoredPosition = new Vector2(tr.anchoredPosition.x + spawnPosX, tr.anchoredPosition.y);

            stageInd.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();

            spawnPosX += 30;
            
            stageInds.Add(stageInd);
        }

        RectTransform rectTransform = GetComponent<RectTransform>();
        
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x - (stageCount - 1) * 15, rectTransform.anchoredPosition.y);

        stageInds[0].GetComponent<Image>().sprite = currentSprite;

        //transform.position = new Vector3(transform.position.x - (stageCount - 1) * 15, transform.position.y, transform.position.z);
    }

    public void NextStage()
    {
        stageInds[stageNumber].GetComponent<Image>().sprite = completedSprite;
        
        stageInds[stageNumber + 1].GetComponent<Image>().sprite = currentSprite;

        stageNumber++;
    }

    public void Finish()
    {
        stageInds[stageInds.Count - 1].GetComponent<Image>().sprite = completedSprite;
    }

}
