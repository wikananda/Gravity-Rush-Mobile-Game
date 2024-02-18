using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private BoxCollider2D spawnCollider;

    public int gridX = 10;
    public int gridY = 10;
    public float spacingX = 1.0f;
    public float spacingY = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 startPosition = spawnCollider.bounds.min;
        Debug.Log("startPosition: " + startPosition);
        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                Vector3 spawnPosition = startPosition + new Vector3(x * spacingX, y * spacingY, 0);
                Debug.Log("Spawning coin at " + spawnPosition);

                if (spawnCollider.bounds.Contains(spawnPosition))
                {
                    Debug.Log("Coin spawned inside collider");
                    Instantiate(coinPrefab, spawnPosition, Quaternion.identity, transform);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
