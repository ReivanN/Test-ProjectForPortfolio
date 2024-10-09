using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Vector3 spawnPosition = Vector3.zero;
    public Quaternion spawnRotation = Quaternion.identity;
    private void Awake()
    {
        if (objectToSpawn != null)
        {
            Instantiate(objectToSpawn, spawnPosition, spawnRotation);
        }
    }
}
