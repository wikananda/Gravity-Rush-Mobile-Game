using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    [SerializeField] GameObject food;
    [SerializeField] GameObject shield;
    [SerializeField] GameObject rocket;
    void Start()
    {
        Instantiate(food, new Vector3(10, 0, 0), Quaternion.identity);
        Instantiate(shield, new Vector3(12, 0, 0), Quaternion.identity);
        Instantiate(rocket, new Vector3(14, 0, 0), Quaternion.identity);   
    }
}
