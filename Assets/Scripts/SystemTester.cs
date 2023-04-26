using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemTester : MonoBehaviour
{
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.position = player.transform.position;
    }

    public void SpawnCoin()
    {
        Instantiate(coinPrefab, new Vector3(-2, 0, 0), Quaternion.identity);
        Instantiate(coinPrefab, new Vector3(0.5f, 0, 0), Quaternion.identity);
        Instantiate(coinPrefab, new Vector3(3, 0, 0), Quaternion.identity);
    }
}
