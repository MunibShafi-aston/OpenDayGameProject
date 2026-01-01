using UnityEngine;
using System.Collections.Generic;

public class chunkGenerator : MonoBehaviour
{
    public Transform player;

    [Header("Chunk Settings")]
    public int chunkSize = 5;
    public int renderDistance = 2;

    [Header("Chunk Prefabs")]
    public GameObject[] chunkPrefabs;

    private Dictionary<Vector2Int, GameObject> activeChunks = new Dictionary<Vector2Int, GameObject>();

    void Update()
    {
        Vector2Int playerChunk = GetChunkCoord(player.position);

        for (int x = -renderDistance; x <= renderDistance; x++)
        {
            for (int y = -renderDistance; y <= renderDistance; y++)
            {
                Vector2Int chunkCoord = playerChunk + new Vector2Int(x, y);

                if (!activeChunks.ContainsKey(chunkCoord))
                {
                    CreateChunk(chunkCoord);
                }
            }
        }
    }

    Vector2Int GetChunkCoord(Vector2 position)
    {
        return new Vector2Int(
            Mathf.FloorToInt(position.x / chunkSize),
            Mathf.FloorToInt(position.y / chunkSize)
        );
    }

    void CreateChunk(Vector2Int chunkCoord)
    {
        GameObject prefab = chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];

        Vector3 worldPosition = new Vector3(
            chunkCoord.x * chunkSize,
            chunkCoord.y * chunkSize,
            0
        );

        GameObject chunk = Instantiate(prefab, worldPosition, Quaternion.identity, transform);
        chunk.name = $"Chunk_{chunkCoord.x}_{chunkCoord.y}";

        activeChunks.Add(chunkCoord, chunk);
    }
}
