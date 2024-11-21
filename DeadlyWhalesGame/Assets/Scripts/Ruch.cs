using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Ruch : MonoBehaviour
{
    public float speed = 6.0f;          // Prêdkoœæ poruszania siê postaci
    public float gravity = -9.81f;      // Grawitacja, aby postaæ nie "fruwa³a"
    public float jumpHeight = 1.0f;     // Wysokoœæ skoku
    public Camera playerCamera;         // Kamera gracza
    public LayerMask groundLayer;       // Warstwa pod³o¿a, na której postaæ siê porusza
    public float rotationSpeed = 5f;    // Prêdkoœæ obrotu kamery i postaci

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Sprawdzenie, czy postaæ jest na ziemi
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Zapewnia, ¿e postaæ trzyma siê pod³o¿a
        }

        // Poruszanie postaci niezale¿nie od jej rotacji
        MovePlayer();

        // Skok
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Grawitacja
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Obracanie postaci w kierunku kursora
        RotateTowardsMouse();
    }

    void MovePlayer()
    {
        // Odczytanie wejœcia gracza (WSAD)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Poruszamy postaci¹ w p³aszczyŸnie XZ kamery (nie w kierunku, w którym patrzy postaæ)
        Vector3 move = playerCamera.transform.right * moveX + playerCamera.transform.forward * moveZ;
        move.y = 0; // Zerujemy komponent Y, aby poruszanie by³o tylko w p³aszczyŸnie XZ

        // Przesuniêcie postaci
        controller.Move(move * speed * Time.deltaTime);
    }

    void RotateTowardsMouse()
    {
        // Tworzymy promieñ od kamery do pozycji kursora
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        // Przechowuje informacjê o trafionym punkcie
        RaycastHit hit;

        // Sprawdzamy, czy promieñ uderzy³ w pod³o¿e
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            // Pobieramy pozycjê, na któr¹ wskazuje kursor
            Vector3 targetPosition = hit.point;

            // Obliczamy kierunek obrotu, ignoruj¹c wysokoœæ (y)
            Vector3 direction = (targetPosition - transform.position).normalized;
            direction.y = 0; // Ignorujemy komponent Y, aby postaæ obraca³a siê tylko w p³aszczyŸnie XZ

            // Sprawdzamy, czy kierunek nie jest zerowy
            if (direction != Vector3.zero)
            {
                // Obrót gracza w kierunku kursora z kontrolowan¹ prêdkoœci¹
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // P³ynny obrót przy u¿yciu Slerp, aby kontrolowaæ prêdkoœæ
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }
}
