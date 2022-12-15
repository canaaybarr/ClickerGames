using System.Collections.Generic;
using _Scripts._Managers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Scripts
{
    public class UIChangeImage : Singleton<UIChangeImage>
    {
        public List<Sprite> itemImages = new List<Sprite>();
        
        public Image itemImage;
        public Image nextItemImage;
        
        public void SceneImage()
        {
            itemImage.sprite = itemImages[ItemManager.Instance.SceneObjectI];
        }
        public void NextSceneImage()
        {   if(ItemManager.Instance.SceneObjectI + 1 < itemImages.Count)
            nextItemImage.sprite = itemImages[ItemManager.Instance.SceneObjectI + 1];
        }
    }
}
