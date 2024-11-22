using UnityEngine;
using TMPro;
using System.Collections;

public class WeaponShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public ParticleSystem muzzleFlash; // Efekt błysku przy wystrzale
    public AudioSource gunAudio; // Dźwięk wystrzału
    public int magazineSize = 10;
    public float reloadTime = 2f;
    public float bulletSpeed = 40f;
    public float bulletDrop = 1f; // Współczynnik grawitacji dla pocisku
    public float spread = 0.1f; // Rozrzut strzałów
    public TextMeshProUGUI ammoText;

    private int currentAmmo;
    private bool isReloading;

    void Start()
    {
        currentAmmo = magazineSize;
        UpdateAmmoText();
    }

    void Update()
    {
        if (isReloading) return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (currentAmmo > 0)
                Shoot();
            else
                Debug.Log("Brak amunicji! Przeładuj broń.");
        }
    }

    void Shoot()
    {
        currentAmmo--;
        UpdateAmmoText();

        // Efekty wizualne i dźwiękowe
        if (muzzleFlash != null) muzzleFlash.Play();
        if (gunAudio != null) gunAudio.Play();

        // Dodanie rozrzutu strzałów
        Vector3 spreadVector = new Vector3(
            Random.Range(-spread, spread),
            Random.Range(-spread, spread),
            0
        );
        Quaternion bulletDirection = firePoint.rotation * Quaternion.Euler(spreadVector);

        // Tworzenie pocisku
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, bulletDirection);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.forward * bulletSpeed;
            rb.useGravity = true; // Włącz wpływ grawitacji na pocisk
            rb.AddForce(Vector3.down * bulletDrop, ForceMode.Impulse);
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        if (ammoText != null) ammoText.text = "Przeładowywanie...";
        yield return new WaitForSeconds(reloadTime);

        currentAmmo = magazineSize;
        isReloading = false;
        UpdateAmmoText();
    }

    void UpdateAmmoText()
    {
        if (ammoText != null)
            ammoText.text = $"Amunicja: {currentAmmo}/{magazineSize}";
    }
}
