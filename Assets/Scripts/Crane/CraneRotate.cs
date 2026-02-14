using UnityEngine;

public class CraneRotate : MonoBehaviour
{
    public float startAngle = -90f;
    public float endAngle = 90f;
    public float rotationSpeed = 10f;

    float currentAngle;
    bool isRotating;
    public bool isCollided = false;

    void Start()
    {
        ResetRotation();
    } 

    void Update()
    {
        if (!isRotating) return;

        currentAngle += rotationSpeed * Time.deltaTime;

        if (currentAngle >= endAngle)
        {
            rotationSpeed *= -1f;
        }
        else if (currentAngle <= startAngle)
        {
            rotationSpeed *= -1f;
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

