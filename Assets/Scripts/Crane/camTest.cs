using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camTest : MonoBehaviour
{
    [SerializeField] private GameObject hookCam;
    [SerializeField] private CraneRotate crane;
    [SerializeField] private Hook hook;

    private void Start()
    {
        hookCam.SetActive(false);
    }
    private void Update()
    {
        hookCam.SetActive(!crane.isRotating && !hook.isReleasing);

    }
}
