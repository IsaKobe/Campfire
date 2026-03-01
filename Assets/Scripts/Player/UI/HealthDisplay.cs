using UnityEngine;
using UnityEngine.UIElements;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] Movements player;
    private void Awake()
    {
        UIDocument document = GetComponent<UIDocument>();
        ProgressBar bar = document.rootVisualElement.Q<ProgressBar>();
        DataBinding binding = new DataBinding()
        {
            dataSource = player,
            bindingMode = BindingMode.ToTarget,
            dataSourcePath = new(nameof(Movements.Health))
        };

        bar.SetBinding(nameof(bar.value), binding);
        bar.highValue = player.maxHealth;
    }
}
