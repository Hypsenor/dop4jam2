using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 3.0f;
    public float sprintSpeed = 6.0f;
    public float crouchSpeed = 1.5f;
    public float gravity = -9.8f;
    public float jumpHeight = 2.0f;
    public float crouchHeight = 0.5f;

    private bool isCrouching;
    private bool isSprinting;
    private Vector3 velocity;
    private CharacterController characterController;

    private float speedMultiplier = 1.0f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float moveSpeed = GetMoveSpeed();

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = transform.TransformDirection(new Vector3(horizontal, 0, vertical)).normalized;
        Vector3 moveVelocity = moveDirection * moveSpeed * speedMultiplier;

        ApplyGravity();

        characterController.Move(moveVelocity * Time.deltaTime + velocity * Time.deltaTime);

        if (characterController.isGrounded)
        {
            velocity.y = -0.5f;

            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        HandleSprint();
        HandleCrouch();
    }

    private void ApplyGravity()
    {
        if (!characterController.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
    }

    private float GetMoveSpeed()
    {
        if (isCrouching)
        {
            return crouchSpeed;
        }
        else if (isSprinting)
        {
            return sprintSpeed;
        }
        else
        {
            return walkSpeed;
        }
    }

    private void HandleSprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
        }
    }

    private void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = !isCrouching;

            characterController.height = isCrouching ? crouchHeight : 2.0f;
        }
    }

    public void ActivateSpeedBuff(float duration)
    {
        StartCoroutine(SpeedBuffCoroutine(duration));
    }

    private IEnumerator SpeedBuffCoroutine(float duration)
    {
        speedMultiplier = 2.0f; // Double the speed
        yield return new WaitForSeconds(duration);
        speedMultiplier = 1.0f; // Reset speed to normal
    }
}