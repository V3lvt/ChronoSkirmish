using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform muzzlePoint;
    public float fireRate = 5f;
    public float bulletSpeed = 50f;
    public float baseDamage = 10f;

    private float cooldown;
    private PlayerController player;

    void Awake()
    {
        enabled = false;
    }

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (cooldown > 0f) cooldown -= Time.deltaTime;

        if (Input.GetButton("Fire1") && cooldown <= 0f)
        {
            Shoot();
            cooldown = 1f / fireRate;
        }
    }

    private void Shoot()
    {
        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        Vector3 dir = ray.direction;

        Vector3 spawnPos = muzzlePoint.position + dir * 0.1f;

        var bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.LookRotation(dir));
        var rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = dir * bulletSpeed;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        float dmg = baseDamage * (player != null ? player.dpsMultiplier : 1f);
        bullet.GetComponent<Bullet>().SetDamage(dmg);
    }
}