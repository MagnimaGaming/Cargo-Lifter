using UnityEngine;

public class CraneControlStick : MonoBehaviour
{
    public float stickAngle = 25f;   // Max forward/back angle
    public float returnSpeed = 5f;   // How fast stick returns to center

    float currentInput;

    void Update()
    {
        // Up Arrow = +1, Down Arrow = -1
        if (Input.GetKey(KeyCode.UpArrow))
            currentInput = 1f;
        else if (Input.GetKey(KeyCode.DownArrow))
            currentInput = -1f;
        else
            currentInput = 0f;

        // Target rotation for stick
        Quaternion targetRot = Quaternion.Euler(currentInput * stickAngle, 0f, 0f);

        // Smooth movement
        transform.localRotation = Quaternion.Lerp(
            transform.localRotation,
            targetRot,
            Time.deltaTime * returnSpeed
        );
    }

    // Expose value for crane
    public float GetStickValue()
    {
        return currentInput;
    }
}