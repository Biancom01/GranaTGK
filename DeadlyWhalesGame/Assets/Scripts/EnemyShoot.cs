using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab pocisku
    public Transform firePoint; // Punkt wystrzału
    public Transform player; // Gracz, do którego przeciwnik ma celować
    public float fireRate = 1f; // Częstotliwość strzałów (w sekundach)
    public float bulletSpeed = 15f; // Prędkość pocisku
    public float aimAccuracy = 0.8f; // Celność przeciwnika (0 = losowe, 1 = perfekcyjna)

    private float nextFireTime = 0f; // Licznik czasu do kolejnego strzału

    void Start()
    {
        // Automatyczne znalezienie gracza w scenie, jeśli nie przypisano w inspektorze
        if (player == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                Debug.LogError("Nie znaleziono obiektu gracza w scenie!");
            }
        }
    }

    void Update()
    {
        if (player == null) return; // Wyjdź, jeśli gracz nie został przypisany

        // Celowanie z uwzględnieniem celności
        Vector3 targetPosition = GetInaccurateTargetPosition();
        Vector3 direction = (targetPosition - firePoint.position).normalized;
        firePoint.forward = direction; // Ustawienie kierunku strzału

        // Strzelanie co określoną ilość czasu
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // Tworzenie pocisku
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = firePoint.forward * bulletSpeed; // Ustaw prędkość pocisku
        }
        else
        {
            Debug.LogError("Prefab pocisku nie ma komponentu Rigidbody!");
        }

        // Ignorowanie kolizji między pociskiem a przeciwnikiem
        Collider bulletCollider = bullet.GetComponent<Collider>();
        Collider enemyCollider = GetComponent<Collider>();

        if (bulletCollider != null && enemyCollider != null)
        {
            Physics.IgnoreCollision(bulletCollider, enemyCollider);
        }
    }

    Vector3 GetInaccurateTargetPosition()
    {
        // Idealna pozycja gracza
        Vector3 perfectTarget = player.position;

        // Dodanie losowego odchylenia na podstawie celności
        float inaccuracyRadius = 1f - aimAccuracy; // Większe odchylenie przy niższej celności
        Vector3 randomOffset = new Vector3(
            Random.Range(-inaccuracyRadius, inaccuracyRadius),
            Random.Range(-inaccuracyRadius, inaccuracyRadius),
            Random.Range(-inaccuracyRadius, inaccuracyRadius)
        );

        // Finalna pozycja celu z odchyleniem
        return perfectTarget + randomOffset;
    }
}
