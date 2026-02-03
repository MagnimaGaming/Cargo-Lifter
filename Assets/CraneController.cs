using UnityEngine;

public class CraneController : MonoBehaviour
{
    [Header("Rotation")]
    public float rotationSpeed = 50f;
    bool rotating = true;

    [Header("Grab Cube")]
    public Transform grabCube;
    public float lowerSpeed = 3f;
    public float maxLowerDistance = 5f;

    Vector3 startLocalPos;
    bool lowering;

    void Start()
    {
        startLocalPos = grabCube.localPosition;
        grabCube.gameObject.SetActive(false);
    }

    void Update()
    {
        // Rotate crane
        if (rotating)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }

        // Press Q
        if (Input.GetKeyDown(KeyCode.Q))
        {
            rotating = false;
            grabCube.gameObject.SetActive(true);
            lowering = true;
        }

        // Lower grab cube
        if (lowering)
        {
            grabCube.localPosition += Vector3.down * lowerSpeed * Time.deltaTime;

            if (Vector3.Distance(startLocalPos, grabCube.localPosition) >= maxLowerDistance)
            {
                lowering = false; // stop lowering
            }
        }
    }

    public void Grabbed()
    {
        lowering = false;
    }
}
