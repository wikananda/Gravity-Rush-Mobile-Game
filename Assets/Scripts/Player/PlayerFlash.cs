using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlash : MonoBehaviour
{
    [SerializeField] bool flash = false;
    [SerializeField] float flashDuration = 10f;

    public bool Flash
    {
        get => flash;
        set => flash = value;
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
