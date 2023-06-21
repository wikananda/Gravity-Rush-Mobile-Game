using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    Player player;
    TextMeshProUGUI distanceText;
    TextMeshProUGUI scoreText;
    TextMeshProUGUI rocketText;
    TextMeshProUGUI speedDebug;
    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        distanceText = GameObject.Find("DistanceText").GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        rocketText = GameObject.Find("RocketText").GetComponent<TextMeshProUGUI>();
        speedDebug = GameObject.Find("SpeedDebug").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        int distance = Mathf.FloorToInt(player.distance);
        distanceText.text = distance.ToString() + " m";
        speedDebug.text = player.speed.ToString() + " km/s";
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
