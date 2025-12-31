using UnityEngine;
using System.Collections;

public class BossMeteorShower : MonoBehaviour
{
    public GameObject meteorPrefab;
    public GameObject warningPrefab;

    public int meteorCount = 8;
    public float spawnRadius = 6f;
    public float warningTime = 0.75f;
    public float heightOffset = 8f;

    Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void StartMeteorShower()
    {
        StartCoroutine(MeteorRoutine());
    }

    IEnumerator MeteorRoutine()
    {
        for (int i = 0; i < meteorCount; i++)
        {
            Vector2 targetPos = (Vector2)player.position +
                                Random.insideUnitCircle * spawnRadius;

            // Warning indicator
            if (warningPrefab != null)
            {
                Instantiate(warningPrefab, targetPos, Quaternion.identity);
            }

            yield return new WaitForSeconds(warningTime);

            Vector2 spawnPos = targetPos + Vector2.up * heightOffset;
            Instantiate(meteorPrefab, spawnPos, Quaternion.identity);
        }
    }
}
