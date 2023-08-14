using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    public TextMeshProUGUI pointsText;

    public void RestartGame(){
        SceneManager.LoadScene("MainGame");
    }

    public void QuitGame(){
        SceneManager.LoadScene("MainMenu");
    }

    public void Setup(){
        gameObject.SetActive(true);
        pointsText.text = "Distance: " + gameManager.Distance.ToString();
    }
}
