using UnityEngine;

public class HookController : MonoBehaviour
{
    private GameObject grabbedObject = null;

    void Update()
    {
        //fpor Q grab and E release 
        if (Input.GetKeyDown(KeyCode.Q) && grabbedObject == null)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, 0.5f); 
            foreach (var hit in hits)
            {
                if (hit.CompareTag("Pickable"))
                {
                    grabbedObject = hit.gameObject;
                    grabbedObject.transform.SetParent(transform); 
                    Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
                    if (rb != null) rb.isKinematic = true;    
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && grabbedObject != null)
        {
            grabbedObject.transform.SetParent(null);
            Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = false;         
            grabbedObject = null;
        }
    }
}
