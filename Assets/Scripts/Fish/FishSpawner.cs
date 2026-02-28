using Framework.Scripts;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.VisualScripting;
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

        [SerializeField] int level;


        List<Fish> activeFish = new();

        HookControls controls;


        //public float Width => width;
        public Vector2 GetHeightBounds(int level)
        {
            float min = -(level * lineHeight + level * linePadding);
            float max = min - lineHeight;
            return new Vector2(min, max);
        }

        private void OnDrawGizmos()
        {
            for (int i = 0; i < level; i++)
            {
                Vector2 vector2 = GetHeightBounds(i);
                Gizmos.DrawLine(new(-width, vector2.x), new(width, vector2.x));
                Gizmos.DrawLine(new(-width, vector2.y), new(width, vector2.y));
            }
        }

        public void Init(BaitData bait, HookControls _controls)
        {
            controls = _controls;
            level = bait.rarity;

            /*fishes = fishes.Where(q => q.level <= level).ToList();*/

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
            

            activeFish.Add(f);
        }

        public void Update()
        {
            for (int i = activeFish.Count -1; i > -1; i--)
            {
                MoveFish(activeFish[i]);
            }
        }


        void MoveFish(Fish fish)
        {
            if(fish.target == null || fish.canChase == false)
            {
                fish.Wander(this);
            }
            else
            {
                fish.Chase(this);
            }
        }

        public void GetCaught(Fish fish)
        {
            foreach (var item in activeFish)
            {
                item.TurnOff();
            }
            activeFish.Remove(fish);
            controls.PullFish(fish);
        }
    }
}
