using Assets.Scripts.Invenory;
using UnityEngine;

public class HatPickup : MonoBehaviour
{
    [SerializeField]
    public ItemData data;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Inventory.Inv.hats.Add(data);
                Destroy(gameObject);
            }
        }
    }
}
