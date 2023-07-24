using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject food;
    [SerializeField] GameObject shield;
    [SerializeField] GameObject rocket;
    Touch touch;

    private void Start()
    {
        Instantiate(food, new Vector3(10, 0, 0), Quaternion.identity);
        Instantiate(shield, new Vector3(12, 0, 0), Quaternion.identity);
        Instantiate(rocket, new Vector3(14, 0, 0), Quaternion.identity);  
    }
    
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

