using UnityEngine;

public class CraneRotate : MonoBehaviour
{
    public float startAngle = -90f;
    public float endAngle = 90f;
    public float rotationSpeed = 30f;

    float currentAngle;
    bool isRotating;

    void Start()
    {
        ResetRotation();
        StopRotation();
    }

    void Update()
    {
        if (!isRotating) return;

        currentAngle += rotationSpeed * Time.deltaTime;

        if (currentAngle >= endAngle)
        {
            currentAngle = endAngle;
            isRotating = false;
        }

        transform.localRotation = Quaternion.Euler(0f, currentAngle, 0f);
    }

    public void ResetRotation()
    {
        currentAngle = startAngle;
        transform.localRotation = Quaternion.Euler(0f, currentAngle, 0f);
    }

    public void StartRotation()
    {
        isRotating = true;
    }

    public void StopRotation()
    {
        isRotating = false;
    }
}
