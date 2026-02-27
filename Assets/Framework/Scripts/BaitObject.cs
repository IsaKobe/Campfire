using Unity.VectorGraphics;
using UnityEngine;

namespace Framework.Scripts
{
    public class BaitObject : MonoBehaviour
    {
        private BaitData data;
        
        public BaitObject(BaitData data)
        {
            this.data = data;
        }


    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (this.data == null)
            {
                this.data = Baits.baits["base"];
            }
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = data.sprite;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
