using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject food;
    [SerializeField] GameObject shield;
    [SerializeField] GameObject rocket;

    [SerializeField] Player player;

    [SerializeField] float acceleration = 1f;
    [SerializeField] float maxSpeed = 17.5f;
    [SerializeField] float distance = 0f;
    [SerializeField] float speed = 7f;
    [SerializeField] GameState state;
    [SerializeField] int level;

    // ENUMS =======================
    public float MaxSpeed
    {
        get => maxSpeed;
        set => maxSpeed = value;
    }
    public float Distance
    {
        get => distance;
        set => distance = value;
    }
    public float Speed
    {
        get => speed;
        set => speed = value;
    }
    public enum GameState
    {
        Playing,
        GameOver
    }
    public GameState State
    {
        get { return state; }
        set { state = value; }
    }
    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    // UPDATE & START =======================
    private void Start()
    {
        Application.targetFrameRate = 60;

        state = GameState.Playing;
        level = 1; 
        player = GameObject.Find("Player").GetComponent<Player>();
    }


    private void Update()
    {
        if (state == GameState.GameOver)
        {
            GameOver();
            return;
        }
        distance += speed * Time.deltaTime / 1.2f;
        speed += acceleration * Time.deltaTime / 20f;

        if (speed > maxSpeed)
        {
            speed = maxSpeed;
        }
    }

    // METHODS =======================
    void GameOver()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene("GameOver");
        if (speed > 0.1)
        {
            speed -= acceleration * Time.deltaTime * 15f * level;
        }
        else
        {
            speed = 0;
        }
    }
}

