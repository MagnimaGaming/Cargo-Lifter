using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    private BoxCollider cargoContainerCollider;
    private List<GameObject> cargoStack = new List<GameObject>();

    private float cargoHeight = 1f;
    [SerializeField] private CraneRotate crane;

    public float ropeSpeed = 2.0f;
    public float minLength = 1.0f;
    public float maxLength = 20.0f;
    [SerializeField]private ConfigurableJoint joint;
    private LineRenderer lineRenderer;
    public Transform trolley;

    [SerializeField] private Transform hookObj;

    private void Start()
    {
        cargoContainerCollider = GetComponent<BoxCollider>();
        lineRenderer = GetComponent<LineRenderer>();

    }

    private void Update()
    {
        RopeControl();
    }

    void RopeControl()
    {
        float input = Input.GetAxis("Vertical");
        SoftJointLimit limit = joint.linearLimit;
        limit.limit -= input * ropeSpeed * Time.deltaTime;
        limit.limit = Mathf.Clamp(limit.limit, minLength, maxLength);
        joint.linearLimit = limit;

        lineRenderer.SetPosition(0, trolley.position);
        lineRenderer.SetPosition(1, hookObj.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!(other.gameObject.tag == "Cargo"))
        {
            if (other.gameObject.tag == "DropZone")
                {
                    StartCoroutine(ReleaseCargo());
            }
            return;
        }

        if (cargoStack.Contains(other.gameObject))
            return;

        StackCargo(other.gameObject);

    }

    void StackCargo(GameObject cargo)
    {
        Rigidbody cargoRb = cargo.GetComponent<Rigidbody>();

        if (cargoRb)
        {
            cargoRb.velocity = Vector3.zero;
            cargoRb.angularVelocity = Vector3.zero;
            cargoRb.isKinematic = true;
        }

        cargo.transform.SetParent(transform);

        int index = cargoStack.Count;

        Vector3 localPos = Vector3.down * (index * cargoHeight + 0.5f * cargoHeight);
        cargo.transform.localPosition = localPos;
        cargo.transform.localRotation = Quaternion.identity;

        cargoStack.Add(cargo);

        if (cargoStack.Count == 1)
            crane.StartRotation();


        GrowTrigger();
    }

    void GrowTrigger()
    {
        Vector3 size = cargoContainerCollider.size;
        size.y = cargoStack.Count * cargoHeight;
        cargoContainerCollider.size = size;

        Vector3 center = cargoContainerCollider.center;
        center.y = -size.y * 0.5f + 0.1f;
        cargoContainerCollider.center = center;

    }

    private IEnumerator ReleaseCargo()
    {


        yield return new WaitForSeconds(2f);

        transform.SetParent(null);

        foreach (GameObject c in cargoStack)
        {
            c.transform.SetParent(null);
            Rigidbody rb = c.GetComponent<Rigidbody>();

            if(rb)
                rb.isKinematic = false;
            rb.velocity = new Vector3(0, -1, 0);
        }

        cargoStack.Clear();

        cargoContainerCollider.size = new Vector3(cargoContainerCollider.size.x, 0.1f, cargoContainerCollider.size.z);
        cargoContainerCollider.center = Vector3.zero;
    }
}
