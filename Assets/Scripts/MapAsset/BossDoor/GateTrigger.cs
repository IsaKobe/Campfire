using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GateTrigger : MonoBehaviour
{
    [SerializeField] DoorToggle bossRoomToggle;
    [SerializeField] GameObject boss;
    [SerializeField] Transform bossSpawnPoint;

    [SerializeField] private UIDocument uiDocument;
    [SerializeField] GameObject bossUI;
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
                SpawnBoss();
                bossRoomToggle.WasTriggered = true;
            }
        }
    }

    private void SpawnBoss() 
    {
        GameObject bossObject=Instantiate(boss, bossSpawnPoint.position, bossSpawnPoint.rotation);
        BossEnemy bossScript = bossObject.GetComponent<BossEnemy>();
        bossScript.uiDocument = uiDocument;
        bossScript.bossUI = bossUI;
        bossScript.bossRoomToggle = bossRoomToggle;
        bossScript.OnSpawn();
    
    }
}
