using System;
using TMPro;
using UnityEngine;

namespace _Scripts._Managers
{
    public class MoneyManager : Singleton<MoneyManager>
    {
        
        [Header("Moneys")]
        
        #region MoneyValue
        
        public float moneyValue;
        public TMP_Text moneyText;
        
        public float MoneyValue
        {
            get
            {
                PlayerPrefs.SetFloat("MoneyValue",moneyValue);
                return moneyValue;
            }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                EX_GameManager.Instance.SetMoney(value,moneyText);
                moneyText.GetComponentInParent<EnableExtension>().CallTween();
                PlayerPrefs.SetFloat("MoneyValue",value);
                moneyValue = value;
            }
        }
        #endregion

        public void SaveMoney()
        {
            MoneyValue = PlayerPrefs.GetFloat("MoneyValue");
        }
    }
}
