using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class WeaponSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [Tooltip("Weapon prefab to spawn")]
    public GameObject weaponPrefab;
    [Tooltip("Spawn points transforms")]
    public Transform[] spawnPoints;
    [Tooltip("Vertical offset above spawn point")]
    public float spawnHeightOffset = 1.5f;
    [Tooltip("Time in seconds between respawn checks")]
    public float respawnTime = 30f;
    [Tooltip("Minimum allowed distance from last spawn to respawn")]
    public float respawnDistance = 1f;

    // Track last spawned instance per point
    private Dictionary<Transform, GameObject> lastSpawn = new Dictionary<Transform, GameObject>();

    void Start()
    {
        foreach (var pt in spawnPoints)
        {
            SpawnAt(pt);
            StartCoroutine(RespawnRoutine(pt));
        }
    }

    private void SpawnAt(Transform point)
    {
        Vector3 spawnPos = point.position + Vector3.up * spawnHeightOffset;
        var instance = Instantiate(weaponPrefab, spawnPos, point.rotation);
        lastSpawn[point] = instance;
    }

    private IEnumerator RespawnRoutine(Transform point)
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            GameObject prev;
            if (lastSpawn.TryGetValue(point, out prev) && prev != null)
            {
                float dist = Vector3.Distance(prev.transform.position, point.position + Vector3.up * spawnHeightOffset);
                if (dist < respawnDistance)
                    continue;
            }
            SpawnAt(point);
        }
    }
}