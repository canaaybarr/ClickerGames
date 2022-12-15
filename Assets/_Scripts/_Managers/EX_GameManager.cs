using System;
using System.Collections.Generic;
using _Scripts.Character;
using DG.Tweening;
using MoreMountains.NiceVibrations;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace _Scripts._Managers
{
    public class EX_GameManager : GameManager<EX_GameManager>
    {
        #region Stage Parameters

        public Transform moneyPos;

        public Camera cam;
        public GameObject startCamPos;


        [HideInInspector] // If the game has stages, delete this line.
        public int stageCount;

        int currentStageIndex;

        #region TutorialPartial

        public List<GameObject> tutorialsObjects = new List<GameObject>();

        #endregion

        [SerializeField] public int value = 0;

        #endregion

        public Transform score;
        public Transform scoreTarget;
        public TextMeshProUGUI scoreText;
        public Transform highScore;
        public Transform highScoreTarget;
        public TextMeshProUGUI highScoreText;
        private float _highScore;

        public float scaleAmount;

        [HideInInspector] public bool highScoreBool;

        private bool newScoreBool;
        public GameObject newScoreUi;


        public Transform moneyUi;
        public Transform moneyTargetObj;

        public Transform skyBox;
        public float skyboxMultiplier;

        private void Awake()
        {
            _highScore = PlayerPrefs.GetFloat("HighScore", 0);
            Debug.Log(_highScore);
        }

        private void LateUpdate()
        {
            float target = PunchController.Instance.scaleSize / skyboxMultiplier;
            target = Mathf.Clamp(target, 0, 120f);
            skyBox.transform.eulerAngles = Vector3.Lerp(skyBox.transform.eulerAngles,
                new Vector3(skyBox.transform.eulerAngles.x, skyBox.transform.eulerAngles.y, target),
                Time.deltaTime * 20);


            Ray r = Camera.main.ScreenPointToRay(moneyUi.position);
            RaycastHit hit;
            if (Physics.Raycast(r, out hit, Mathf.Infinity))
            {
                moneyTargetObj.transform.position = hit.point;
            }
            // Debug.DrawRay(r.origin , r.direction * 100, Color.red, 100, true);
            // Vector3 target = Camera.main.ScreenToWorldPoint(moneyUi.position);
            // float dist = Vector3.Distance(Camera.main.transform.position, moneyTargetObj.transform.position);
            // moneyTargetObj.transform.position = new Vector3(target.x, moneyTargetObj.transform.position.y,dist);
            //

            highScoreTarget.transform.position = new Vector3(highScoreTarget.transform.position.x, _highScore,
                highScoreTarget.transform.position.z);
            Vector3 targetPos1 = Camera.main.WorldToScreenPoint(highScoreTarget.position);
            highScore.transform.position =
                new Vector3(highScore.transform.position.x, targetPos1.y, highScore.transform.position.z);


            scoreTarget.transform.position = new Vector3(scoreTarget.transform.position.x,
                PunchController.Instance.scaleSize * 1.6f,
                scoreTarget.transform.position.z);
            Vector3 targetPos = Camera.main.WorldToScreenPoint(scoreTarget.position);
            score.transform.position =
                new Vector3(score.transform.position.x, targetPos.y, score.transform.position.z);

            if (PunchController.Instance.scaleSize * 1.6f > _highScore)
            {
                highScore.gameObject.SetActive(false);
                if (!newScoreBool)
                {
                    newScoreUi.SetActive(true);
                    newScoreBool = true;
                }

                _highScore = PunchController.Instance.scaleSize * 1.6f;
                PlayerPrefs.SetFloat("HighScore", PunchController.Instance.scaleSize * 1.6f);
                scoreText.color = Color.green;
            }
            else if (PunchController.Instance.scaleSize * 1.6f < _highScore && !highScoreBool)
            {
                highScoreBool = true;
                newScoreBool = false;
                newScoreUi.SetActive(false);
                highScore.gameObject.SetActive(true);
                highScoreText.text = ((((_highScore) * 10)) * 0.0328f).ToString("F") + "ft";
                scoreText.color = Color.black;
            }
        }

        public void RestartPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(0);
        }

        public override void Start()
        {
            value = PlayerPrefs.GetInt("bool");
            ElephantSDK.Elephant.LevelStarted(EX_LevelManager.Instance.currentLevelNumber + 1);
            base.Start();
            if (value == 0)
            {
                tutorialsObjects[0].SetActive(true);
                tutorialsObjects[1].SetActive(false);
                UIChangeImage.Instance.NextSceneImage();
                UIChangeImage.Instance.SceneImage();
                ItemManager.Instance.ItemSellSave();
                ItemManager.Instance.SaveItemManager();
                PunchController.Instance.PunchSave();
                PunchController.Instance.SetSave();
                PowerManager.Instance.PunchTextSave();
                startCamPos.transform.position = cam.transform.position;
                value = 1;
                PlayerPrefs.SetInt("bool", value);
            }
            else if (value == 1)
            {
                tutorialsObjects[0].SetActive(false);
                tutorialsObjects[1].SetActive(true);
            }

            GameSaveSetting();
        }

        public void GameSaveSetting()
        {
            ItemManager.Instance.CameraPos();
            PunchController.Instance.scaleSize = PlayerPrefs.GetFloat("ScaleSize", 10);
            UIChangeImage.Instance.NextSceneImage();
            UIChangeImage.Instance.SceneImage();
            startCamPos.transform.position = cam.transform.position;
            MoneyManager.Instance.SaveMoney();
            ItemManager.Instance.SaveItemManager();
            ItemManager.Instance.ItemSpawner();
            PunchController.Instance.PunchStartGame();
            PunchController.Instance.SetSave();
            PunchController.Instance.TotalSkorStart();
            TableChange.Instance.ChangeTable(ItemManager.Instance.SceneObjectI);
            ItemManager.Instance.SceneObjectIndex();
            PowerManager.Instance.SavePunchValue();
            PowerManager.Instance.PunchTextSave();
            PunchController.Instance.StartScale();
            PunchController.Instance.BlendShapeSave();
            PunchController.Instance.ScoreUpdate();
        }


        public void SlowVib()
        {
            MMVibrationManager.Haptic(HapticTypes.MediumImpact);
            // Ekrana her dokundugunda hafif titretir
        }

        public void MidVib()
        {
            MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
            // Ekrana her dokundugunda hafif titretir
        }

        #region Template

        public void SetMoney(float money, TMP_Text txt)
        {
            if (money > 999999) txt.text = "$" + (money / 1000000f).ToString("F2") + "M";
            else if (money > 999) txt.text = "$" + (money / 1000f).ToString("F2") + "K";
            else txt.text = "$" + money.ToString("F0");
        }

        public override void Update()
        {
            base.Update();
            switch (state)
            {
                case STATE.Play:
                    break;
            }
        }


        public override void Perfect()
        {
            base.Perfect();
        }

        public override void Win()
        {
            if (state == STATE.Win || state == STATE.Lose)
            {
                return;
            }

            ElephantSDK.Elephant.LevelCompleted(EX_LevelManager.Instance.currentLevelNumber + 1);
            base.Win();
        }

        public override void Lose()
        {
            if (state == STATE.Win || state == STATE.Lose)
            {
                return;
            }

            ElephantSDK.Elephant.LevelFailed(EX_LevelManager.Instance.currentLevelNumber + 1);
            base.Lose();
        }

        public void NextStage()
        {
            if (currentStageIndex + 1 < stageCount)
            {
                currentStageIndex++;
                StageUIManager.Instance.NextStage();
            }
            else
            {
                StageUIManager.Instance.Finish();
                Win();
            }
        }

        #region Ragdoll

        public void CharacterRagdoll(Transform characterParent)
        {
            foreach (Rigidbody rb in characterParent.GetComponentsInChildren<Rigidbody>())
            {
                rb.isKinematic = false;
            }

            if (characterParent.GetComponent<Animator>())
            {
                characterParent.GetComponent<Animator>().enabled = false;
            }

            if (characterParent.GetComponent<Rigidbody>())
            {
                characterParent.GetComponent<Rigidbody>().isKinematic = true;
            }
        }

        public void CharacterUnRagdoll(Transform characterParent)
        {
            foreach (Rigidbody rb in characterParent.GetComponentsInChildren<Rigidbody>())
            {
                rb.isKinematic = true;
            }

            if (characterParent.GetComponent<Animator>())
            {
                characterParent.GetComponent<Animator>().enabled = true;
            }

            if (characterParent.GetComponent<Rigidbody>())
            {
                characterParent.GetComponent<Rigidbody>().isKinematic = false;
            }
        }

        #endregion

        #endregion
    }
}