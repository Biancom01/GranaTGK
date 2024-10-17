using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6.0f; // Prędkość poruszania się
    public float jumpHeight = 1.5f; // Wysokość skoku
    public float gravity = -9.81f; // Siła grawitacji

    private CharacterController controller;
    private Vector3 velocity; // Aktualna prędkość gracza
    private bool isGrounded; // Czy gracz dotyka ziemi

    public Transform groundCheck; // Punkt, który sprawdza kontakt z ziemią
    public float groundDistance = 0.4f; // Odległość do wykrycia ziemi
    public LayerMask groundMask; // Warstwa ziemi

    void Start()
    {
        // Pobieramy komponent CharacterController
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Sprawdzamy, czy gracz jest na ziemi
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Resetujemy prędkość opadania, jeśli gracz jest na ziemi
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Pobieramy dane z osi (W, A, S, D lub strzałki)
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Przekształcamy dane na ruch w przestrzeni
        Vector3 move = transform.right * x + transform.forward * z;

        // Przemieszczamy gracza
        controller.Move(move * speed * Time.deltaTime);

        // Skok
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Dodajemy grawitację do prędkości
        velocity.y += gravity * Time.deltaTime;

        // Przemieszczamy gracza w dół, aby symulować grawitację
        controller.Move(velocity * Time.deltaTime);
    }
}
