using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(LineRenderer))]
public class RopeSystem : MonoBehaviour
{
    [Header("--- Setup ---")]
    public Transform trolley;
    public string targetTag = "Pickable";

    [Header("--- Winch Settings ---")]
    public float winchSpeed = 2.0f;
    public float minLength = 1.0f;
    public float maxLength = 20.0f;

    [Header("--- Auto-Grab Settings ---")]
    public float grabRadius = 1.2f;
    public LayerMask grabLayer;
    public float reGrabDelay = 2.0f;

    [Header("--- THE FIX ---")]
    [Tooltip("Increase this value to close the gap manually. Try 0.1 or 0.2")]
    public float gapCorrection = 0.05f; 

    private ConfigurableJoint joint;
    private LineRenderer lineRenderer;
    private List<GameObject> stackList = new List<GameObject>();
    private float nextGrabTime = 0f;

    void Start()
    {
        joint = GetComponent<ConfigurableJoint>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        joint.autoConfigureConnectedAnchor = false;
        joint.anchor = Vector3.zero;
        joint.connectedAnchor = Vector3.zero;
        joint.connectedBody = trolley.GetComponent<Rigidbody>();

        float currentGap = Vector3.Distance(transform.position, trolley.position);
        SoftJointLimit limit = new SoftJointLimit();
        limit.limit = currentGap;
        joint.linearLimit = limit;
    }

    void Update()
    {
        HandleWinch();
        HandleVisuals();

        if (Input.GetKeyDown(KeyCode.E))
        {
            ReleaseAll();
        }
        else if (Time.time >= nextGrabTime)
        {
            CheckAndAutoGrab();
        }
    }

    void CheckAndAutoGrab()
    {
        GameObject currentGrabber = (stackList.Count == 0) ? gameObject : stackList[stackList.Count - 1];

        Vector3 searchPos = currentGrabber.transform.position;
        Collider grabberCol = currentGrabber.GetComponent<Collider>();

        if (grabberCol != null)
            searchPos = new Vector3(searchPos.x, grabberCol.bounds.min.y, searchPos.z);

        Collider[] nearbyObjects = Physics.OverlapSphere(searchPos, grabRadius, grabLayer);

        foreach (Collider col in nearbyObjects)
        {
            if (col.CompareTag(targetTag) &&
                col.gameObject != currentGrabber &&
                !stackList.Contains(col.gameObject))
            {
                GrabObject(currentGrabber, col.gameObject);
                return;
            }
        }
    }

    void GrabObject(GameObject grabber, GameObject target)
    {
        Rigidbody targetRb = target.GetComponent<Rigidbody>();
        Collider targetCol = target.GetComponent<Collider>();
        Collider grabberCol = grabber.GetComponent<Collider>();

        if (targetRb == null || targetCol == null || grabberCol == null) return;

     
        float bottomOfGrabber = grabberCol.bounds.min.y;
        float topOfTarget = targetCol.bounds.max.y;
        float verticalGap = bottomOfGrabber - topOfTarget;

        float totalMoveUp = verticalGap + gapCorrection;

        target.transform.position += Vector3.up * totalMoveUp;

        Vector3 grabberCenter = grabberCol.bounds.center;
        Vector3 targetCenter = targetCol.bounds.center;
        float diffX = grabberCenter.x - targetCenter.x;
        float diffZ = grabberCenter.z - targetCenter.z;
        target.transform.position += new Vector3(diffX, 0, diffZ);

        target.transform.rotation = grabber.transform.rotation;

        FixedJoint link = grabber.AddComponent<FixedJoint>();
        link.connectedBody = targetRb;
        link.autoConfigureConnectedAnchor = true;

        Physics.IgnoreCollision(grabberCol, targetCol, true);

        stackList.Add(target);
        Debug.Log("Grabbed with Correction: " + gapCorrection);
    }

    void ReleaseAll()
    {
        if (stackList.Count == 0) return;

        FixedJoint hookJoint = GetComponent<FixedJoint>();
        if (hookJoint != null) Destroy(hookJoint);

        foreach (GameObject obj in stackList)
        {
            if (obj != null)
            {
                FixedJoint[] joints = obj.GetComponents<FixedJoint>();
                foreach (FixedJoint j in joints) Destroy(j);
            }
        }

        stackList.Clear();
        nextGrabTime = Time.time + reGrabDelay;
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
        if (trolley != null)
        {
            lineRenderer.SetPosition(0, trolley.position);
            lineRenderer.SetPosition(1, transform.position);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (stackList.Count == 0)
        {
            Collider col = GetComponent<Collider>();
            if (col) Gizmos.DrawWireSphere(new Vector3(transform.position.x, col.bounds.min.y, transform.position.z), grabRadius);
        }
    }
}