using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    GameManager gameManager;
    TextMeshProUGUI distanceText;
    TextMeshProUGUI scoreText;
    TextMeshProUGUI rocketText;
    TextMeshProUGUI speedDebug;
    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        distanceText = GameObject.Find("DistanceText").GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        rocketText = GameObject.Find("RocketText").GetComponent<TextMeshProUGUI>();
        speedDebug = GameObject.Find("SpeedDebug").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        int distance = Mathf.FloorToInt(gameManager.Distance);
        distanceText.text = distance.ToString() + " m";
        speedDebug.text = gameManager.Speed.ToString() + " km/s";
    }

    // Increase coin count, called from Player.cs
    public void ScoreUp(int score)
    {
        int tempScore = int.Parse(scoreText.text);
        score += tempScore;
        scoreText.text = score.ToString();
    }

    // Increase rocket count, called from Player.cs
    public void RocketUp(int rocket)
    {
        rocketText.text = rocket.ToString();
    }
}
