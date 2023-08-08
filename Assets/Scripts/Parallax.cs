using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public float parallaxDepth = 1f;

    float initialSpeed;
    float parallaxSpeed;
    
    GameManager gameManager;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        initialSpeed = gameManager.Speed;
    }


    void FixedUpdate()
    {
        parallaxSpeed = gameManager.Speed * parallaxDepth / (12 * initialSpeed);
        meshRenderer.material.mainTextureOffset += new Vector2(parallaxSpeed * Time.deltaTime, 0);
    }
}
