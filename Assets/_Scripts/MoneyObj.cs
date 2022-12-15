using System;
using _Scripts._Managers;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts
{
    public class MoneyObj : Singleton<MoneyObj>
    {
        private bool move;
        private void Start()
        {
            EX_TimeManager.Instance.transform.DOMoveX(0, Random.Range(1f, 2f)).OnComplete(() =>
            {
                // EX_TimeManager.Instance.transform.DOMoveX(0, 0.9f).OnComplete(() =>
                // {
                //     // MoneyManager.Instance.MoneyValue = MoneyManager.Instance.MoneyValue;
                // });
                move = true;
                GetComponent<Collider>().enabled = false;
                GetComponent<Rigidbody>().useGravity = false;
            });
        }

        private void Update()
        {
            if (move)
            {
                transform.position = Vector3.Lerp(transform.position, EX_GameManager.Instance.moneyPos.position,
                    Time.deltaTime * 15);
                if(Vector3.Distance(transform.position,EX_GameManager.Instance.moneyPos.position) < 2f)
                    Destroy(gameObject);
            }
          
        }
    }
    
}
