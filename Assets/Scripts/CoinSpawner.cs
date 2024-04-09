using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private List<Collider2D> spawnColliders;

    // private List<GameObject> coins = new List<GameObject>();

    public int gridX = 10;
    public int gridY = 1;
    public float spacingX = 1.0f;
    public float spacingY = 1.0f;
    
    public void SpawnCoins(Collider2D spawnCollider)
    {
        Vector3 startPosition = spawnCollider.bounds.min;
        Debug.Log("startPosition: " + startPosition);
        GameObject coinContainer = new GameObject("Coins");
        coinContainer.transform.position = spawnCollider.transform.position;
        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                Vector3 spawnPosition = startPosition + new Vector3(x * spacingX, y * spacingY, 0);
                Debug.Log("Spawning coin at " + spawnPosition);

                if (spawnCollider.bounds.Contains(spawnPosition))
                {
                    Debug.Log("Coin spawned inside collider");
                    GameObject newCoin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity, coinContainer.transform);
                }
            }
        }
        coinContainer.transform.rotation = spawnCollider.transform.rotation;
    }
    // Start is called before the first frame update
    void Start()
    {
        GameObject initCoin = Instantiate(coinPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        initCoin.SetActive(false);

        float coinSize = initCoin.GetComponent<SpriteRenderer>().bounds.size.x;
        Debug.Log("Coin size: " + coinSize);

        System.Random rand = new System.Random();
        int spawnerCount = rand.Next(0, spawnColliders.Count + 1);
        Debug.Log("Spawner count: " + spawnerCount);


        List<int> indexToSpawned = new List<int>();
        
        for (int i = 0; i < spawnerCount; i++)
        {
            int spawnerIndex = rand.Next(0, spawnColliders.Count);
            Debug.Log("Spawner index: " + spawnerIndex);
            if (indexToSpawned.Count < spawnerCount)
            {
                while(!indexToSpawned.Contains(spawnerIndex))
                {
                    gridX = (int)(spawnColliders[spawnerIndex].bounds.size.x / coinSize);
                    indexToSpawned.Add(spawnerIndex);
                    SpawnCoins(spawnColliders[spawnerIndex]);
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
