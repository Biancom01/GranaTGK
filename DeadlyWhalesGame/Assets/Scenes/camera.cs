using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Czułość myszki

    public Transform playerBody; // Referencja do ciała gracza

    private float xRotation = 0f; // Aktualny obrót w osi X (góra/dół)

    void Start()
    {
        // Ukrywamy kursor podczas gry
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Pobieramy wartości ruchu myszki
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Obrót kamery w osi X (góra/dół)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Ograniczamy zakres obrotu, aby nie można było obrócić kamery całkowicie w górę lub w dół

        // Obracamy kamerę w górę/dół
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Obracamy ciałem gracza w osi Y (lewo/prawo)
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
