using UnityEngine;
using TMPro; // Do obsługi wyświetlania amunicji w UI
using System.Collections; // Dodano brakujący namespace

public class WeaponShoot : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab pocisku
    public Transform firePoint; // Punkt wystrzału
    public int magazineSize = 10; // Pojemność magazynka
    public float reloadTime = 2f; // Czas przeładowania
    public float bulletSpeed = 20f; // Prędkość pocisku

    public TextMeshProUGUI ammoText; // Tekst wyświetlający liczbę pocisków w magazynku
    public GameObject player; // Obiekt gracza (do ignorowania kolizji)

    private int currentAmmo; // Aktualna liczba pocisków w magazynku
    private bool isReloading = false; // Czy broń jest w trakcie przeładowania

    void Start()
    {
        currentAmmo = magazineSize; // Ustaw początkową liczbę amunicji
        UpdateAmmoText(); // Zaktualizuj wyświetlanie amunicji
    }

    void Update()
    {
        // Jeśli broń jest w trakcie przeładowania, blokuj strzelanie
        if (isReloading)
            return;

        // Obsługa przeładowania na klawiszu "R"
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }

        // Obsługa strzału
        if (Input.GetMouseButtonDown(0)) // Strzał lewym przyciskiem myszy
        {
            if (currentAmmo > 0)
            {
                Shoot();
            }
            else
            {
                Debug.Log("Brak amunicji! Przeładuj broń.");
            }
        }
    }

    void Shoot()
    {
        currentAmmo--; // Zmniejsz liczbę pocisków w magazynku
        UpdateAmmoText(); // Zaktualizuj licznik amunicji

        // Tworzenie pocisku
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.forward * bulletSpeed; // Ustaw prędkość pocisku
        }

        // Ignorowanie kolizji pocisku z graczem
        Collider bulletCollider = bullet.GetComponent<Collider>();
        Collider playerCollider = player.GetComponent<Collider>();
        if (bulletCollider != null && playerCollider != null)
        {
            Physics.IgnoreCollision(bulletCollider, playerCollider);
        }
    }

    IEnumerator Reload()
    {
        Debug.Log("Przeładowywanie...");
        isReloading = true;

        // Wyświetl komunikat w UI
        if (ammoText != null)
        {
            ammoText.text = "Przeładowywanie...";
        }

        yield return new WaitForSeconds(reloadTime); // Odczekaj czas przeładowania

        currentAmmo = magazineSize; // Odnów liczbę pocisków
        isReloading = false;
        UpdateAmmoText(); // Zaktualizuj licznik amunicji
    }

    void UpdateAmmoText()
    {
        if (ammoText != null)
        {
            ammoText.text = $"Amunicja: {currentAmmo}/{magazineSize}";
        }
    }
}
