using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    // ���������� � ��������� ������� ������
    public Transform cameraTransform;

    [Header("Mouse Look Settings")]
    public float mouseSensitivity = 100f;
    float xRotation = 0f;

    [Header("Movement Settings")]
    public float speed = 6.0f;
    public float gravity = -9.81f;
    public float jumpHeight = 2.0f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    [Header("Mouse Look Smoothing")]
    public float smoothTime = 0.05f;
    private Vector2 currentMouseDelta;
    private Vector2 currentMouseDeltaVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

    }

    void LateUpdate()
    {
        HandleMouseLook();
    }

    void Update()
    {
        HandleMovement();
    }


    void HandleMouseLook()
    {
        // ������� ��� ���� (��� ��������)
        Vector2 targetMouseDelta = new Vector2(
            Input.GetAxisRaw("Mouse X"),
            Input.GetAxisRaw("Mouse Y")
        );

        // ������ ������������ ���� ����
        currentMouseDelta = Vector2.SmoothDamp(
            currentMouseDelta,
            targetMouseDelta,
            ref currentMouseDeltaVelocity,
            smoothTime
        );

        // ��������� ��� �������� �� ��������
        xRotation -= currentMouseDelta.y * mouseSensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // ����������� ������� �� ������ (�����-����)
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // ������� ��� ������ (����-������)
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity * Time.deltaTime);
    }

    void HandleMovement()
    {
        // �� �� ����?
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0f)
            velocity.y = -2f;

        // ������� WASD/������
        Vector3 move = transform.right * Input.GetAxis("Horizontal")
                     + transform.forward * Input.GetAxis("Vertical");
        controller.Move(move * speed * Time.deltaTime);

        // �������
        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        // ���������
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}