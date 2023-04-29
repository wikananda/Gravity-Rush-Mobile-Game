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
    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        distanceText = GameObject.Find("DistanceText").GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.Find("CoinText").GetComponent<TextMeshProUGUI>();
        rocketText = GameObject.Find("RocketText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        int distance = Mathf.FloorToInt(player.distance);
        distanceText.text = distance.ToString() + " m";
    }

    // Increase coin count, called from Player.cs
    public void ScoreUp(int score)
    {
        scoreText.text = score.ToString();
    }

    // Increase rocket count, called from Player.cs
    public void RocketUp(int rocket)
    {
        rocketText.text = rocket.ToString();
    }
}
