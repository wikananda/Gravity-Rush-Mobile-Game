using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public float parallaxDepth = 1f;

    float initialSpeed;
    float parallaxSpeed;
    
    Player player;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        player = GameObject.Find("Player").GetComponent<Player>();
        initialSpeed = player.speed;
    }


    void FixedUpdate()
    {
        parallaxSpeed = player.speed * parallaxDepth / (10 * initialSpeed);
        meshRenderer.material.mainTextureOffset += new Vector2(parallaxSpeed * Time.deltaTime, 0);
    }
}
