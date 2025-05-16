using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [Header("Camera Settings")]
    public float sensitivityX = 2.0f;
    public float sensitivityY = 2.0f;
    public Transform playerBody;

    [Header("Clamping")]
    public float minimumX = -90f;
    public float maximumX = 90f;

    private float rotationX = 0;

    private void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleRotation();
    }

    private void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityY;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);

        transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);


    }
}
