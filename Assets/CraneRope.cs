using UnityEngine;

public class CraneRope : MonoBehaviour
{
    public float speed = 2f;
    public float maxHeight = 5f;
    public float minHeight = 0f; 

    void Update()
    {
        float move = 0f;
        if (Input.GetKey(KeyCode.W))
            move = speed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.S))
            move = -speed * Time.deltaTime;

        Vector3 newPos = transform.position + Vector3.up * move;
        newPos.y = Mathf.Clamp(newPos.y, minHeight, maxHeight);
        transform.position = newPos;
    }
}
