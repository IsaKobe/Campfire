using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Fish
{
    public class FishSpawner : MonoBehaviour
    {
        [SerializeField] List<FishData> fishes;
        [SerializeField] Vector2 bound;
        //[SerializeField] Vector2 max;
        [SerializeField] float verticalMove;


        List<Fish> activeFish = new();
        
        
        private void Start()
        {
            for (int i = 0; i < 10; i++)
            {
                CreateFish(fishes[Random.Range(0, fishes.Count)]);
            }
        }

        public void CreateFish(FishData data)
        {
            GameObject gameObject = new();
            gameObject.transform.parent = transform;
            Fish f = gameObject.AddComponent<Fish>();
            Vector2 spawnPos = new(Random.Range(-bound.x, bound.x), Random.Range(-bound.y, bound.y));
            f.Init(data, spawnPos, true);
            SpriteRenderer renderer = gameObject.AddComponent<SpriteRenderer>();
            renderer.sprite = data.sprite;

            activeFish.Add(f);
        }

        public void Update()
        {
            foreach (Fish f in activeFish)
            {
                Vector2 moveVec = f.faceLeft ? Vector2.left : Vector2.right;
                moveVec.y = Random.Range(0, 2) == 0 ? 1 : -1;
                moveVec *= f.data.speed * Time.deltaTime;

                moveVec += (Vector2)f.transform.localPosition;

                if (moveVec.x < -bound.x || moveVec.x > bound.x)
                    f.Turn();
                f.transform.localPosition = moveVec;
            }
        }
    }
}
