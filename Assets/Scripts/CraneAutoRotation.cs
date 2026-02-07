using UnityEngine;

public class CraneAutoRotate : MonoBehaviour
{
    public float rotationSpeed = 30f;

    bool finished = false;

    void Start()
    {
        // Force start from -90
        transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
    }

    void Update()
    {
        if (finished) return;

        float currentY = transform.localEulerAngles.y;
        if (currentY > 180f) currentY -= 360f;

        currentY += rotationSpeed * Time.deltaTime;

        if (currentY >= 90f)
        {
            currentY = 90f;
            finished = true; // STOP forever
        }

        transform.localRotation = Quaternion.Euler(0f, currentY, 0f);
    }
}
