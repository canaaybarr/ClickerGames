using System;
using _Scripts.Character;
using DG.Tweening;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts._Managers
{
    public class PowerManager : Singleton<PowerManager>
    {
        
        public float totalPunchValue;
        [SerializeField] private TMP_Text punchText;
        [SerializeField] private TMP_Text punchChangeMoneyText;
        public Transform player;
        public float TotalPunchValue
        {
            get
            {
                return totalPunchValue;
            }
            set
            {
                PlayerPrefs.SetFloat("TotalPunchValue",value);
                totalPunchValue = value;
            }
        }
        
        public void MoneyAddPunch()
        {
            EX_GameManager.Instance.highScoreBool = false;
            PunchController.Instance.ScoreUpdate();
            MoneyManager.Instance.MoneyValue += TotalPunchValue;
            //HighScoreMovement.Instance.distance.y -= TotalPunchValue;
            EffectCreator.Instance.AddCoinEffect(TotalPunchValue);
            PunchController.Instance.curvedWorldController.bendCurvatureSize = 2f;
            PunchController.Instance.curvedWorldController.bendCurvatureOffset = 2;
            TotalPunchValue = 0;
            HighScoreMovement.Instance.SetDistance(PunchController.Instance._totalScore - 1.5f);
            PunchController.Instance._totalScore = 1.5f;
            // PunchController.Instance.map.transform.DOScale((new Vector3(1, 1, 1)), 0.5f);
            PunchController.Instance.blendShapeRendererValue = 0;
            PunchController.Instance.blendShapeRenderer.SetBlendShapeWeight(0, PunchController.Instance.blendShapeRendererValue);
            PlayerPrefs.SetFloat("BlendShapeRendererValue",PunchController.Instance.blendShapeRendererValue);
            PunchController.Instance.scaleSize = 10;
            player.DOScale((new Vector3(10, 10, 10)), 0.4f);
            PlayerPrefs.SetFloat("ScaleSize", PunchController.Instance.scaleSize);
            PunchController.Instance.TotalPunchSave();
            PunchTextSave();
            ItemManager.Instance.LenghtValueReset();
            PunchController.Instance.scoreTransformPos.DOMoveY(PunchController.Instance._totalScore, 0f).OnComplete(() => PunchController.Instance.ScoreUpdate());
        }
        

        public void PunchTextSave()
        {
            punchText.text = "" + TotalPunchValue;
            EX_GameManager.Instance.SetMoney(TotalPunchValue,punchChangeMoneyText);
            punchText.GetComponentInParent<EnableExtension>().CallTween();
            SavePunchValue();
        }
        
        public void SavePunchValue()
        {
            TotalPunchValue = PlayerPrefs.GetFloat("TotalPunchValue");
        }
    }
}
