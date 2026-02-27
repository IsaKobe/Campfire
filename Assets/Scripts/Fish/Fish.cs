using System;
using UnityEngine;

namespace Fish
{
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class Fish : MonoBehaviour
    {
        public FishData data;
        public bool faceLeft;

        public bool goingUp;
        public float continueDirection;

        public Transform target;

        public void Init(FishData _data, Vector2 position, bool left)
        {
            data = _data;
            transform.position = position;
            faceLeft = left;
            target = null;
            Turn(true);

            CapsuleCollider2D detector = GetComponent<CapsuleCollider2D>();
            detector.direction = CapsuleDirection2D.Horizontal;
            detector.offset = new Vector2(-(0.65f + (data.seeRange/2)), 0);
            detector.size = new Vector2(data.seeRange, 0.25f);
        }

        public void Turn(bool isInit = false)
        {
            if(!isInit)
                faceLeft = !faceLeft;
            transform.rotation = Quaternion.Euler(0, faceLeft ? 0 : 180, 0);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Bait"))
            {
                GameObject bait = collision.gameObject;
                Debug.Log("found bait!");
            }
        }
    }
}