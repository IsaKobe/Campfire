using System;
using System.Collections.Generic;
using Framework.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;


public enum PoolType
{
    Bait,
    Enemy
}

public class ObjectPool : MonoBehaviour
{
    public List<IPoolable> objects;
    [SerializeField] public PoolType pType;
    public IPoolable GetRandom()
    {
        return objects[Random.Range(0, objects.Count)];
    }

    public void Add(IPoolable obj)
    {
        objects.Add(obj);
    }

    private void Start()
    {
        switch (pType)
        {
            case PoolType.Bait:
                Texture2D tex = Resources.Load<Texture2D>("Sprites/0.png");
                // Add(new BaitData(Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero), "test bait", "test_bait", "Test bait idk man", 0));
                break;
            case PoolType.Enemy:
                break;
        }
    }
}
