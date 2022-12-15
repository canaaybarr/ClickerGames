using System;
using _Scripts._Managers;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts
{
    public class DestroyGameObject : MonoBehaviour
    {
        [SerializeField] private float min;
        [SerializeField] private float  max;
        void Start()
        {
            EX_TimeManager.Instance.transform.DOMoveX(0f,0.4f).OnComplete(() =>
            {
                float time = Random.Range(min, max);
                EX_TimeManager.Instance.transform.DOMoveX(0, time).OnComplete(() =>
                {
                    gameObject.layer = 6;
                    Destroy(gameObject);
                });
            });
        }
    }
}
