using UnityEngine;

public class CraneRotateByStick : MonoBehaviour
{
    public CraneControlStick stick;
    public float rotationSpeed = 60f;

    public float maxAngle = 90f;   // +90 right, -90 left

    void Update()
    {
        if (stick == null) return;

        float input = stick.GetStickValue();
        if (Mathf.Abs(input) < 0.01f) return;

        // Current local Y rotation
        float currentY = transform.localEulerAngles.y;

        // Convert 0–360 to -180 to +180
        if (currentY > 180f)
            currentY -= 360f;

        // Calculate next rotation
        float nextY = currentY + input * rotationSpeed * Time.deltaTime;

        // Clamp between -90 and +90
        nextY = Mathf.Clamp(nextY, -maxAngle, maxAngle);

        // Apply rotation
        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x,
            nextY,
            transform.localEulerAngles.z
        );
    }
}