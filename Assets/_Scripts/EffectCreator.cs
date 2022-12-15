using System;
using System.Collections.Generic;
using _Scripts._Managers;
using _Scripts.Character;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts
{
    public class EffectCreator : Singleton<EffectCreator>
    {
        [Header("Color Effect")]
        public List<Color> colors = new List<Color>();

        [Header("Punch Effect")]
        public RectTransform canvasParent;
        public GameObject effectPunchObj;
        [SerializeField]private GameObject punchEffect;
        [SerializeField]private Transform punchEffectTransform;
        [SerializeField]private Material punchEffectMaterial;
        
        public List<Vector3> genaralPos = new List<Vector3>();

        [SerializeField] private GameObject addCoinEffect;
        public Transform addCoinEffectTransform;


      

        public void ColorEffect()
        {
            GameObject bla = Instantiate(punchEffect,punchEffectTransform.position,Quaternion.Euler(new Vector3(0,90,0)),punchEffectTransform);
            punchEffectMaterial.color = colors[ItemManager.Instance.SceneObjectI];
            Destroy(bla,3.5f);
        }

        public void AddCoinEffect(float addmoney)
        {
            if (PowerManager.Instance.TotalPunchValue > 0)
            {
                var obj = Instantiate(addCoinEffect,addCoinEffectTransform.position,Quaternion.identity,addCoinEffectTransform.transform);
                obj.gameObject.GetComponent<TMP_Text>().text ="+" + addmoney;
                MoneyManager.Instance.MoneyValue = MoneyManager.Instance.MoneyValue;
                Destroy(obj,1.5f);
            }
        }

        private List<Vector3> generalPos = new List<Vector3>();

        Vector3 Pos()
        {
            Vector3 randompos;
            var nXpos = new Vector3(Random.Range(-110f, -60f), Random.Range(-17f, 80f), 0);
            var pXpos = new Vector3(Random.Range(60f, 110f), Random.Range(-17f, 80f), 0);
            generalPos.Add(nXpos);
            generalPos.Add(pXpos);
            randompos = generalPos[Random.Range(0,2)];
            generalPos.Clear();
            return randompos;
        }

        public void EffectInstantie()
        {
            var obj = Instantiate(effectPunchObj,Vector3.zero, Quaternion.identity,canvasParent);
            obj.transform.localPosition = Pos();
            
            obj.gameObject.GetComponentInChildren<TMP_Text>().text = "+" + PunchController.Instance.punchValue;
            Destroy(obj,1.5f);
        }
        
        
    }
}
