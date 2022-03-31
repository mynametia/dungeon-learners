using UnityEngine;
using Pathfinding;

/// <summary>
/// Control's the player sprite
/// </summary>
public class PlayerSpriteController : MonoBehaviour
{
    // Control sprite appearance of player
    
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
        // Make sprite face left
        if (aiPath.desiredVelocity.x <= -0.01f) 
        {
            transform.localScale = new Vector3(-defaultFaceRight*initialScaleX, initialScaleY, initialScaleZ);
        }
        //Make sprite face right
        else if (aiPath.desiredVelocity.x >= 0.01f) 
        {
            transform.localScale = new Vector3(defaultFaceRight * initialScaleX, initialScaleY, initialScaleZ);
        }

        // Update animator to activate/deactivate walking animation
        animator.SetFloat("Speed", Mathf.Abs(aiPath.desiredVelocity.magnitude));
    }
}
