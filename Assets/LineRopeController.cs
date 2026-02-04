using UnityEngine;

public class LineRopeController : MonoBehaviour
{
    public Transform ropeTop;    // Fixed point at crane top
    public Transform hook;       // Hook object
    public float speed = 2f;     // Hook up/down speed
    public float minY = 0.5f;    // Lowest Y position of hook
    public float maxY = 5f;      // Highest Y position of hook
    public int segments = 20;    // Number of points in the rope

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
        // Hook up/down movement
        float move = 0f;
        if (Input.GetKey(KeyCode.W))
            move = -speed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.S))
            move = speed * Time.deltaTime;

        Vector3 newHookPos = hook.position + Vector3.down * move; // move down for S, up for W
        newHookPos.y = Mathf.Clamp(newHookPos.y, minY, maxY);
        hook.position = newHookPos;

        // Update rope visuals
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
