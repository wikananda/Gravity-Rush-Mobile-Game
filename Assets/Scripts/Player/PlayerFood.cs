using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFood : MonoBehaviour
{
    GameManager gameManager;
    GameUI gameui;
    [SerializeField] int foodEaten = 0;
    
    public int FoodEaten
    {
        get => foodEaten;
        set => foodEaten = value;
    }

    void Start()
    {
        gameui = GameObject.Find("GameUI").GetComponent<GameUI>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        foodEaten = 0;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Food")
        {
            foodEaten++;
            gameui.ScoreUp(10 * gameManager.Level);
            Destroy(other.gameObject);
            return;
        }
    }
}
