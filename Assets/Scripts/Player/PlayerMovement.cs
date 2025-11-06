using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 5f;
    public float jumpForce = 2f;
    public float gravity = -9.81f;
    public Transform groundCheckPoint;
    public float groundCheckRadius = 0.5f;
    public LayerMask groundLayer;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 move = transform.right * InputManager.playerActions.Move.ReadValue<Vector2>().x + transform.forward * InputManager.playerActions.Move.ReadValue<Vector2>().y;
        controller.Move(move * speed * Time.deltaTime);

        isGrounded = Physics.CheckSphere(groundCheckPoint.position, groundCheckRadius, groundLayer);
        Debug.DrawLine(groundCheckPoint.position, groundCheckPoint.position + Vector3.down * groundCheckRadius, Color.red);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (InputManager.playerActions.Jump.triggered && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
