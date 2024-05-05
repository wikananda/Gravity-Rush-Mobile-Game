using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    Missile missile;
    [SerializeField] GameObject missilePrefab;

    BoxCollider2D missileSpawnArea;

    [SerializeField] float rocketSpeed = 2f;
    [SerializeField] float missileTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        missile = missilePrefab.GetComponent<Missile>();
        missileSpawnArea = GetComponent<BoxCollider2D>();
        missileTimer = Random.Range(10f, 25f);
        missile.Speed = rocketSpeed;
    }

    void Update()
    {
        if(missileTimer <= 0)
        {
            missileTimer = Random.Range(10f, 25f);
            SpawnMissile();
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
        Instantiate(missilePrefab, new Vector3(x, y, transform.position.z), Quaternion.Euler(0, 0, 90));
    }
}
