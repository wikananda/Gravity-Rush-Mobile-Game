using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemTester : MonoBehaviour
{
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject player;
    [SerializeField] GameObject missilePrefab;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.position = player.transform.position;

        if (Input.GetKeyDown(KeyCode.Space))
            SpawnMissile();
    }

    public void SpawnCoin()
    {
        Instantiate(coinPrefab, new Vector3(-2, 0, 0), Quaternion.identity);
        Instantiate(coinPrefab, new Vector3(0.5f, 0, 0), Quaternion.identity);
        Instantiate(coinPrefab, new Vector3(3, 0, 0), Quaternion.identity);
    }

    public void SpawnMissile()
    {
        Instantiate(missilePrefab, new Vector3(15,3.6f,0), Quaternion.Euler(0, 0, 90));
        Instantiate(missilePrefab, new Vector3(12,0,0), Quaternion.Euler(0, 0, 90));
        Instantiate(missilePrefab, new Vector3(14,-3.5f,0), Quaternion.Euler(0, 0, 90));
    }
}
