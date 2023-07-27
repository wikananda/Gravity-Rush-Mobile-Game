using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagnet : MonoBehaviour
{
    [SerializeField] bool magnet = false;
    [SerializeField] float magnetDuration = 10f;
    [SerializeField] float magnetSpeed = 10f;

    public bool Magnet
    {
        get => magnet;
        set => magnet = value;
    }

    public float MagnetDuration
    {
        get => magnetDuration;
        set => magnetDuration = value;
    }

    public float MagnetSpeed
    {
        get => magnetSpeed;
        set => magnetSpeed = value;
    }

    IEnumerator CoinMagnet()
    {
        while (magnetDuration > 0)
        {
            magnetDuration -= Time.deltaTime;
            yield return null;
        }
        magnet = false;
        magnetDuration = 10f;
        yield return null;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Magnet")
        {
            magnet = true;
            Destroy(other.gameObject);
            StartCoroutine(CoinMagnet());
            return;
        }    
    }
}
