using UnityEngine;
using System.Collections;

/*
 ====================================================================
 Author:			Tom Clark

 Purpose:			To initialise the target tile.
 Notes:				
 
 ====================================================================
*/

public class TargetPosition : MonoBehaviour
{
    public static Tile targetTile;

    /// <summary>
    /// Initialises the target tile using a raycast from the mouse position.
    /// </summary>
    public static void SelectTile()
    {
        //The raycast to initialise the target tile.
        Ray targetTileRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit targetTileHit;
        if (Physics.Raycast(targetTileRay, out targetTileHit))
        {
            if (targetTileHit.collider.GetComponent<GemPickup>())
            {
                targetTile = targetTileHit.collider.GetComponentInParent<Tile>();
            }
            else
            {
                targetTile = targetTileHit.collider.GetComponent<Tile>();
            }
        }
    }
}