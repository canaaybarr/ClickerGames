using _Scripts._Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
    public class ItemActive : MonoBehaviour
    {
        [SerializeField] private Image buttonImage;
        [SerializeField] private TMP_Text valuetext;
        [SerializeField] private TMP_Text stText;
    
        
        void Update()
        {
            if (MoneyManager.Instance.MoneyValue <= ItemManager.Instance.ItemSellValue)
            {
                buttonImage.color = Color.gray;
                valuetext.color = Color.gray;
                stText.color = Color.gray;
            }
            else
            {
                buttonImage.color = Color.white;
                valuetext.color = Color.white;
                stText.color = Color.white;
            }
        }
    }
}
