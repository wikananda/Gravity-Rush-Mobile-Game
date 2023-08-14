using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRocket : MonoBehaviour
{
    GameUI gameui;
    [SerializeField] int rocketCount = 0;
    [SerializeField] GameObject rocket;

    public int RocketCount
    {
        get => rocketCount;
        set => rocketCount = value;
    }

    private void Start()
    {
        gameui = GameObject.Find("GameUI").GetComponent<GameUI>();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Rocket" && rocketCount < 4)
        {
            rocketCount++;
            gameui.RocketUp(rocketCount);
            gameui.ScoreUp(10);
            Destroy(other.gameObject);
            Debug.Log("Rocket acquired...");
            return;
        }
    }

    public void RocketLaunch()
    {
        Vector3 pos = transform.position;
        pos.x += 1;
        Instantiate(rocket, pos, Quaternion.Euler(0, 0, 90));
    }
}
