using UnityEngine;

public class HookController : MonoBehaviour
{
    private GameObject grabbedObject = null;

    void Update()
    {
        // Grab object
        if (Input.GetKeyDown(KeyCode.Q) && grabbedObject == null)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, 0.5f); // small radius
            foreach (var hit in hits)
            {
                if (hit.CompareTag("Pickable"))
                {
                    grabbedObject = hit.gameObject;
                    grabbedObject.transform.SetParent(transform); // attach to hook
                    Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
                    if (rb != null) rb.isKinematic = true; // freeze physics while held
                    break;
                }
            }
        }

        // Release object
        if (Input.GetKeyDown(KeyCode.E) && grabbedObject != null)
        {
            grabbedObject.transform.SetParent(null);
            Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = false; // enable physics
            grabbedObject = null;
        }
    }
}
