using Assets.Scripts.Enemy.Enemies;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class BossEnemy : AttackEnemy
{
    private ProgressBar _healthBar;
    public  UIDocument uiDocument;
    public GameObject bossUI;
    public DoorToggle bossRoomToggle;
    [SerializeField] Transform ClawLeft;
    [SerializeField] Transform ClawRight;
    [SerializeField] GameObject Claws;
    [SerializeField] Transform ClawsTransform;
    private float AttackCooldown;

    protected override void Awake()
    {
        base.Awake();
        AttackCooldown = 0;
    }
    protected override void Update()
    {
        AttackCooldown -= Time.deltaTime;
        WatchPlayer();
        if(AttackCooldown <= 0) 
        {
            SlashAttackStart();
        }
        
    }
    protected override void FixedUpdate()
    {

    }

    private void WatchPlayer()
    {
        Vector3 diff = player.position - transform.position;
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg + (180);
        transform.rotation = Quaternion.Euler(0, 0, rot_z);
    }
    private void SlashAttackStart()
    {
        Debug.Log("SlashAttack Started");
        ClawsTransform.position = new Vector2(ClawsTransform.position.x , ClawsTransform.position.y - 0.6f);
        Claws.SetActive(true);
        ClawLeft.rotation = Quaternion.Euler(0, 0, ClawLeft.rotation.z - 120f);
        ClawRight.rotation = Quaternion.Euler(0, 0, ClawRight.rotation.z + 120f);
        AttackCooldown = AtkEnemyData.AttackSpeed;
        StartCoroutine(SlashAttackFinished());
        
    }
    private IEnumerator SlashAttackFinished() 
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("SlashAttack Finished");

        ClawsTransform.position = new Vector2(ClawsTransform.position.x , ClawsTransform.position.y + 0.6f);
        Claws.SetActive(false);
        ClawLeft.rotation = Quaternion.Euler(0, 0, ClawLeft.rotation.z + 120f);
        ClawRight.rotation = Quaternion.Euler(0, 0, ClawRight.rotation.z - 120f);
    }


    public void OnSpawn() 
    {
        bossUI.SetActive(true);
        _healthBar = uiDocument.rootVisualElement.Q<ProgressBar>("HealthBar");
        _healthBar.highValue = EnemyStats.MaxHealth;
        UpdateUI();
    }
    public override void OnDeath() 
    {
        bossRoomToggle.ToggleGate();
        bossUI.SetActive(false);
        Destroy(gameObject);
    }
    void UpdateUI()
    {
        if (_healthBar != null)
        {
            // Progress bars use a 0 to 100 scale by default
            _healthBar.value = CurrentHealth;

            // Optional: Change the title text inside the bar
            _healthBar.title = $"HP: {CurrentHealth}/{EnemyStats.MaxHealth}";
        }
        else 
        {
            Debug.Log("NO HEALTHBAR FOUND");
        }
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        UpdateUI();

    }
}
