using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI ageText;
    [SerializeField] private TextMeshProUGUI damageText;

    void Awake()
    {
        if (healthText == null)
        {
            var ht = transform.Find("HealthText");
            if (ht != null) healthText = ht.GetComponent<TextMeshProUGUI>();
        }
        if (ageText == null)
        {
            var at = transform.Find("AgeText");
            if (at != null) ageText = at.GetComponent<TextMeshProUGUI>();
        }
        if (damageText == null)
        {
            var dt = transform.Find("DamageText");
            if (dt != null) damageText = dt.GetComponent<TextMeshProUGUI>();
        }

        if (healthText == null)
            Debug.LogError("HUDManager: healthText is not assigned and no child named 'HealthText' found.");
        if (ageText == null)
            Debug.LogError("HUDManager: ageText is not assigned and no child named 'AgeText' found.");
        if (damageText == null)
            Debug.LogError("HUDManager: damageText is not assigned and no child named 'DamageText' found.");
    }

    void Start()
    {
        var player = GameManager.Instance?.player;
        if (player == null)
        {
            Debug.LogError("HUDManager: PlayerController reference is missing from GameManager.");
            return;
        }
        UpdateHealth(player.currentHealth, player.maxHealth);
        UpdateAge(player.age);
        UpdateDamage(player.dpsMultiplier);
    }

    public void UpdateHealth(float current, float max)
    {
        if (healthText == null) return;
        healthText.text = "hp: " + string.Format("{0}/{1}", Mathf.RoundToInt(current), Mathf.RoundToInt(max));
    }

    public void UpdateAge(int age)
    {
        if (ageText == null) return;
        ageText.text = age.ToString();
    }

    public void UpdateDamage(float dpsMultiplier)
    {
        if (damageText == null) return;
        damageText.text = string.Format("damage multiplier: {0}x", dpsMultiplier);
    }
}
