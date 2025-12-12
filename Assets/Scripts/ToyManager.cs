using UnityEngine;

public class ToyManager : MonoBehaviour
{
    [Header("Assign toy prefabs here")]
    public GameObject dragonToyPrefab;
    public GameObject catToyPrefab;
    public GameObject dogToyPrefab;

    private GameObject currentToy;
    public Transform SimPrefab;

    // UI BUTTON WILL CALL THIS
public void SpawnToy(string petTag)
{
    // Remove old toy if any
    if (currentToy != null)
        Destroy(currentToy);

    GameObject prefabToSpawn = null;

    if (petTag == "Dragon") prefabToSpawn = dragonToyPrefab;
    if (petTag == "Cat")    prefabToSpawn = catToyPrefab;
    if (petTag == "Dog")    prefabToSpawn = dogToyPrefab;

    if (prefabToSpawn == null)
    {
        Debug.LogError("No toy prefab assigned for tag: " + petTag);
        return;
    }

    // Spawn in front of camera
    Vector3 spawnPos = Camera.main.transform.position + Camera.main.transform.forward * 0.5f;
    currentToy = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity,SimPrefab);

    // Give the toy a reference back to this manager
    Toy toyScript = currentToy.GetComponent<Toy>();
    if (toyScript != null)
    {
        toyScript.SetManager(this);
    }

    // Corrected Debug.Log
    Debug.Log("Spawning toy for: " + petTag + " prefab=" + prefabToSpawn.name);
}
    // Called by toy when pet touches it
    public void DespawnToy(GameObject toy)
    {
        Destroy(toy);
        currentToy = null;
    }

}
