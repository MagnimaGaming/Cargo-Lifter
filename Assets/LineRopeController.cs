using UnityEngine;

public class LineRopeController : MonoBehaviour
{
    public Transform ropeTop;    
    public Transform hook;       
    public float speed = 2f;     
    public float minY = 0.5f;    
    public float maxY = 5f;     
    public int segments = 20;    

    private LineRenderer lineRenderer;
    private Vector3[] points;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segments;
        points = new Vector3[segments];

        UpdateRope();
    }

    void Update()
    {
        
        float move = 0f;
        if (Input.GetKey(KeyCode.W))
            move = -speed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.S))
            move = speed * Time.deltaTime;

        Vector3 newHookPos = hook.position + Vector3.down * move; 
        newHookPos.y = Mathf.Clamp(newHookPos.y, minY, maxY);
        hook.position = newHookPos;

       
        UpdateRope();
    }

    void UpdateRope()
    {
        for (int i = 0; i < segments; i++)
        {
            float t = (float)i / (segments - 1);
            points[i] = Vector3.Lerp(ropeTop.position, hook.position, t);
        }
        lineRenderer.SetPositions(points);
    }
}
