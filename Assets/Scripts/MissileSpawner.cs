using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    [SerializeField] GameObject missilePrefab;

    BoxCollider2D missileSpawnArea;
    // Start is called before the first frame update
    void Start()
    {
        missileSpawnArea = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SpawnMissile();
    }

    public void SpawnMissile()
    {
        float x = Random.Range(missileSpawnArea.bounds.min.x, missileSpawnArea.bounds.max.x);
        float y = Random.Range(missileSpawnArea.bounds.min.y, missileSpawnArea.bounds.max.y);
        Instantiate(missilePrefab, new Vector3(x, y, 0), Quaternion.Euler(0, 0, 90));
    }
}
