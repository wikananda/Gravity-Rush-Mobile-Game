using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlash : MonoBehaviour
{
    [SerializeField] bool flash = false;
    [SerializeField] float flashDuration = 10f;
    PlayerShield shield;

    public bool Flash
    {
        get => flash;
        set => flash = value;
    }

    private void Start()
    {
        shield = GetComponent<PlayerShield>();
    }

    IEnumerator FlashDuration()
    {
        while (flash && flashDuration > 0)
        {
            flashDuration -= Time.deltaTime;
            yield return null;
        }
        flash = false;
        flashDuration = 10f;
        yield return null;
    }

    public void FlashMove(int gravityDirection)
    {
        Vector3 upPos = GameObject.Find("UpPos").transform.position;
        Vector3 downPos = GameObject.Find("DownPos").transform.position;

        if (gravityDirection < 0)
        {
            transform.position = new Vector3(transform.position.x, upPos.y, 0);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, downPos.y, 0);
        }

        shield.ShieldOn(.2f, 1); // Give mini shield during teleporting
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "Flash")
        {
            flash = true;
            Destroy(other.gameObject);
            StartCoroutine(FlashDuration());
            return;
        }
    }
}
