using System.Collections.Generic;
using Framework.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class Baiits : MonoBehaviour
{
    
    public Dictionary<string, BaitData> baits = new Dictionary<string, BaitData>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Texture2D tex = Resources.Load<Texture2D>("Sprites/Bait/0.png");
        baits.Add("base", new BaitData(Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero), "Base Bait", "Basic bait, nothing special", 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
