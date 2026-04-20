using UnityEngine;

public class ThirdPersonCameraFollow : MonoBehaviour
{
    [Header("References")]
    public Transform target;

    [Header("Distance Settings")]
    public float distance = 4f;
    public float height = 2f;

    [Header("Mouse Look")]
    public float mouseSensitivity = 3f;
    public float minPitch = -30f;
    public float maxPitch = 60f;

    [Header("Smooth")]
    public float followSmoothSpeed = 10f;

    private float yaw;
    private float pitch;

    void Start()
    {
        if (target != null)
        {
            yaw = target.eulerAngles.y;
            pitch = 15f;
        }

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // 鼠标控制视角
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);

        Vector3 desiredPosition = target.position + Vector3.up * height - rotation * Vector3.forward * distance;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSmoothSpeed * Time.deltaTime);

        // 相机看向角色上半身
        transform.LookAt(target.position + Vector3.up * height);
    }
}