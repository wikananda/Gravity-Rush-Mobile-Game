using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public float parallaxDepth = 1f;
    public bool useTexture = false;

    float initialSpeed;
    float parallaxSpeed;
    
    Player player;
    private void Awake() {
        if (useTexture)
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }
        player = GameObject.Find("Player").GetComponent<Player>();
        initialSpeed = player.speed;
    }


    void Update()
    {
        parallaxSpeed = player.speed * parallaxDepth / (10 * initialSpeed);
        if (useTexture)
        {
            meshRenderer.material.mainTextureOffset += new Vector2(parallaxSpeed * Time.deltaTime, 0);
        }
        else
        {
            transform.position = new Vector3(transform.position.x - player.speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
    }

    // Trigger still bugging for item, maybe because item object's collider also "is trigger"
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Despawn")
        {
            Debug.Log("Item despawned");
            Destroy(gameObject);
        }
    }
}
