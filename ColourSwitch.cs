using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/*
 ====================================================================
 Author:            Tom Clark

 Purpose:           To detect when the tile switch has been triggered
                    and to switch the switchable tiles.
 Notes:             
 
 ====================================================================
*/

public class ColourSwitch : GenericSingleton<ColourSwitch>
{
	List<Tile> switchableTiles = new List<Tile>();

	public Animator[] tileAnims;

    public Tile tileToRotTo;

	/// <summary>
	/// Checks the player can switch, and if so, activates switching tiles.
	/// </summary>
	public void CheckSwitch()
	{
		int xTile = (int)PlayerPosition.playerTile.GetTile().x;
		int yTile = (int)PlayerPosition.playerTile.GetTile().y;

		if (
			PathfindingManager.Instance.currentTileGrid[xTile + 1, yTile].isSwitch 
			|| PathfindingManager.Instance.currentTileGrid[xTile - 1, yTile].isSwitch 
			|| PathfindingManager.Instance.currentTileGrid[xTile, yTile + 1].isSwitch 
			|| PathfindingManager.Instance.currentTileGrid[xTile, yTile - 1].isSwitch
		)
		{
            PopulateSwitchableTiles();            
            SwitchTiles(xTile, yTile);

			switchableTiles.Clear();
		}
	}

	/// <summary>
	/// Populates the switchable tiles list with all switchable tiles from the current scene.
	/// </summary>
	void PopulateSwitchableTiles()
	{
		foreach (Tile colorTile in PathfindingManager.Instance.currentTileGrid)
		{
			if (colorTile.isSwitchable)
			{
				switchableTiles.Add(colorTile);
			}
		}
	}

	/// <summary>
	/// Switches all switchable tiles in the level.
	/// </summary>
    void SwitchTiles(int xTile, int yTile)
	{
        if (PathfindingManager.Instance.currentTileGrid[xTile + 1, yTile].isSwitch)
        {
            PathfindingManager.Instance.currentTileGrid[xTile + 1, yTile].GetComponentInChildren<Animator>().SetBool("IsTurning", true);
            tileToRotTo = PathfindingManager.Instance.currentTileGrid[xTile + 1, yTile];
        }
        if (PathfindingManager.Instance.currentTileGrid[xTile - 1, yTile].isSwitch)
        {
            PathfindingManager.Instance.currentTileGrid[xTile - 1, yTile].GetComponentInChildren<Animator>().SetBool("IsTurning", true);
            tileToRotTo = PathfindingManager.Instance.currentTileGrid[xTile - 1, yTile];
        }
        if (PathfindingManager.Instance.currentTileGrid[xTile, yTile + 1].isSwitch)
        {
            PathfindingManager.Instance.currentTileGrid[xTile, yTile + 1].GetComponentInChildren<Animator>().SetBool("IsTurning", true);
            tileToRotTo = PathfindingManager.Instance.currentTileGrid[xTile, yTile + 1];
        }
        if (PathfindingManager.Instance.currentTileGrid[xTile, yTile - 1].isSwitch)
        {
            PathfindingManager.Instance.currentTileGrid[xTile, yTile - 1].GetComponentInChildren<Animator>().SetBool("IsTurning", true);
            tileToRotTo = PathfindingManager.Instance.currentTileGrid[xTile, yTile - 1];
        }

        if (SceneManager.GetActiveScene().buildIndex < 13)
        {
            PlayerPosition.player.GetComponent<Animator>().SetBool("IsWheeling", true);
        }
        else if (SceneManager.GetActiveScene().buildIndex >= 13)
        {
            PlayerPosition.player.GetComponent<Animator>().SetBool("IsRoping", true);
        }

//		foreach (Animator tileAnim in tileAnims) 
//		{
//            bool currentAnimState = tileAnim.GetBool ("isSwitched");
//            tileAnim.SetBool ("isSwitched", !currentAnimState);
//		}

		for (int i = 0; i < switchableTiles.Count; i++)
		{


            Debug.Log("tiles are switched");
			switchableTiles[i].isPassable = !switchableTiles[i].isPassable;


		}
	}
}