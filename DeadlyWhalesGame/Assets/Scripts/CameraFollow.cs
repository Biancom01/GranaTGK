using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Obiekt gracza do śledzenia
    public Vector3 offset = new Vector3(0, 80, -32); // Offset pozycji kamery względem gracza
    public float smoothSpeed = 0.125f; // Płynność ruchu kamery

    void LateUpdate()
    {
        // Oblicz docelową pozycję kamery
        Vector3 desiredPosition = player.position + offset;

        // Płynnie interpoluj pozycję kamery (smooth follow)
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Kamera zawsze patrzy na gracza
        transform.LookAt(player.position + Vector3.up * 1.5f); // Patrz na środek gracza (lub lekko powyżej)
    }
}
