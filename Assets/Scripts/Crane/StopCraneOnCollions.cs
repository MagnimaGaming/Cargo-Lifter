using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCraneOnCollions : MonoBehaviour
{

    [SerializeField] private CraneRotate crane;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            return;

        if (collision.gameObject.tag == "Hook" || collision.gameObject.tag == "Cargo")
        {
            crane.StopRotation();
            crane.isCollided = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            return;

        if (collision.gameObject.tag == "Hook" || collision.gameObject.tag == "Cargo")
        {
            crane.isCollided = false;
        }
    }

}
