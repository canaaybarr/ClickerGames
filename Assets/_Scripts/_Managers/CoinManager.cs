using System;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Scripts._Managers
{
    public class CoinManager : MonoBehaviour
    {
        [SerializeField] private Image button;
        [SerializeField] private Image itemImage;
        [SerializeField] private TMP_Text text;
        [SerializeField] private TMP_Text cointext;
        [SerializeField] private TMP_Text itemText;


        private void Update()
        {
            if (MoneyManager.Instance.MoneyValue <= ItemManager.Instance.ItemSellValue)
            {
                button.color = Color.gray;
                itemImage.color = Color.gray;
                text.color = Color.gray;
                cointext.color = Color.gray;
                // itemText.color = Color.gray;
            }
            else
            {
                button.color = Color.white;
                itemImage.color = Color.white;
                text.color = Color.white;
                cointext.color = Color.white;
                // itemText.color = Color.white;
            }
        }
    }
}
