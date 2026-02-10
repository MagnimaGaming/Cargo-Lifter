using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField] private GameObject cargoContainer;
    BoxCollider cargoContainerCollider;
    public List<GameObject> cargoStack = new List<GameObject>();

    private float cargoHeight = 1f;
    public float ropeSpeed = 2.0f;
    public float minLength = 1.0f;
    public float maxLength = 20.0f;
    private LineRenderer lineRenderer;
    public Transform trolley;
    public int totalCargoReleased = 0;

    public bool isReleasing;

    [SerializeField] private CraneRotate crane;

    private void Start()
    {
        cargoContainerCollider = cargoContainer.GetComponent<BoxCollider>();
        lineRenderer = GetComponent<LineRenderer>();
        isReleasing = false;
    }

    private void Update()
    {
        RopeControl();
    }

    void RopeControl()
    {
        if (isReleasing)
        {
            crane.StopRotation();
            return;
        }


        float input = Input.GetAxis("Vertical");

        if(input <= 0.01f && input >= -0.01f)
        {
            crane.StartRotation();
        }
        else
        {   
            crane.StopRotation();
        }

        //float distanceFromTrolley = Math.Abs(transform.position.y - trolley.position.y);

        //if (input > 0 && distanceFromTrolley < maxLength)
        //{
        //    transform.Translate(0, input * ropeSpeed * Time.deltaTime, 0);
        //}
        //if (input < 0 && distanceFromTrolley > minLength)
        //{
        //    transform.Translate(0, input * ropeSpeed * Time.deltaTime, 0);
        //}

        transform.Translate(0, input * ropeSpeed * Time.deltaTime, 0);
        
        //clamping y to prevent crossing boundaries
        float minY = trolley.position.y - maxLength;
        float maxY = trolley.position.y - minLength;

        Vector3 pos = transform.position;

        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;

        lineRenderer.SetPosition(0, trolley.position);
        lineRenderer.SetPosition(1, transform.position);
    }


    public void StackCargo(GameObject cargo)
    {
        Rigidbody cargoRb = cargo.GetComponent<Rigidbody>();

        if (cargoRb)
        {
            cargoRb.velocity = Vector3.zero;
            cargoRb.angularVelocity = Vector3.zero;
            cargoRb.isKinematic = true;
        }

        cargo.transform.SetParent(cargoContainer.transform);

        int index = cargoStack.Count;

        Vector3 localPos = Vector3.down * (index * cargoHeight + 0.5f * cargoHeight);
        cargo.transform.localPosition = localPos;
        //cargo.transform.localRotation = Quaternion.identity;

        cargoStack.Add(cargo);


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

    public void ReleaseCargo()
    {
        isReleasing = true;
        cargoContainerCollider.enabled = false;


        totalCargoReleased += cargoStack.Count;
        foreach (GameObject c in cargoStack)
        {
            c.transform.SetParent(null, true);
            Rigidbody rb = c.GetComponent<Rigidbody>();
            c.tag = "ReleasedCargo";


            if (rb)
                rb.isKinematic = false;

        }

        cargoStack.Clear();

        cargoContainerCollider.enabled = true;


        cargoContainerCollider.size = new Vector3(cargoContainerCollider.size.x, 0.1f, cargoContainerCollider.size.z);
        cargoContainerCollider.center = Vector3.zero;

        Invoke("ReleaseComplete", 2f);
    }


    void ReleaseComplete()
    {
        isReleasing = false;
    }

}
