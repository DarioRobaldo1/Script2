using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    private CharacterController CC;
    [SerializeField] private float speed = 100f;
    [SerializeField] private float speedMultiplier = 1.5f;
    [SerializeField] private float jumpHight = 9f;
    [SerializeField] private float mouseSensitivity = 10f;
    [SerializeField] private Transform head;
    private Vector2 rotationRef;
    private float currentJumpForce;

    private static bool hasJumped => Input.GetKeyDown(KeyCode.Space);

    private static bool isSprinting => Input.GetKey(KeyCode.LeftShift);

    public static bool isShooting => Input.GetKey(KeyCode.Mouse0);

    private static Vector2 InputMovement => new(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    private static Vector2 InputRotation => new(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

    private Vector3 MovementVector => ((transform.right * InputMovement.x) + (transform.forward * InputMovement.y)).normalized;

    private static float GetDelta(float n) => n * Time.deltaTime;


    private void Awake()
    {
        CC = GetComponent<CharacterController>();
    }
    private void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        Move();
        Rotate();
    }
    private void Move()
    {
        float currentSpeed = isSprinting ? speed * speedMultiplier : speed;
        Vector3 movementVector = MovementVector * GetDelta(currentSpeed);
        Vector3 jumpVector = transform.up * GetDelta(CalculateJumpForce());
        CC.Move(movementVector + jumpVector);
    }
    private void Rotate()

    {
        rotationRef += InputRotation * Time.deltaTime * mouseSensitivity;
        rotationRef.y = Mathf.Clamp(rotationRef.y, -90f, 90f);

        transform.localRotation = Quaternion.Euler(0,rotationRef.x, 0);
        head.localRotation = Quaternion.Euler(- rotationRef.y, 0, 0);
    }
    private float CalculateJumpForce()
    {
        if(!CC.isGrounded)
        {
            currentJumpForce -= 1f / jumpHight;
        }
        else if (hasJumped)
        {
            currentJumpForce = jumpHight;
        }
        return currentJumpForce;
    }
    

    
}
