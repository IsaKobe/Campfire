using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    [SerializeField] DoorToggle bossRoomToggle;
    private void Awake() 
    {
    }
    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (!bossRoomToggle.WasTriggered)
        {
            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("player stepped on trigger");
                bossRoomToggle.ToggleGate();
                //SpawnBoss();
                bossRoomToggle.WasTriggered = true;
            }
        }
    }
}
