using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SemiCirclePillarSystem : MonoBehaviour
{
    [Header("Crane Reference")]
    public Transform cranePivot;

    [Header("Obstacle Prefab")]
    public GameObject pillarPrefab;

    [Header("Spawn Count")]
    public int obstacleCount = 6;

    [Header("Arc Area")]
    public float minRadius = 4f;
    public float maxRadius = 9f;
    public float groundY = 0f;

    [Header("Anti-Merge Settings")]
    public float extraGap = 0.6f;          // extra safety gap
    public int maxSpawnAttempts = 150;

    [Header("Pillar Height")]
    public float stepHeight = 1.2f;

    [Header("Pillar Fatness (Inspector)")]
    public float baseWidth = 2.5f;

    [Header("Rise Animation")]
    public float riseSpeed = 3f;
    public float delayBeforeRise = 0.25f;

    void Start()
    {
        SpawnPillars();
    }

    void SpawnPillars()
    {
        if (cranePivot == null || pillarPrefab == null)
        {
            Debug.LogError("Crane Pivot or Pillar Prefab not assigned!");
            return;
        }

        List<Vector3> usedPositions = new List<Vector3>();

        int spawned = 0;
        int attempts = 0;

        float pillarRadius = baseWidth * 0.5f;

        while (spawned < obstacleCount && attempts < maxSpawnAttempts)
        {
            attempts++;

            // Random angle inside -90 to +90
            float angle = Random.Range(-90f, 90f);
            Vector3 dir = Quaternion.Euler(0f, angle, 0f) * cranePivot.forward;

            // Random distance
            float radius = Random.Range(minRadius, maxRadius);

            Vector3 spawnPos = cranePivot.position + dir.normalized * radius;
            spawnPos.y = groundY;

            // ---- SIZE-AWARE DISTANCE CHECK ----
            bool tooClose = false;

            foreach (Vector3 pos in usedPositions)
            {
                float requiredDistance =
                    (pillarRadius * 2f) + extraGap;

                if (Vector3.Distance(pos, spawnPos) < requiredDistance)
                {
                    tooClose = true;
                    break;
                }
            }

            if (tooClose) continue;

            // Spawn pillar
            GameObject pillar = Instantiate(pillarPrefab, spawnPos, Quaternion.identity);
            StartCoroutine(HandlePillar(pillar));

            usedPositions.Add(spawnPos);
            spawned++;
        }

        if (spawned < obstacleCount)
        {
            Debug.LogWarning(
                "Not all pillars spawned. Increase radius or reduce extraGap."
            );
        }
    }

    IEnumerator HandlePillar(GameObject pillar)
    {
        Transform mesh = pillar.transform.GetChild(0);
        Renderer rend = mesh.GetComponent<Renderer>();

        int rand = Random.Range(0, 3);

        int steps;
        Color color;

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

        // SCALE (FAT + HEIGHT)
        Vector3 scale = new Vector3(
            baseWidth,
            steps * stepHeight,
            baseWidth
        );

        mesh.localScale = scale;

        // RISE FROM GROUND
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
