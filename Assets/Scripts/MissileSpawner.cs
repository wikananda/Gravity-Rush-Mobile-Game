using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    Missile missile;
    GameManager gameManager;
    [SerializeField] GameObject missilePrefab;


    BoxCollider2D missileSpawnArea;

    [SerializeField] float rocketSpeed = 2f;
    [SerializeField] float missileTimer = 0f;
    [SerializeField] float acceleration = 1f;
    bool consecutive = false;
    int consecutiveMissiles = 3;
    int num2Spawned = 1;
    float spawnDelay = 2f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        missile = missilePrefab.GetComponent<Missile>();
        missileSpawnArea = GetComponent<BoxCollider2D>();
        missileTimer = Random.Range(10f, 12f);
        rocketSpeed = missile.Speed;
    }

    void Update()
    {

        if(missileTimer <= 0)
        {
            if (Random.value < 0.3f) // 30% chance of consecutive missile
            {
                consecutive = true;
                // make random integer between 3 and 7 inclusive
                consecutiveMissiles = Random.Range(2, 6);
            }
            if (Random.value < 0.5f)
            {
                num2Spawned = Random.Range(2, 4);
            }
            // while (num2Spawned > 0)
            // {
            //     if (spawnDelay <= 0)
            //     {
            //         Debug.Log("Spawning");
            //         SpawnMissile();
            //         num2Spawned--;
            //         spawnDelay = 2f;
            //     }
            //     else
            //     {
            //         spawnDelay -= Time.deltaTime;
            //     }
            // }
            SpawnMissile();
            missileTimer = Random.Range(10f, 12f);
        }
        else
        {
            missileTimer -= Time.deltaTime;
        }
    }

    public void SpawnMissile()
    {
        float x = Random.Range(missileSpawnArea.bounds.min.x, missileSpawnArea.bounds.max.x);
        float y = Random.Range(missileSpawnArea.bounds.min.y, missileSpawnArea.bounds.max.y);
        if (consecutive)
            {
            for (float i = 0; i < consecutiveMissiles * 4.35f; i+=4.35f)
            {
                Instantiate(missilePrefab, new Vector3(x + (i + 1.5f * gameManager.Level), y, transform.position.z), Quaternion.Euler(0, 0, 90));
            }
            consecutive = false;
            return;
        }
        Instantiate(missilePrefab, new Vector3(x, y, transform.position.z), Quaternion.Euler(0, 0, 90));
        num2Spawned = 1;
    }
}
