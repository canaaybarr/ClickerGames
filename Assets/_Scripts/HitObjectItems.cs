using System;
using System.Collections.Generic;
using _Scripts._Managers;
using _Scripts.Character;
using UnityEngine;
using DG.Tweening;

namespace _Scripts
{
    public class HitObjectItems : MonoBehaviour
    {
        public float hp;
        public float backUpHp;
        public float itemValue;
        public int backHp;
        public int punch;
        private bool once;
        
        public GameObject endEffect;
        public EnableExtension enableExtension;
        
        public List<GameObject> brokenLevelObjects = new List<GameObject>();
        private void Awake()
        {
            backUpHp = hp;
        }

        public float Hp
        {
            get => hp;
            set
            {
                if (value <= 0)
                {
                   
                }
                hp = value;
                if (ItemManager.Instance.SceneObjectI > 0 && backUpHp != 5)
                {
                    Broken(value);
                }
                else if (ItemManager.Instance.SceneObjectI == 0)
                {
                    Broken(value);
                }
            }
        }

        public bool NoReturn { get; } = true;

        void Broken(float shp)
        {           
            if(once)
                return;
            
            enableExtension.CallTween();
            var brokenLevel = backUpHp / 3;
            if (shp <= brokenLevel * 3  && shp >= brokenLevel * 2)
            {
                PunchController.Instance.playerTextureMaterial.mainTexture = PunchController.Instance.playerTextures[2];
            }
            if (shp <= brokenLevel * 2  && shp >= brokenLevel * 1)
            {
                brokenLevelObjects[0].SetActive(false);
                 brokenLevelObjects[1].transform.position = brokenLevelObjects[0].transform.position;
                brokenLevelObjects[1].SetActive(true);
            }
            else if (shp <= brokenLevel * 0)
            {
                once = true;
                
                brokenLevelObjects[1].SetActive(false);
                 brokenLevelObjects[2].transform.position = brokenLevelObjects[1].transform.position;
                brokenLevelObjects[2].SetActive(true);
                
                Vector3 explosionPos =  brokenLevelObjects[2].transform.position;
                Collider[] colliders = Physics.OverlapSphere(explosionPos, 20);
                foreach (Collider hit in colliders)
                {
                    Rigidbody rb = hit.GetComponent<Rigidbody>();

                    if (rb != null)
                        rb.AddExplosionForce(20, explosionPos,5, 3f,ForceMode.Impulse);
                }
                
                PunchController.Instance.playerTextureMaterial.mainTexture = PunchController.Instance.playerTextures[3];
                PunchController.Instance.punchBool = false;
                EX_GameManager.Instance.MidVib();
                print("a");
                Instantiate(endEffect, brokenLevelObjects[2].transform.position, Quaternion.identity, EX_UIManager.Instance.gameObject.transform);
                EX_TimeManager.Instance.transform.DOMoveX(0, 0.5f).OnComplete(() =>
                {
                    PunchController.Instance.playerTextureMaterial.mainTexture = PunchController.Instance.playerTextures[0];
                    Dead();
                    EX_TimeManager.Instance.transform.DOMoveX(0, 0.3f).OnComplete(() =>
                    {
                       
                    });
                });

            }
            #region Uzatılabilir Kırılma Derecesi
            // else if (shp <= brokenLevel * 0)
            // {
            //     Debug.Log("EffectInstantie and Player Dead");
            //     Debug.Log(shp);
            //     brokenLevelObjects[2].SetActive(false);
            //     brokenLevelObjects[3].transform.position = brokenLevelObjects[2].transform.position;
            //     brokenLevelObjects[3].SetActive(true);
            // }
            #endregion
        }

        private bool stop = true;

        void Dead()
        {
            if (stop)
            {
                stop = false;
                gameObject.layer = 6;
                EX_TimeManager.Instance.transform.DOMoveX(0, 0.2f).OnComplete(() =>
                {
                    ItemManager.Instance.sceneItem = null;
                    EX_TimeManager.Instance.transform.DOMoveX(0, 0.01f).OnComplete(() =>
                    {
                        gameObject.GetComponentInChildren<HitObjectItems>().enabled = false;
                        Destroy(gameObject, 1f);
                        ItemManager.Instance.sceneItems.RemoveAt(0);
                        PunchController.Instance.punchBool = false;
                        ItemManager.Instance.ItemSpawner();
                    });

                });
            }
        }
    }
}
