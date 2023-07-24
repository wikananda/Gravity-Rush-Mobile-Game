using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public TextMeshProUGUI pointsText;

    public void RestartGame(){
        SceneManager.LoadScene("MainGame");
    }

    public void QuitGame(){
        SceneManager.LoadScene("MainMenu");
    }

    public void Setup(){
        gameObject.SetActive(true);
        pointsText.text = "Distance: " + Player.dist.ToString();
    }
}
