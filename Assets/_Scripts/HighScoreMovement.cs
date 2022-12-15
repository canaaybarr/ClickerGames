using _Scripts.Character;
using DG.Tweening;
using UnityEngine;

namespace _Scripts
{
    public class HighScoreMovement : Singleton<HighScoreMovement>
    {
        
        [SerializeField] private RectTransform moveTransform;
        [SerializeField] private RectTransform scoreTransform;
        public float distance;
        
        
        
        public void SetUpdate()
        {
            // scoreTransform.DOLocalMoveY(moveTransform.localPosition.y + distance,1f);
        }


        public void SetDistance(float value)
        {
            distance += value;
            distance = Mathf.Clamp(distance,0,Mathf.Infinity);
        }
    }
}
