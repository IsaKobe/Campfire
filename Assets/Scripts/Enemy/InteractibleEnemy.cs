using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class InteractibleEnemy : MonoBehaviour
{
    [SerializeField]
    List<ObjectOnInteract> linkedObjectArray;
    public void Die()
    {
        foreach (ObjectOnInteract linkedObject in linkedObjectArray)
            linkedObject.OnInteract();
        Destroy(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger enter");
        if (collision.gameObject.CompareTag("Sword"))
        {
            Sword sword = (Sword)collision.gameObject.GetComponent(typeof(Sword));
            Die();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision enter");
        if (collision.gameObject.CompareTag("Player"))
        {
            Movements player = (Movements)collision.gameObject.GetComponent(typeof(Movements));
            player.DealDmg(0);
        }
        
    }

}
