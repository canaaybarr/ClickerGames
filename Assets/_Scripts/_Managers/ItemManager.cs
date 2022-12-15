using System;
using System.Collections.Generic;
using _Scripts.Character;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Scripts._Managers
{
    public class ItemManager : Singleton<ItemManager>
    {
        public int sceneObjecti;

        public int SceneObjectI
        {
            get => sceneObjecti;

            set
            {
                sceneObjecti = value;
                PlayerPrefs.SetInt("SceneObjindex", value);
            }
        }

        public List<GameObject> items = new List<GameObject>();
        public List<GameObject> sceneItems = new List<GameObject>();
        public List<String> sceneItemsName = new List<String>();
        public List<float> lenghtValue = new List<float>();
        public List<float> lenghtValueReset = new List<float>();
        public List<float> scaleValueLists = new List<float>();
        public List<float> scaleValueListsReset = new List<float>();


        public GameObject sceneItem;
        [SerializeField] private TMP_Text itemİncomeButton;
        [SerializeField] public TMP_Text itemMoneyText;
        [SerializeField] public TMP_Text itemNameText;
        [SerializeField] public TMP_Text nextItemNameText;


        [SerializeField] public float itemSellValue;

        #region ItemSellValues

        public float ItemSellValue
        {
            get { return itemSellValue; }
            set
            {
                PlayerPrefs.SetFloat("itemSellValue", value);
                EX_GameManager.Instance.SetMoney(value, itemMoneyText);
                itemSellValue = value;
            }
        }

        #endregion


        public Transform spawnPos;
        public Transform parentSpawn;

        private float _itemSpawnerTimer;

        public Button upgradeButton;

        private void Start()
        {
            for (int i = 0; i < lenghtValue.Count; i++)
            {
                lenghtValueReset.Add(lenghtValue[i]);
            }

            for (int i = 0; i < scaleValueLists.Count; i++)
            {
                scaleValueListsReset.Add(scaleValueLists[i]);
            }
        }

        private void Update()
        {
            if (SceneObjectI == items.Count - 1)
                upgradeButton.gameObject.SetActive(false);

            if (!sceneItem)
            {
                _itemSpawnerTimer += Time.deltaTime;
                if (_itemSpawnerTimer > 0.4f)
                {
                    _itemSpawnerTimer = 0;
                    ItemSpawner();
                }
            }
        }

        public void ItemUpgrade()
        {
            if (MoneyManager.Instance.MoneyValue >= ItemSellValue && SceneObjectI < items.Count)
            {
                SceneObjectI++;
                DestroyItem();
                ItemSpawner();
                PunchController.Instance.punchValue = sceneItem.GetComponentInChildren<HitObjectItems>().punch;
                PunchController.Instance.worldScale = sceneItem.GetComponentInChildren<HitObjectItems>().punch;
                MoneyManager.Instance.MoneyValue -= ItemSellValue;
                ItemSellValue = sceneItem.GetComponentInChildren<HitObjectItems>().itemValue;
                SceneObjectIndex();
                PlayerPrefs.SetFloat("PunchValue", PunchController.Instance.punchValue);
                ItemSellSave();
                TableChange.Instance.ChangeTable(SceneObjectI);
            }
        }

        public void CameraPos()
        {
            if (SceneObjectI >= 7)
            {
                CameraFollow.Instance.distance = 4.85f;
                CameraFollow.Instance.height = 2.55f;
            }
            else if (SceneObjectI < 7)
            {
                CameraFollow.Instance.distance = 4.8f;
                CameraFollow.Instance.height = 2.5f;
            }
        }

        public void DestroyItem()
        {
            if (SceneObjectI < items.Count)
            {
                sceneItems.RemoveAt(0);
                Destroy(sceneItem);
                sceneItem = null;
            }
        }

        public void ItemSpawner()
        {
            if (sceneItem == null && SceneObjectI - 1 < items.Count - 1)
            {
                sceneItem = Instantiate(items[SceneObjectI], spawnPos.position, Quaternion.identity, parentSpawn);
                UIChangeImage.Instance.NextSceneImage();
                UIChangeImage.Instance.SceneImage();
                sceneItems.Add(items[SceneObjectI]);
                EX_TimeManager.Instance.transform.DOMoveX(0, 0.5f)
                    .OnComplete(() => { PunchController.Instance.punchBool = true; });
                SceneObjectIndex();
                itemNameText.text = sceneItemsName[SceneObjectI];
                if (SceneObjectI + 1 != sceneItemsName.Count)
                    nextItemNameText.text = sceneItemsName[SceneObjectI + 1];
            }
        }

        public void SceneObjectIndex()
        {
            itemİncomeButton.text = "" + sceneItem.GetComponentInChildren<HitObjectItems>().punch;
        }


        public void SaveItemManager()
        {
            SceneObjectI = PlayerPrefs.GetInt("SceneObjindex");
            ItemSellValue = PlayerPrefs.GetFloat("itemSellValue");
        }

        public void ItemSellSave()
        {
            PlayerPrefs.SetFloat("itemSellValue", ItemSellValue);
        }

        public void LenghtValueReset()
        {
            for (int i = 0; i < lenghtValue.Count; i++)
            {
                lenghtValue[i] = lenghtValueReset[i];
            }

            for (int i = 0; i < scaleValueLists.Count; i++)
            {
                scaleValueLists[i] = scaleValueListsReset[i];
            }
        }
    }
}