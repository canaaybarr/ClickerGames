using System;
using _Scripts.Character;
using UnityEngine;

namespace _Scripts
{
    public class AITarget : Singleton<AITarget>
    {
        [SerializeField]private Transform playerTarget;

        void Update()
        {
            Vector3 relativePos = playerTarget.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = rotation;
        }
    }
}
