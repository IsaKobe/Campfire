using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Fish
{
    public class FishSpawner : MonoBehaviour
    {
        [SerializeField] List<FishData> fishes;
        [SerializeField] float width;
        [SerializeField] float lineHeight;
        [SerializeField] float linePadding;
        //[SerializeField] Vector2 max;
        [SerializeField] float verticalMove;
        [SerializeField] int fishCount = 20;

        List<Fish> activeFish = new();

        private void OnDrawGizmos()
        {
            for (int i = 0; i < 5; i++)
            {
                Vector2 vector2 = GetHeightBounds(i);
                Gizmos.DrawLine(new(-width, vector2.x), new(width, vector2.x));
                Gizmos.DrawLine(new(-width, vector2.y), new(width, vector2.y));
            }
        }

        private void Start()
        {
            for (int i = 0; i < fishCount; i++)
            {
                CreateFish(fishes[Random.Range(0, fishes.Count)]);
            }
        }

        public void CreateFish(FishData data)
        {
            GameObject gameObject = new();
            gameObject.transform.parent = transform;
            Fish f = gameObject.AddComponent<Fish>();

            Vector2 heightBounds = GetHeightBounds(data.level);
            Vector2 spawnPos = new(Random.Range(-width, width), Random.Range(heightBounds.x, heightBounds.y));
            f.Init(data, spawnPos, true);
            SpriteRenderer renderer = gameObject.AddComponent<SpriteRenderer>();
            renderer.sprite = data.sprite;

            activeFish.Add(f);
        }

        public void Update()
        {
            foreach (Fish f in activeFish)
            {
                MoveFish(f);
            }
        }

        Vector2 GetHeightBounds(int level)
        {
            float min = -(level * lineHeight + level * linePadding);
            float max = min - lineHeight;
            return new Vector2(min, max);
        }

        void MoveFish(Fish fish)
        {
            Vector2 moveVec = fish.faceLeft ? Vector2.left : Vector2.right;
            if (fish.continueDirection > 0)
            {
                moveVec.y = (fish.goingUp ? 0.2f : -0.2f);
                fish.continueDirection -= Time.deltaTime;
            }
            else
            {
                fish.continueDirection = Random.Range(0.5f, 2f);
                fish.goingUp = Random.Range(0, 2) == 1;
            }

            moveVec *= fish.data.speed * Time.deltaTime;

            moveVec += (Vector2)fish.transform.localPosition;

            if (moveVec.x < -width || moveVec.x > width)
                fish.Turn();

            Vector2 bound = GetHeightBounds(fish.data.level);

            if (moveVec.y > bound.x)
            {
                fish.goingUp = false;
                fish.continueDirection = Random.Range(0.5f, 2f);
            }
            else if (moveVec.y < bound.y)
            {
                fish.goingUp = true;
                fish.continueDirection = Random.Range(0.5f, 2f);
            }
            fish.transform.localPosition = moveVec;
        }
    }
}
