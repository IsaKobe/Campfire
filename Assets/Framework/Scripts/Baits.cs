using System.Collections.Generic;
using System.Linq;
using Framework.Scripts;
using UnityEngine;
using UnityEngine.UI;

public static class Baits
{
    
    public static Dictionary<string, BaitData> baits = new Dictionary<string, BaitData>();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Awake()
    {
        var  tex = Resources.LoadAll("Sprites/Bait", typeof(Texture2D)).Cast<Texture2D>().ToArray();
        Texture2D zero = tex.Where(x => x.name == "0").FirstOrDefault();
        baits.Add("base", new BaitData(Sprite.Create(zero, new Rect(0, 0, zero.width, zero.height), Vector2.zero), "Base Bait", "Basic bait, nothing special", 0));
    }
}
