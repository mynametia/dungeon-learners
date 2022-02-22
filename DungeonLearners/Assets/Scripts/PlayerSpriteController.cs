using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlayerSpriteController : MonoBehaviour
{
    public AIPath aiPath;
    public Animator animator;

    //-1 if the sprite is default facing left, 1 if the sprite is default facing right
    public int defaultFaceRight = 1;
    private float initialScaleX, initialScaleY, initialScaleZ;

    void Start()
    {
        initialScaleX = transform.localScale.x;
        initialScaleY = transform.localScale.y;
        initialScaleZ = transform.localScale.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (aiPath.desiredVelocity.x <= -0.01f) //sprite face left, flip
        {
            transform.localScale = new Vector3(-defaultFaceRight*initialScaleX, initialScaleY, initialScaleZ);
        }
        else if (aiPath.desiredVelocity.x >= 0.01f) //sprite face right
        {
            transform.localScale = new Vector3(defaultFaceRight * initialScaleX, initialScaleY, initialScaleZ);
        }

        //update animator
        animator.SetFloat("Speed", Mathf.Abs(aiPath.desiredVelocity.magnitude));
    }
}
