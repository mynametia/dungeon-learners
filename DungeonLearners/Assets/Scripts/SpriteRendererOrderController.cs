using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls sprite rendering order
/// </summary>
public class SpriteRendererOrderController : MonoBehaviour
{
    private SpriteRenderer playerSprite;
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer[] renderers = FindObjectsOfType<SpriteRenderer>();
        foreach (SpriteRenderer renderer in renderers)
        {
            renderer.sortingOrder = calculateSortOrder(renderer);
        }
        playerSprite = GameObject.FindGameObjectWithTag("PlayerSprite").GetComponent<SpriteRenderer>();
        playerSprite.sortingOrder = calculateSortOrder(playerSprite);
    }

    // Update is called once per frame
    void Update()
    {
        playerSprite.sortingOrder = calculateSortOrder(playerSprite);
    }

    private int calculateSortOrder(SpriteRenderer renderer)
    { 
        return (int)(renderer.transform.position.y * -100);
    }
}
