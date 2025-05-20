using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [Header("Character Attributes")]
    public float maxHealth = 200f;
    public float currentHealth;
    public int age = 20;
    public float moveSpeed = 5f;
    public float jumpHeight = 1.2f;
    public float rotationSpeed = 10f;
    public float dpsMultiplier = 1f;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
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
