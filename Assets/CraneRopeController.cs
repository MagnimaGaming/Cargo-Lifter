using UnityEngine;

public class CraneRopeController : MonoBehaviour
{
    [Header("Hook Control")]
    public Transform hook;              // Your hook cube
    public float speed = 2f;            // Up/down speed
    public float minY = 0.5f;           // Lowest hook position
    public float maxY = 5f;             // Highest hook position

    [Header("Grab Settings")]
    public float grabRadius = 0.5f;     // Radius around hook to grab objects
    private GameObject grabbedObject;

    void Update()
    {
        MoveHook();
        HandleGrabRelease();
    }

    /// <summary>
    /// Moves the hook up/down with W/S keys
    /// </summary>
    void MoveHook()
    {
        if (hook == null) return;

        float move = 0f;
        if (Input.GetKey(KeyCode.W)) move = speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S)) move = -speed * Time.deltaTime;

        Vector3 newPos = hook.position + Vector3.down * move; // down for S
        newPos.y = Mathf.Clamp(newPos.y, minY, maxY);
        hook.position = newPos;
    }

    /// <summary>
    /// Handles grabbing with Q and releasing with E
    /// </summary>
    void HandleGrabRelease()
    {
        if (hook == null) return;

        // Grab object
        if (Input.GetKeyDown(KeyCode.Q) && grabbedObject == null)
        {
            Collider[] hits = Physics.OverlapSphere(hook.position, grabRadius);
            foreach (var hit in hits)
            {
                if (hit.CompareTag("Pickable"))
                {
                    grabbedObject = hit.gameObject;
                    grabbedObject.transform.SetParent(hook); // Attach to hook
                    Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
                    if (rb != null) rb.isKinematic = true;
                    break;
                }
            }
        }

        // Release object
        if (Input.GetKeyDown(KeyCode.E) && grabbedObject != null)
        {
            grabbedObject.transform.SetParent(null);
            Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = false;
            grabbedObject = null;
        }
    }

    // Optional: visualize grab radius
    void OnDrawGizmosSelected()
    {
        if (hook != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(hook.position, grabRadius);
        }
    }
}
