using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI ageText;

    void Awake()
    {
        if (healthBar == null)
            healthBar = GetComponentInChildren<Slider>();
        if (ageText == null)
            ageText = GetComponentInChildren<TextMeshProUGUI>();

        if (healthBar == null)
            Debug.LogError("HUDManager: healthBar is not assigned and no Slider found in children.");
        if (ageText == null)
            Debug.LogError("HUDManager: ageText is not assigned and no TextMeshProUGUI found in children.");
    }

    void Start()
    {
        if (GameManager.Instance == null || GameManager.Instance.player == null)
        {
            Debug.LogError("HUDManager: GameManager or PlayerController reference is missing.");
            return;
        }

        var player = GameManager.Instance.player;
        healthBar.minValue = 0;
        healthBar.maxValue = player.maxHealth;
        healthBar.value = player.currentHealth;
        ageText.text = player.age.ToString();
    }

    public void UpdateHealth(float current, float max)
    {
        if (healthBar == null) return;
        healthBar.maxValue = max;
        healthBar.value = current;
    }

    public void UpdateAge(int age)
    {
        if (ageText == null) return;
        ageText.text = "age: " + age.ToString();
    }
}
