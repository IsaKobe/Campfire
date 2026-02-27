using System;
using UnityEngine;

namespace Fish
{
    public class Fish : MonoBehaviour
    {
        public FishData data;
        public bool faceLeft;

        public void Init(FishData _data, Vector2 position, bool left)
        {
            data = _data;
            transform.position = position;
            faceLeft = left;
            Turn(true);
        }

        public void Turn(bool isInit = false)
        {
            if(!isInit)
                faceLeft = !faceLeft;
            transform.rotation = Quaternion.Euler(0, 0, faceLeft ? 0 : 180);
        }
    }
}