using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using _Scripts._Managers;
using AmazingAssets.CurvedWorld;
using DG.Tweening;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Scripts.Character
{
    public class PunchController : Singleton<PunchController>
    {
        #region Variables

        public List<Texture> playerTextures = new List<Texture>();
        public Material playerTextureMaterial;

        [SerializeField] private Animator anim;
        [SerializeField] private float punchSpeed;
        [SerializeField] private bool touncDelay;
        public bool gameStart = false;
        public bool punchBool = false;
        private static readonly int Punch = Animator.StringToHash("Punch");
        private static readonly int Kick = Animator.StringToHash("Kick");

        public float scaleSize;
        public float punchValue;
        public float worldScale = 1;


        #region Score

        public TextMeshProUGUI scoreText;
        public TMP_Text highScoreText;
        public GameObject fillingFtText;
        public GameObject highScoreObj;
        public float _highScoreValue;
        public float _scoreValue;
        public Transform scoreTransformPos;
        public Transform posFt;
        public float _totalScore = 1.5f;

        #endregion


        public SkinnedMeshRenderer blendShapeRenderer;
        public float blendShapeRendererValue;
        public GameObject map;
        public CurvedWorldController curvedWorldController;
        public Image punchImage;

        #endregion

        public Transform aim;

        private void Start()
        {
            playerTextureMaterial.mainTexture = playerTextures[0];
        }

        public void ScoreUpdate()
        {
            HighScoreMovement.Instance.SetUpdate();
            _scoreValue = scoreTransformPos.transform.localPosition.y;
            var round = Math.Round(_scoreValue, 2);
            scoreText.text = ((((scaleSize * 1.6f) * 10)) * 0.0328f).ToString("F") + "ft";
            // if (_scoreValue >= _highScoreValue)
            // {
            //     _highScoreValue = _scoreValue;
            //     var highround = Math.Round(_highScoreValue, 2);
            //     highScoreText.text = highround + "ft";
            //     scoreText.enabled = false;
            // }
            // else
            // {
            //     scoreText.enabled = true;
            // }
        }

        public void StartGame()
        {
            gameStart = true;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartGame();
                SpeedNegative();
                touncDelay = true;
                PunchStar();
            }

            if (Input.GetMouseButtonUp(0))
            {
                touncDelay = false;
                punchSpeed = 3f;
            }

            aim.transform.localPosition = Vector3.Lerp(aim.transform.localPosition,
                new Vector3(scaleSize * 3, aim.transform.localPosition.y, aim.transform.localPosition.z),
                Time.deltaTime * 10);
        }

        void SpeedNegative()
        {
            var speed = punchSpeed;
            punchSpeed = 0;
        }

        IEnumerator ForeverPunch()
        {
            for (;;)
            {
                // ItemManager.Instance.CameraPos();
                if (gameStart && !touncDelay && punchBool)
                {  
                    if(!DOTween.IsTweening("PunchUiKill"))
                    punchImage.DOFillAmount(1, punchSpeed).SetEase(Ease.Linear).SetId("PunchUi").OnComplete(() =>
                    {
                        punchImage.fillAmount = 0;
                        if (scaleSize < 30)
                        {
                            anim.SetTrigger(Punch);
                        }
                        else if (scaleSize >= 30)
                        {
                            anim.SetTrigger(Kick);
                        }
                    });
                }
                else
                {
                    punchImage.fillAmount = 0;
                }

                yield return new WaitForSeconds(punchSpeed);
            }
            // ReSharper disable once IteratorNeverReturns
        }

        public void PunchStar()
        {
            ItemManager.Instance.CameraPos();
            DOTween.Kill("PunchUi");
            punchImage.DOFillAmount(0, 0.1f).SetEase(Ease.Linear).SetId("PunchUiKill");
            if (gameStart && punchBool)
            {
                if (scaleSize < 30f)
                {
                    anim.SetTrigger(Punch);
                }
                else if (scaleSize >= 30f)
                {
                    anim.SetTrigger(Kick);
                }
            }
        }

        public void BlendShapeSave()
        {
            blendShapeRendererValue = PlayerPrefs.GetFloat("BlendShapeRendererValue");
            blendShapeRenderer.SetBlendShapeWeight(0, blendShapeRendererValue);
        }

        public void PunchMain()
        {
            if (PowerManager.Instance.TotalPunchValue >= 2000f)
            {
                curvedWorldController.bendCurvatureSize += 0.3f;
                curvedWorldController.bendCurvatureOffset = 0;
            }

            #region Scale

            #region ValueAdd

            if (ItemManager.Instance.sceneItem == null)
                ItemManager.Instance.sceneItem = ItemManager.Instance.parentSpawn
                    .GetComponentInChildren<HitObjectItems>().gameObject;

            ItemManager.Instance.sceneItem.GetComponentInChildren<HitObjectItems>().Hp -= punchValue;
            if (blendShapeRendererValue <= 200)
            {
                blendShapeRendererValue++;
                blendShapeRenderer.SetBlendShapeWeight(0, blendShapeRendererValue);
                PlayerPrefs.SetFloat("BlendShapeRendererValue", blendShapeRendererValue);
            }

            PowerManager.Instance.TotalPunchValue += punchValue;
            PowerManager.Instance.PunchTextSave();
            EffectCreator.Instance.EffectInstantie();

            #endregion

            ItemManager.Instance.scaleValueLists[ItemManager.Instance.SceneObjectI] +=
                (ItemManager.Instance.scaleValueLists[ItemManager.Instance.SceneObjectI] / 5);
            // Debug.Log(scaleSize / ItemManager.Instance.scaleValueLists[ItemManager.Instance.SceneObjectI]);
            // scaleSize +=  scaleSize / ItemManager.Instance.scaleValueLists[ItemManager.Instance.SceneObjectI];
            scaleSize += punchValue / EX_GameManager.Instance.scaleAmount;

            transform.DOScale(new Vector3(scaleSize, scaleSize, scaleSize), 0.15f);
            PlayerPrefs.SetFloat("ScaleSize", scaleSize);
            EffectCreator.Instance.ColorEffect();
            EX_GameManager.Instance.SlowVib();

            #endregion

            TotalPunchSave();
            var itemV = ItemManager.Instance.lenghtValue[ItemManager.Instance.SceneObjectI];
            itemV = itemV - itemV / 9;
            ItemManager.Instance.lenghtValue[ItemManager.Instance.SceneObjectI] = itemV;
            _totalScore += itemV;

            #region FtFx

            var position = posFt.position;
            var ft = Instantiate(fillingFtText, new Vector3(position.x, position.y, position.x),
                Quaternion.Euler(0, 90, 0), posFt);
            ft.transform.position = position;
            var round = Math.Round(itemV, 1);
            fillingFtText.GetComponentInChildren<TMP_Text>().text = "+" + round + "ft";
            Destroy(ft, 2f);

            #endregion

            HighScoreMovement.Instance.SetDistance(-itemV);
            scoreTransformPos.DOMoveY(_totalScore, 0f).OnComplete(() => ScoreUpdate());
            PlayerPrefs.SetFloat("SkorValue", _totalScore);
        }

        #region SimpleFuncion

        public void TotalSkorStart()
        {
            _totalScore = PlayerPrefs.GetFloat("SkorValue");
            // _highScoreValue = PlayerPrefs.GetFloat("HighSkore");
        }

        public void StartScale()
        {
            transform.DOScale(new Vector3(scaleSize, scaleSize, scaleSize), 0);
        }

        public void PunchStartGame()
        {
            StartCoroutine(ForeverPunch());
        }

        public void TotalPunchSave()
        {
            PlayerPrefs.SetFloat("TotalPunchValue", PowerManager.Instance.TotalPunchValue);
        }

        public void SetSave()
        {
            punchValue = PlayerPrefs.GetFloat("PunchValue");
        }

        public void PunchSave()
        {
            PlayerPrefs.SetFloat("ScaleSize", scaleSize);
            PlayerPrefs.SetFloat("PunchValue", punchValue);
        }

        #endregion
    }
}