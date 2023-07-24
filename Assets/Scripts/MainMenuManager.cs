using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// using UnityEngine.InputSystem;

public class MainMenuManager : MonoBehaviour
{
    Touch touch;
    private void Update()
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
