using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    [Header("Transform")]
    public float gridX = 10;
    public float gridY = 2;
    public float spacingX = 1.0f;
    public float spacingY = 0.5f;
    public float rotation = 0.0f;

    [Header("Coin Prefab")]
    [SerializeField] private GameObject coinPrefab;
    public float colliderRadius = 1f;
    public float coinSize = 1f;

    [Header("Spawner")]
    public bool addStart = false;
    public bool addEnd = false;


    public void SpawnCoins()
    {
        List<GameObject> coins = new List<GameObject>();    
        coinPrefab.GetComponent<CircleCollider2D>().radius = colliderRadius;
        coinPrefab.transform.localScale = new Vector3(coinSize, coinSize, coinSize);

        Vector3 startPosition = new Vector3(0, 0, 0);
        Debug.Log("startPosition: " + startPosition);
        GameObject coinContainer = new GameObject("Coins");
        coinContainer.transform.position = startPosition;
        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                Vector3 spawnPosition = startPosition + new Vector3(x * spacingX, y * spacingY, 0);
                Debug.Log("Spawning coin at " + spawnPosition);
                GameObject newCoin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity, coinContainer.transform);
                coins.Add(newCoin);
            }
        }

        Debug.Log("coins 0: " + coins[0].transform.position);
        Debug.Log("coins end: " + coins[coins.Count - 1].transform.position);

        Vector3 coinStartPosition = coins[0].transform.position;
        Vector3 coinEndPosition = coins[coins.Count - 1].transform.position;

        double endStartPositionX = Math.Sqrt(spacingX * spacingX - ((spacingY/2) * (spacingY/2)));

        if (addStart)
        {
            Vector3 spawnStartPosition = new Vector3 (coinStartPosition.x - (float)endStartPositionX,  
                                                    coinStartPosition.y + (spacingY / 2),
                                                    coinStartPosition.z);
            GameObject start = Instantiate(coinPrefab, spawnStartPosition, Quaternion.identity, coinContainer.transform);
            coins.Add(start);
        }
        if (addEnd)
        {
            Vector3 spawnEndPosition = new Vector3 (coinEndPosition.x + (float)endStartPositionX,
                                                    coinEndPosition.y - (spacingY / 2),
                                                    coinEndPosition.z);
            GameObject end = Instantiate(coinPrefab, spawnEndPosition, Quaternion.identity, coinContainer.transform);
            coins.Add(end);
        }

        Debug.Log("Coins count: " + coins.Count);
        coinContainer.transform.rotation = Quaternion.Euler(0, 0, rotation);
    }
}
