using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Touch touch;
    private void update()
    {
        if(Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                SceneManager.LoadScene("MainGame");
            }
        }
    }
}
