using System;
using System.Collections;
using _Scripts._Managers;
using UnityEngine;
using DG.Tweening;

namespace _Scripts
{
    public class MoneyAdd : MonoBehaviour
    {
        private void Start()
        {
            EX_TimeManager.Instance.transform.DOMoveX(0, 2.2f).OnComplete(() =>
            {
                EffectCreator.Instance.AddCoinEffect(ItemManager.Instance.sceneItem.GetComponentInChildren<HitObjectItems>().backUpHp);
                StartCoroutine(SmootCoin());
            });
        }

        IEnumerator SmootCoin()
        {
            var itemHp = ItemManager.Instance.sceneItem.GetComponentInChildren<HitObjectItems>().backUpHp;
            for (int i = 0; i < 10; i++)
            {
                MoneyManager.Instance.MoneyValue += itemHp / 50;
                yield return new WaitForSeconds(0.02f);
            }
        }
    }
}
