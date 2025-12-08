using UnityEngine;
using System.Collections;

public class ParticleSpawner : MonoBehaviour
{
    [Header("Assign particle prefabs (prefabs should have ParticleSystem root)")]
    public GameObject heartPrefab;    // HeartParticles prefab
    public GameObject sparklePrefab;  // SparkleParticles prefab

    /// <summary>
    /// Spawn particle prefab at world position (or at transform position)
    /// and auto-destroy after its duration.
    /// </summary>
    public GameObject SpawnOnce(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        if (prefab == null) return null;
        GameObject go = Instantiate(prefab, pos, rot);
        var ps = go.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            // Calculate total lifetime we should wait before destroying
            float total = ps.main.duration + ps.main.startLifetime.constantMax;
            Destroy(go, total + 0.1f);
        }
        else
        {
            // If prefab root isn't ParticleSystem, try find child system
            var child = go.GetComponentInChildren<ParticleSystem>();
            if (child != null)
            {
                float total = child.main.duration + child.main.startLifetime.constantMax;
                Destroy(go, total + 0.1f);
            }
            else
            {
                Destroy(go, 2f);
            }
        }
        return go;
    }

    // Convenience wrappers
    public void SpawnHearts(Vector3 position)
    {
        SpawnOnce(heartPrefab, position, Quaternion.identity);
    }

    public void SpawnSparkles(Vector3 position)
    {
        SpawnOnce(sparklePrefab, position, Quaternion.identity);
    }
}

