using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public abstract class InteractableTile : MonoBehaviour
{
    [SerializeField]protected string displayMsg;

    protected virtual void Awake()
    {
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        box.isTrigger = true;

        GameObject gb = Instantiate(Resources.Load<GameObject>("InteractKey/f_key"), transform);
        gb.transform.localPosition = new(0, 0.75f);
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

    internal void Hide()
    {
        if(transform.childCount > 0)
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        enabled = false;
    }

    internal void Show()
    {
        if (transform.childCount > 0)
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
        enabled = true;
    }
}
