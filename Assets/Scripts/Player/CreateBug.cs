using UnityEngine;

public class CreateBug : MonoBehaviour
{
    [SerializeField] GameObject bug;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bug.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
