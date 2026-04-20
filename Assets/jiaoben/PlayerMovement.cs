using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cameraTransform;
    public Animator animator;

    [Header("ÒÆ¶¯")]
    public float normalSpeed = 5f;
    public float boostedSpeed = 10f;
    public float rotationSmoothTime = 0.1f;

    [Header("ÌøÔ¾")]
    public float jumpHeight = 1.2f;
    public float gravity = -20f;

    private float currentSpeed;
    private float rotationVelocity;
    private Vector3 velocity;
    private bool isGrounded;
    private Coroutine speedBoostCoroutine;

    void Start()
    {
        currentSpeed = normalSpeed;
    }

    void Update()
    {
        if (controller == null || cameraTransform == null) return;

        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;
        bool isMoving = inputDirection.magnitude >= 0.1f;

        if (isMoving)
        {
            float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(
                transform.eulerAngles.y,
                targetAngle,
                ref rotationVelocity,
                rotationSmoothTime
            );

            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * currentSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (animator != null)
        {
            animator.SetFloat("Speed", isMoving ? 1f : 0f);
            animator.SetBool("Grounded", isGrounded);
        }
    }

    public void ApplySpeedBoost(float duration)
    {
        if (speedBoostCoroutine != null)
        {
            StopCoroutine(speedBoostCoroutine);
        }

        speedBoostCoroutine = StartCoroutine(SpeedBoostRoutine(duration));
    }

    private IEnumerator SpeedBoostRoutine(float duration)
    {
        currentSpeed = boostedSpeed;
        yield return new WaitForSeconds(duration);
        currentSpeed = normalSpeed;
        speedBoostCoroutine = null;
    }
}