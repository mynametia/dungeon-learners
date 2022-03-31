using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls camera movement.
/// </summary>
public class CameraMovementController : MonoBehaviour
{
    // Controls camera movement

    private GameObject player;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }
}
