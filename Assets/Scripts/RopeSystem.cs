using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(LineRenderer))]
public class RopeSystem : MonoBehaviour
{
    [Header("Setup")]
    public Transform trolley;        
    public string targetTag = "Pickable";

    [Header("Winch Settings")]
    public float winchSpeed = 2.0f;
    public float minLength = 1.0f;
    public float maxLength = 15.0f;

    [Header("Hook Settings")]
    public float grabRadius = 1.5f;

    
    private ConfigurableJoint joint;
    private LineRenderer lineRenderer;
    private FixedJoint grabJoint;
    private Rigidbody currentLoad;

    void Start()
    {
        joint = GetComponent<ConfigurableJoint>();
        lineRenderer = GetComponent<LineRenderer>();

        
        joint.connectedBody = trolley.GetComponent<Rigidbody>();
        joint.autoConfigureConnectedAnchor = false;
        joint.anchor = Vector3.zero;
        joint.connectedAnchor = Vector3.zero;
    }

    void Update()
    {
        HandleWinch();
        HandleVisuals();
        HandleGrabInput();
    }

    
    void HandleWinch()
    {
        float input = Input.GetAxis("Vertical"); 

       
        SoftJointLimit limit = joint.linearLimit;

        
        limit.limit -= input * winchSpeed * Time.deltaTime;

        
        limit.limit = Mathf.Clamp(limit.limit, minLength, maxLength);

       
        joint.linearLimit = limit;
    }

    
    void HandleVisuals()
    {
        lineRenderer.SetPosition(0, trolley.position); 
        lineRenderer.SetPosition(1, transform.position); 
    }

    void HandleGrabInput()
    {
        if (Input.GetKeyDown(KeyCode.Q)) TryGrab();
        if (Input.GetKeyDown(KeyCode.E)) Release();
    }

    void TryGrab()
    {
        if (grabJoint != null) return; 

        Collider[] colliders = Physics.OverlapSphere(transform.position, grabRadius);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag(targetTag) && col.GetComponent<Rigidbody>())
            {
                
                currentLoad = col.GetComponent<Rigidbody>();

                
                grabJoint = gameObject.AddComponent<FixedJoint>();
                grabJoint.connectedBody = currentLoad;

                Debug.Log("Grabbed: " + col.name);
                break;
            }
        }
    }

    void Release()
    {
        if (grabJoint == null) return;

        Destroy(grabJoint);
        grabJoint = null;
        currentLoad = null;
        Debug.Log("Released");
    }

    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, grabRadius);
    }
}