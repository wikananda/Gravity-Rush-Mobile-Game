using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 50f;

    private Transform levelPart_Start;
    Transform lastLevelPartTransform;
    [SerializeField] private List<Transform> levelPartList;
    [SerializeField] private Player player;

    private Vector3 lastEndPosition;
    private Vector3 lastStartPosition;
    private float distanceToSpawn;
    float playerPosX;
    float lastEndPosX;

    // public bool spawnFlag = false;

    private void Awake()
    {
        levelPart_Start = levelPartList[0].transform;
        lastEndPosition = levelPart_Start.Find("EndPosition").transform.TransformPoint(Vector3.zero);
        lastLevelPartTransform = SpawnLevelPart(levelPart_Start, new Vector3(lastEndPosition.x, lastEndPosition.y, -10));
        playerPosX = player.transform.position.x;
        Debug.Log(levelPart_Start);
    }

    void Update()
    {   
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").transform.TransformPoint(Vector3.zero);
        lastEndPosX = lastEndPosition.x;
        distanceToSpawn = Mathf.Abs(lastEndPosX - playerPosX);

        // Debug.Log(lastEndPosition);
        // Debug.Log("Distance: " + Vector3.Distance(player.transform.position, lastEndPosition));
        // Debug.Log("Distance : " + distanceToSpawn);

        if (distanceToSpawn < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
        {
            Transform chosenLevelPart = levelPartList[Random.Range(0, levelPartList.Count)];
            lastStartPosition = chosenLevelPart.Find("StartPosition").transform.TransformPoint(Vector3.zero);
            Debug.Log("Spawning : " + chosenLevelPart.name);
            Debug.Log("last start pos : " + lastStartPosition);
            Debug.Log("Spawn Position : " + new Vector3(lastEndPosition.x + Mathf.Abs(lastStartPosition.x), lastEndPosition.y, -1));
            lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, new Vector3(lastEndPosition.x + Mathf.Abs(lastStartPosition.x), lastEndPosition.y, -10));
            // lastEndPosition = lastLevelPartTransform.Find("EndPosition").transform.TransformPoint(Vector3.zero);
        }
    }

    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }
}
