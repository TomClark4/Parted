using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 ====================================================================
 Author:			Tom Clark

 Purpose:			To move the player along the path calculated by
 					the pathfinding manager.
 Notes:				
 
 ====================================================================
*/

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 0;

    [SerializeField]
    float speedRot = 20f;

    bool canRotate = true;

    public static bool isWalking;

    void Awake()
    {
        isWalking = false;
    }

    void Update()
    {
        MovePlayer();



        if (PlayerPosition.player.GetComponent<Animator>().GetBool("IsWheeling") || PlayerPosition.player.GetComponent<Animator>().GetBool("IsRoping"))
        {
            Vector3 targetDir = new Vector3(ColourSwitch.Instance.tileToRotTo.transform.position.x - transform.position.x, 0, ColourSwitch.Instance.tileToRotTo.transform.position.z - transform.position.z);
            float step = speedRot * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
        }

    }

    /// <summary>
    /// Moves the player along the tile queue path.
    /// </summary>
    public void MovePlayer()
    {
		if (PathfindingManager.Instance.tilePath.Count != 0)
        {
            PlayerMovement.isWalking = true;

            //Moves the player from their current tile to the next tile in the queue.
            transform.position = Vector3.MoveTowards
			(
                new Vector3(transform.position.x, transform.position.y, transform.position.z),
                new Vector3(PathfindingManager.Instance.tilePath[0].transform.position.x, transform.position.y, PathfindingManager.Instance.tilePath[0].transform.position.z),
                speed * Time.deltaTime
            );
                    
            //As the first tile in the tileQueue is reached, the tile is then removed and replaced by the next tile in the queue.
            //to make sure the player moves to the tiles in the correct order.
            if (new Vector3(transform.position.x, transform.position.y, transform.position.z) ==
                new Vector3(PathfindingManager.Instance.tilePath[0].transform.position.x, transform.position.y, PathfindingManager.Instance.tilePath[0].transform.position.z))
            {
                canRotate = false;
                PathfindingManager.Instance.tilePath.RemoveAt(0);
            }
            else
            {
                canRotate = true;
            }

            if (canRotate)
            {
                //Rotates the player by making them face the direction in which they are moving.
                Vector3 targetDir = new Vector3(PathfindingManager.Instance.tilePath[0].transform.position.x - transform.position.x, 0, PathfindingManager.Instance.tilePath[0].transform.position.z - transform.position.z);
                float step = speedRot * Time.deltaTime;
                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDir);
            }

            //If the player has reached the target, the tiles are cleared in preparation for the next input.
            if (PathfindingManager.Instance.tilePath.Count == 0)
            {
                PlayerPosition.playerTile = null;
                TargetPosition.targetTile = null;
                PlayerMovement.isWalking = false;
            }
        }
    }
}