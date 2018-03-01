using UnityEngine;
using System.Collections;

/*
 ====================================================================
 Author:			Tom Clark

 Purpose:			To initialise the player tile.
 Notes:				
 
 ====================================================================
*/

public class PlayerPosition : MonoBehaviour
{
    public static Tile playerTile;
    public static Tile fixingTile;
    public static GameObject player;

    void Start()
    {
        player = gameObject;
    }

    /// <summary>
    /// Initialises the starting (player) tile using a raycast from the player position.
    /// </summary>
    public static void GetPlayerTile()
    {
        //The raycast to initialise the starting (player) tile.
        Ray playerTileRay = new Ray(player.transform.position, -player.transform.up);
        RaycastHit playerTileHit;
        Physics.Raycast(playerTileRay, out playerTileHit);

        playerTile = playerTileHit.collider.GetComponent<Tile>();

        fixingTile = playerTile;
    }
}