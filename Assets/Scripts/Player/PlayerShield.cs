using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    // Shield properties
    [SerializeField] bool shield = false;
    [SerializeField] float shieldDuration = 5f;
    [SerializeField] int shieldCount = 1;

    // GameUI
    GameUI gameui;

    // Getters and Setters
    public bool Shield
    {
        get => shield;
        set => shield = value;
    }

    public float ShieldDuration
    {
        get => shieldDuration;
        set => shieldDuration = value;
    }

    public int ShieldCount
    {
        get => shieldCount;
        set => shieldCount = value;
    }

    private void Awake()
    {
        gameui = GameObject.Find("GameUI").GetComponent<GameUI>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Shield")
        {
            ShieldOn(12f, 3);
            gameui.ScoreUp(15);
            Destroy(other.gameObject);
            return;
        }
    }



// ================ SHIELD METHODS ====================

    IEnumerator ShieldTimer()
    {
        while (shieldCount > 0 && shieldDuration > 0)
        {
            shieldDuration -= Time.deltaTime;
            Debug.Log("Shield duration : " + shieldDuration);
            yield return null;
        }
        ShieldOff();
        yield return null;
    }

    public void ShieldOn(float duration, int count)
    {
        shield = true;
        shieldDuration = duration;
        shieldCount = count;
        StartCoroutine(ShieldTimer());
        Debug.Log("Shield acquired...");
    }

    private void ShieldOff()
    {
        shield = false;
        shieldDuration = 5f;
        shieldCount = 1;
        Debug.Log("Shield deactivated...");
    }
}
