using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Headbob : MonoBehaviour
{

    [Range(0.001f, 0.01f)]
    public float WalkAmount = 0.002f; // Amount for walking
    [Range(0.001f, 0.01f)]
    public float SprintAmount = 0.003f; // Amount for sprinting (increased)

    [Range(1f, 30f)]
    public float WalkFrequency = 10.0f; // Frequency for walking
    [Range(1f, 30f)]
    public float SprintFrequency = 12.0f; // Increased frequency for sprinting

    [Range(10f, 100f)]
    public float WalkSmooth = 10.0f; // Smoothness for walking
    [Range(10f, 100f)]
    public float SprintSmooth = 15.0f; // Increased smoothness for sprinting

    public float GunSwayAmount = 0.5f; // Amount of gun sway relative to camera bob

    Vector3 StartPos;
    Transform[] gunTransforms; // Array to store gun transforms

    void Start()
    {
        StartPos = transform.localPosition;
        gunTransforms = GetComponentsInChildren<Transform>(true); // Find all child transforms (including inactive)
    }

    void Update()
    {
        CheckForHeadbobTrigger();
        StopHeadbob();
    }

    private void CheckForHeadbobTrigger()
    {
        float inputMagnitude = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).magnitude;

        // Check for sprint key (replace "LeftShift" with your actual key)
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);

        if (inputMagnitude > 0)
        {
            if (isSprinting)
            {
                StartHeadBob(true); // Call with sprinting flag
            }
            else
            {
                StartHeadBob(false); // Call with walking flag
            }
        }
    }

    private Vector3 StartHeadBob(bool isSprinting)
    {
        float amount = isSprinting ? SprintAmount : WalkAmount;
        float frequency = isSprinting ? SprintFrequency : WalkFrequency;
        float smooth = isSprinting ? SprintSmooth : WalkSmooth;

        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * frequency) * amount * 1.4f, smooth * Time.deltaTime);
        pos.x += Mathf.Lerp(pos.x, Mathf.Cos(Time.time * frequency / 2f) * amount * 1.6f, smooth * Time.deltaTime);


        transform.localPosition += pos;

        // Apply sway to guns (assuming guns are first two child objects)
        for (int i = 1; i < 3 && i < gunTransforms.Length; i++)
        {
            gunTransforms[i].localPosition += pos * GunSwayAmount;
        }

        return pos;
    }

    private void StopHeadbob()
    {
        if (transform.localPosition == StartPos) return;
        transform.localPosition = Vector3.Lerp(transform.localPosition, StartPos, 1 * Time.deltaTime);
    }
}
