using UnityEngine;
using ProbablyCats.Utilities;
using System.Collections;

/*
 ====================================================================
 Author:            Tom Clark 

 Purpose:           To check if the player's phase request can be
                    processed, and if it can, initiates the phasing.
 Notes:             
 
 ====================================================================
*/

public class PlayerPhasing : MonoBehaviour
{
    public static bool isPhasing = false;

    public static bool hasRun = false;

    public static bool readyToPhase = false;

    PhasingShaderControl phaseShader;
    [SerializeField]
    float phaseSpeed = 0;
    float speedRot = 20f;
    bool canRotate = true;



    static bool hello = false;

    void Awake()
    {
        isPhasing = false;
    }

    void Start()
    {
        phaseShader = GetComponent<PhasingShaderControl> ();
    }

    void Update()
    {
        if (isPhasing)
        {
            StartCoroutine(Coroutines.RotateObjectTo(gameObject, TargetTileRotation(), 0.2f));


            if (!hasRun)
            {
                hasRun = true;
                phaseShader.StartCoroutine(phaseShader.PartedShaderLerp(0, 1, 0.5f));
            }



            if (readyToPhase)
                PhaseMovement();


        }


    }

    /// <summary>
    /// Initialises player and target tiles in preparation to check their validity.
    /// </summary>
    public static void PlayerPhase()
    {
       
        if (PlayerPosition.playerTile != null && TargetPosition.targetTile != null)
        {
            float playerTileX = PlayerPosition.playerTile.GetTile().x;
            float playerTileY = PlayerPosition.playerTile.GetTile().y;
            float targetTileX = TargetPosition.targetTile.GetTile().x;
            float targetTileY = TargetPosition.targetTile.GetTile().y;
            bool targetTilePassable = TargetPosition.targetTile.isPassable;

            int phaseDistance = 2;

            CheckPhasing(playerTileX, playerTileY, targetTileX, targetTileY, targetTilePassable, phaseDistance, 0);
            CheckPhasing(playerTileX, playerTileY, targetTileX, targetTileY, targetTilePassable, -phaseDistance, 0);
            CheckPhasing(playerTileX, playerTileY, targetTileX, targetTileY, targetTilePassable, 0, phaseDistance);
            CheckPhasing(playerTileX, playerTileY, targetTileX, targetTileY, targetTilePassable, 0, -phaseDistance);
        }
    }

    /// <summary>
    /// Checks that the player is attempting to phase to a viable tile.
    /// </summary>
    static void CheckPhasing(float playerTileX, float playerTileY, float targetTileX, float targetTileY, bool targetTilePassable, int phaseDistanceX, int phaseDistanceY)
    {
        int midPhaseDistanceX = (int) (phaseDistanceX * 0.5f);
        int midPhaseDistanceY = (int) (phaseDistanceY * 0.5f);

        if (playerTileX + phaseDistanceX == targetTileX && playerTileY + phaseDistanceY == targetTileY && targetTilePassable)
        {
            if (!PathfindingManager.Instance.currentTileGrid[(int)playerTileX + midPhaseDistanceX, (int)playerTileY + midPhaseDistanceY].isCrystalWall && !PathfindingManager.Instance.currentTileGrid[(int)playerTileX + midPhaseDistanceX, (int)playerTileY + midPhaseDistanceY].isUnphasable)
            {
                InputManager.Instance.tileIsPhasable = true;
                if (!PlayerManager.Instance.hasGem)
                {
                    hello = true;
                }
                else if (PlayerManager.Instance.hasGem)
                {
                    hello = false;
                }
            }
            if (PathfindingManager.Instance.currentTileGrid[(int)playerTileX + midPhaseDistanceX, (int)playerTileY + midPhaseDistanceY].isCrystalWall && PlayerManager.Instance.hasGem == true && !PathfindingManager.Instance.currentTileGrid[(int)playerTileX + midPhaseDistanceX, (int)playerTileY + midPhaseDistanceY].isUnphasable)
            {
                InputManager.Instance.tileIsPhasable = true;
                if (!PlayerManager.Instance.hasGem)
                {
                    hello = true;
                }
                else if (PlayerManager.Instance.hasGem)
                {
                    hello = false;
                }

                //Comment out the if statement to allow players to walk through the crystal wall
                if (PlayerPhasing.isPhasing)
                {
                    PlayerManager.Instance.usedGem = true;
                    PlayerManager.Instance.hasGem = false;
                    PathfindingManager.Instance.currentTileGrid[(int)playerTileX + midPhaseDistanceX, (int)playerTileY + midPhaseDistanceY].isPassable = true;
                    PathfindingManager.Instance.currentTileGrid[(int)playerTileX + midPhaseDistanceX, (int)playerTileY + midPhaseDistanceY].isCrystalWall = false;

                    Debug.Log("should be setting door to open");
                    PathfindingManager.Instance.currentTileGrid[(int)playerTileX + midPhaseDistanceX, (int)playerTileY + midPhaseDistanceY].transform.GetChild(0).GetComponent<Animator>().SetBool("IsOpen", true);
                    Destroy(PlayerManager.Instance.gemsInInventory[0]);
                }
            }
        }
    }

    //memes
    //hehe xd

    /// <summary>
    /// Phases the player to the target tile.
    /// </summary>
    void PhaseMovement()
    {
        transform.position = Vector3.MoveTowards
            (
                new Vector3(transform.position.x, transform.position.y, transform.position.z),
                new Vector3(TargetPosition.targetTile.transform.position.x, transform.position.y, TargetPosition.targetTile.transform.position.z),
                phaseSpeed * Time.deltaTime
            );
        if (transform.position.x == TargetPosition.targetTile.transform.position.x && transform.position.z == TargetPosition.targetTile.transform.position.z)
        {

           
            phaseShader.StartCoroutine(phaseShader.PartedShaderLerp(1, 0, 0.5f));
            readyToPhase = false;
            hasRun = false;
           
            Debug.Log("this is happening");

            InputManager.Instance.tileIsPhasable = false;
            canRotate = false;
            isPhasing = false;
            PlayerPosition.playerTile = null;
            TargetPosition.targetTile = null;

       //     phaseShader.StartCoroutine (phaseShader.PartedShaderLerp (1, 0, 0.5f));
           
            if (PlayerManager.Instance.gemsInInventory.Count != 0 && !hello)
            {
                PlayerManager.Instance.gemsInInventory[0].SetActive(true);
                PlayerManager.Instance.gemsInInventory.Clear();
                PlayerManager.Instance.hasGem = false;
            }
                
        }
        else
        {
            canRotate = true;
        }

        if (canRotate)
        {
            //Rotates the player by making them face the direction in which they are moving.
            Vector3 targetDir = new Vector3(TargetPosition.targetTile.transform.position.x - transform.position.x, 0, TargetPosition.targetTile.transform.position.z - transform.position.z);
            float step = speedRot * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
    }

    Quaternion TargetTileRotation()
    {       
        Vector3 relativePos = new Vector3 (TargetPosition.targetTile.transform.position.x - transform.position.x, 0f, TargetPosition.targetTile.transform.position.z - transform.position.z);
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        return transform.rotation = rotation;
    }
}