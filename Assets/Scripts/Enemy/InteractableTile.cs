using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public abstract class InteractableTile : MonoBehaviour
{
    [SerializeField] string displayMsg;

    private void Awake()
    {
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        box.isTrigger = true;
    }
    public abstract void Interact();

    void ShowInfo()
    {
        Debug.Log(displayMsg);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ShowInfo();
            Movements player = (Movements)collision.gameObject.GetComponent(typeof(Movements));
            player.interactableObject = this;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //HideInfo();
            Movements player = (Movements)collision.gameObject.GetComponent(typeof(Movements));
            if(player.interactableObject == this)
            {
                player.interactableObject = null;
            }
        }
    }

}
