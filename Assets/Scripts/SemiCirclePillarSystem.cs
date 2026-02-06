using UnityEngine;
using System.Collections;

public class SemiCirclePillarSystem : MonoBehaviour
{
    [Header("Crane Reference")]
    public Transform cranePivot;

    [Header("Obstacle")]
    public GameObject pillarPrefab;

    [Header("Arc Settings")]
    public int obstacleCount = 9;
    public float radius = 6f;
    public float groundY = 0f;

    [Header("Pillar Settings")]
    public float stepHeight = 1.2f;
    public float riseSpeed = 3f;
    public float delayBeforeRise = 0.3f;

    void Start()
    {
        SpawnPillars();
    }

    void SpawnPillars()
    {
        float angleStep = 180f / (obstacleCount - 1);

        for (int i = 0; i < obstacleCount; i++)
        {
            float angle = -90f + i * angleStep;

            Vector3 dir =
                Quaternion.Euler(0f, angle, 0f) * cranePivot.forward;

            Vector3 spawnPos =
                cranePivot.position + dir.normalized * radius;

            spawnPos.y = groundY;

            GameObject pillar = Instantiate(
                pillarPrefab,
                spawnPos,
                Quaternion.identity
            );

            

            StartCoroutine(HandlePillar(pillar));
        }
    }

    IEnumerator HandlePillar(GameObject pillar)
    {
        Transform mesh = pillar.transform.GetChild(0); // Mesh child
        Renderer rend = mesh.GetComponent<Renderer>();

        // RANDOM TYPE
        int rand = Random.Range(0, 3);

        int steps = 1;
        Color color = Color.green;

        if (rand == 0)
        {
            steps = 1;
            color = Color.green;
        }
        else if (rand == 1)
        {
            steps = 2;
            color = Color.yellow;
        }
        else
        {
            steps = 3;
            color = Color.red;
        }

        rend.material.color = color;

        // SCALE HEIGHT
        Vector3 scale = mesh.localScale;
        scale.y = steps * stepHeight;
        mesh.localScale = scale;

        // START BELOW GROUND
        Vector3 startPos = pillar.transform.position;
        Vector3 targetPos = startPos + Vector3.up * (scale.y / 2f);

        yield return new WaitForSeconds(delayBeforeRise);

        while (Vector3.Distance(pillar.transform.position, targetPos) > 0.01f)
        {
            pillar.transform.position = Vector3.MoveTowards(
                pillar.transform.position,
                targetPos,
                riseSpeed * Time.deltaTime
            );
            yield return null;
        }
    }
}
