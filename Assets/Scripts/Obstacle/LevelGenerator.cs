using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 50f;

    private Transform levelPart_Start;
    [SerializeField] private List<Transform> levelPartList;
    [SerializeField] private Player player;

    private Vector3 lastEndPosition;
    private float distanceToSpawn;

    private void Awake()
    {
        levelPart_Start = levelPartList[0].transform;
        lastEndPosition = levelPart_Start.Find("EndPosition").position;
        Transform lastLevelPartTransform = SpawnLevelPart(levelPart_Start, new Vector3(lastEndPosition.x, lastEndPosition.y, -10));
        Debug.Log(levelPart_Start);
    }

    void Update()
    {
        lastEndPosition = levelPart_Start.Find("EndPosition").position;
        Debug.Log(lastEndPosition);
        if (Vector3.Distance(player.transform.position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
        {
            Transform chosenLevelPart = levelPartList[Random.Range(0, levelPartList.Count)];
            Debug.Log("Spawning : " + chosenLevelPart.name);
            Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, new Vector3(lastEndPosition.x, lastEndPosition.y, -10));
            lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
        }
    }

    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }
}
