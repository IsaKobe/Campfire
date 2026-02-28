using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class BossEnemy : Enemy<AttackEnemyData>
{
    Movements PlayerBaseScript;
    private ProgressBar _healthBar;
    private float AtkCooldown;
    [SerializeField] private UIDocument uiDocument;
    protected override void Awake()
    {
        base.Awake();
        AtkCooldown = EnemyStats.AttackSpeed;
        PlayerBaseScript = player.GetComponent<Movements>();
        _healthBar = uiDocument.rootVisualElement.Q<ProgressBar>("HealthBar");
        _healthBar.highValue = EnemyStats.MaxHealth;
        UpdateUI();
    }
    protected override void Update()
    {
        WatchPlayer();
        UpdateUI();
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
    private void AttackPattern() 
    {
    }
    private void SlashAttack() 
    {
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
}
