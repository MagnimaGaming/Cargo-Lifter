using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swcam : MonoBehaviour
{
    public GameObject cam1;
    public GameObject cam2;
    int camnum ;

    public void ChangeCam()
    {
        GetComponent<Animator>().SetTrigger("Change");
    }
    public void CamManager()
    {
        if (camnum == 0)
        {
            Camera2();
            camnum = 1;
        }
        else 
        {
            camnum = 0;
            Camera1();
        }
    }
    void Camera1()
    {
        cam1.SetActive(true);
        cam2.SetActive(false);
    }

    void Camera2()
    {
        cam1.SetActive(false);
        cam2.SetActive(true);
    }

}
