using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage;
    public float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void SetDamage(float d) => damage = d;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent<TargetStats>(out var stats))
        {
            stats.ApplyHit(damage);
        }

        if (collision.collider.TryGetComponent<Character>(out var character))
        {
            character.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}