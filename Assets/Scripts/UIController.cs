using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    Player player;
    TextMeshProUGUI distanceText;
    TextMeshProUGUI coinText;
    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        distanceText = GameObject.Find("DistanceText").GetComponent<TextMeshProUGUI>();
        coinText = GameObject.Find("CoinText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        int distance = Mathf.FloorToInt(player.distance);
        distanceText.text = distance.ToString() + " m";
    }

    // Increase coin count, called from Player.cs
    public void CoinUp(int coin)
    {
        coinText.text = coin.ToString();
    }
}
