using System.Collections.Generic;
using _Scripts._Managers;
using UnityEngine;

namespace _Scripts
{
    public class TableChange : Singleton<TableChange>
    {
        public List<GameObject> tables = new List<GameObject>();


        public void ChangeTable(int i)
        {
            if (i >= 4 && i < 7)
            {
                tables[0].SetActive(false);
                tables[1].SetActive(true);
                tables[2].SetActive(false);
            }
            else if (i >= 7)
            {
                tables[0].SetActive(false);
                tables[1].SetActive(false);
                tables[2].SetActive(true);
            }
            else
            {
                tables[0].SetActive(true);
                tables[1].SetActive(false);
                tables[2].SetActive(false);
            }
        }
    }
}
