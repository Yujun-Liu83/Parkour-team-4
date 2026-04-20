using UnityEngine;

public class SimpleThirdPersonController : MonoBehaviour
{
    public CharacterController controller;
    public Transform cameraTransform;
    public Animator animator;

    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float rotationSpeed = 10f;
    public float jumpHeight = 1.2f;
    public float gravity = -20f;

    private Vector3 velocity;
    private bool isGrounded;

    void Update()
    {
        if (controller == null || cameraTransform == null)
            return;

        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;
        bool isMoving = inputDirection.magnitude >= 0.1f;
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        if (isMoving)
        {
            float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * currentSpeed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (animator != null)
        {
            animator.SetFloat("Speed", isMoving ? 1f : 0f);
            animator.SetBool("Grounded", isGrounded);
            animator.SetBool("Running", isRunning && isMoving);
        }
    }
}