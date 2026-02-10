using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStack : MonoBehaviour
{

    [SerializeField] Hook hook;
    private void OnTriggerEnter(Collider other)
    {
        

        if (!(other.gameObject.tag == "Cargo"))
        {
            if (other.gameObject.tag == "DropZone" && hook.cargoStack.Count > 0)
            {
                hook.ReleaseCargo();
            }
            return;
        }

        if (hook.cargoStack.Contains(other.gameObject))
            return;

        hook.StackCargo(other.gameObject);

    }
}
