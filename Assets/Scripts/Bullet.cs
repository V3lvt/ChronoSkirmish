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
        var target = collision.collider.GetComponent<Character>();
        if (target != null)
        {
            target.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
