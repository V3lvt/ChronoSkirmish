using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [Header("Base Stats")]
    [SerializeField] protected float baseMaxHealth = 200f;
    [SerializeField] protected float baseMoveSpeed = 5f;
    [SerializeField] protected float baseJumpHeight = 1.2f;
    [SerializeField] protected float baseRotationSpeed = 10f;
    [SerializeField] protected float baseDpsMultiplier = 1f;

    [Header("Character Attributes (Scaled)")]
    public float maxHealth;
    public float currentHealth;
    public float moveSpeed;
    public float jumpHeight;
    public float rotationSpeed;
    public float dpsMultiplier;
    public int age = 20;

    [Header("Aging Settings")]
    public float defaultAgeRate = 0f;
    [HideInInspector] public float currentAgeRate;
    private float ageAccumulator = 0f;

    [Header("Age Stages")]
    public AgeStage[] ageStages;

    [System.Serializable]
    public struct AgeStage
    {
        public int minAge;
        public int maxAge;
        public float healthMultiplier;
        public float speedMultiplier;
        public float dpsMultiplier;
    }

    protected virtual void Awake()
    {
        currentAgeRate = defaultAgeRate;
        ApplyAgeStats();
        currentHealth = maxHealth;
    }

    protected virtual void Update()
    {
        HandleAging();
    }

    protected void HandleAging()
    {
        ageAccumulator += currentAgeRate * Time.deltaTime;
        if (Mathf.Abs(ageAccumulator) >= 1f)
        {
            int years = (int)(Mathf.Floor(Mathf.Abs(ageAccumulator)) * Mathf.Sign(ageAccumulator));
            int oldAge = age;
            age = Mathf.Clamp(age + years, 20, 90);
            ageAccumulator -= years;
            if (age != oldAge)
            {
                float oldMax = maxHealth;
                float normalizedHealth = currentHealth / oldMax;

                ApplyAgeStats();

                currentHealth = normalizedHealth * maxHealth;
                currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

                if (age >= 90)
                {
                    Die();
                }
            }
        }
    }

    protected void ApplyAgeStats()
    {
        maxHealth = baseMaxHealth;
        moveSpeed = baseMoveSpeed;
        jumpHeight = baseJumpHeight;
        rotationSpeed = baseRotationSpeed;
        dpsMultiplier = baseDpsMultiplier;

        foreach (var stage in ageStages)
        {
            if (age >= stage.minAge && age <= stage.maxAge)
            {
                maxHealth = baseMaxHealth * stage.healthMultiplier;
                moveSpeed = baseMoveSpeed * stage.speedMultiplier;
                dpsMultiplier = baseDpsMultiplier * stage.dpsMultiplier;
                return;
            }
        }
    }

    public void SetAgeRate(float rate)
    {
        currentAgeRate = rate;
    }

    public virtual void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
            Die();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}