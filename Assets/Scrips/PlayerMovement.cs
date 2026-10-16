using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    public float moveSpeed = 6f;
    public float rotationSpeed = 250f; // grados por segundo
    public float gravity = -9.81f;
    public float jumpHeight = 1.2f;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 1. Mantener al jugador en el suelo
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // 2. Input
        float turn = Input.GetAxis("Horizontal");   // A/D o flechas
        float forward = Input.GetAxis("Vertical");  // W/S o flechas

        // 3. Girar sobre su propio eje (funciona aunque esté quieto)
        transform.Rotate(0f, turn * rotationSpeed * Time.deltaTime, 0f);

        // 4. Avanzar/retroceder hacia donde mira el personaje
        Vector3 move = transform.forward * forward * moveSpeed;

        // 5. Salto
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // 6. Gravedad
        velocity.y += gravity * Time.deltaTime;

        // 7. Un solo Move con todo combinado
        controller.Move((move + velocity) * Time.deltaTime);
    }
}