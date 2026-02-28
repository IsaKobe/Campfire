using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class InteractibleEnemy : MonoBehaviour
{
    [SerializeField]
    List<GameObject> linkedObjectArray;
    public void Die()
    {
        foreach (GameObject linkedObject in linkedObjectArray)
        {
            IInteractable i = (IInteractable)linkedObject.GetComponent(typeof(IInteractable));

            i.OnInteract();
        }
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
        if (collision.gameObject.CompareTag("Sword"))
        {
            Sword sword = (Sword)collision.gameObject.GetComponent(typeof(Sword));
            Die();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Movements player = (Movements)collision.gameObject.GetComponent(typeof(Movements));
            player.DealDmg(0);
        }
        
    }

}
