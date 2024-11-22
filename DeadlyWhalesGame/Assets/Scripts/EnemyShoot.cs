using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bulletPrefab;   // Prefab pocisku
    public Transform firePoint;       // Punkt, z którego przeciwnik strzela
    public float fireRate = 2f;       // Czas między strzałami
    public float bulletSpeed = 15f;   // Prędkość pocisku
    private Transform player;         // Referencja do gracza

    private float nextFireTime = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        // Strzelanie w kierunku gracza
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogError("BulletPrefab lub FirePoint nie są przypisane!");
            return;
        }

        // Strzelanie w kierunku gracza
        Vector3 directionToPlayer = (player.position - firePoint.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(directionToPlayer));
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = directionToPlayer * bulletSpeed;
        }
    }
}
