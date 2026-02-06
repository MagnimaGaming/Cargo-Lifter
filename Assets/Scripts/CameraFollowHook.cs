
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform Hook;
    public Vector3 offset;
    void Update()
    {
        transform.position = Hook.position + offset;
    }
}