using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;
    public Transform head;
    public Camera camera;

    [Header("Configurations")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float mouseSensitivity = 2f;

    private float verticalRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMouseLook();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        head.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void MovePlayer()
    {
        Vector3 newVelocity = Vector3.up * rb.velocity.y;
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        newVelocity.x = Input.GetAxis("Horizontal") * speed;
        newVelocity.z = Input.GetAxis("Vertical") * speed;

        rb.velocity = transform.TransformDirection(newVelocity);
    }
}
