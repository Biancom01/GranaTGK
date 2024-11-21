using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform player; // Automatycznie znajdowany gracz
    public float moveSpeed = 2f; // Prędkość ruchu

    void Start()
    {
        // Znajdź obiekt gracza w scenie
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("Nie znaleziono obiektu gracza w scenie!");
        }
    }

    void Update()
    {
        if (player == null)
            return; // Wyjdź, jeśli gracz nie został znaleziony

        // Kierunek w stronę gracza
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // Zablokowanie osi Y

        // Poruszanie się w stronę gracza
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Obracanie się w stronę gracza
        if (direction.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
