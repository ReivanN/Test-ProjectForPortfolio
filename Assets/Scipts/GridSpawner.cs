using UnityEngine;
using System.Collections.Generic;

public class GridSpawner : MonoBehaviour
{
    public GameObject[] blockPrefabs; 
    public float[] spawnChances;
    public int columns = 4;
    public int rows = 5;
    public float columnSpacing = 2f;
    public float rowSpacing = 1.5f;
    public Vector3 startPosition = Vector3.zero;

    private List<GameObject> spawnedBlocks = new List<GameObject>();
    public bool win = false;

    public int maxHp = 300;
    public float minDistance = 1f; 
    public float maxDistance = 5f;

    public Canvas canvas;
    public GameObject Win;

    void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Vector3 position = new Vector3(x * columnSpacing, -y * rowSpacing, 0) + startPosition;
                GameObject blockPrefab = GetRandomPrefab();
                GameObject block = Instantiate(blockPrefab, position, Quaternion.identity);
                spawnedBlocks.Add(block);
                BlockStandart blockComponent = block.GetComponent<BlockStandart>();
                float distance = Vector3.Distance(position, startPosition);
                blockComponent.HP = CalculateHP(distance);
            }
        }
    }

    GameObject GetRandomPrefab()
    {
        float randomValue = Random.Range(0f, 1f);
        float cumulative = 0f;
        for (int i = 0; i < blockPrefabs.Length; i++)
        {
            cumulative += spawnChances[i];
            if (randomValue < cumulative)
            {
                return blockPrefabs[i];
            }
        }
        return blockPrefabs[0];
    }
    int CalculateHP(float distance)
    {
        float normalizedDistance = Mathf.Clamp01((distance - minDistance) / (maxDistance - minDistance));
        return Mathf.Max(5, (int)(maxHp * (1 - normalizedDistance)));
    }
    public void OnBlockDestroyed(GameObject block)
    {
        spawnedBlocks.Remove(block);

        if (spawnedBlocks.Count == 0)
        {
            Time.timeScale = 0f;
            canvas.gameObject.SetActive(true);
            Win.SetActive(true);
            win = true;
            Debug.Log("You win!");
        }
    }
}