using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camBlend : MonoBehaviour
{
    [SerializeField] GameObject rotationCamOne;
    [SerializeField] GameObject rotationCamTwo;

    [SerializeField] GameObject hookCam;

    [SerializeField] private Transform rotatingRod;
    [SerializeField] CraneRotate crane;
    [SerializeField] Hook hook;
    public float camChangePoint = -40f;


    private void Start()
    {
        rotationCamOne.SetActive(true);
        rotationCamTwo.SetActive(false);
        hookCam.SetActive(false);
    }


    private void Update()
    {

        if (!crane.isRotating && !hook.isReleasing)
        {
            hookCam.SetActive(true);
        }
        else
        {
            hookCam.SetActive(false);
        }

        Debug.Log(rotatingRod.localEulerAngles.y <= camChangePoint && crane.isRotating);


        float yAngle = rotatingRod.localEulerAngles.y;

        if (yAngle > 180) yAngle -= 360;

        if (yAngle <= camChangePoint && crane.isRotating)
        {
            rotationCamOne.SetActive(true);
            rotationCamTwo.SetActive(false);
        }
        else if (yAngle > camChangePoint && crane.isRotating)
        {
            rotationCamTwo.SetActive(true);
            rotationCamOne.SetActive(false);
        }
    }
}
