using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject CountdownShow;
    public TextMeshProUGUI countDownText;
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }
    public IEnumerator countDown(){
        for(int i = 0 ; i < 3 ; i++){
            countDownText.text = (3-i).ToString();
            Debug.Log(countDownText.text);
            yield return new WaitForSecondsRealtime(1f);
        }
        CountdownShow.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        CountdownShow.SetActive(true);
        StartCoroutine(countDown());
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainGame");
    }
}
