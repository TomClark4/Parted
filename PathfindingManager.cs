using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 ====================================================================
 Author:			Tom Clark

 Purpose:			To calculate, using the A* pathfinding algorithm,
 					the shortest movement distance between 2 tiles.
 Notes:				
 
 ====================================================================
*/

public class PathfindingManager : GenericSingleton<PathfindingManager>
{
    public int currentLevelGridSizeX;
    public int currentLevelGridSizeY;

    public Tile[,] currentTileGrid;

    //Set of tiles to be evaluated.
    public List<Tile> openSet;

    //set of tiles already evaluated.
    public List<Tile> closedSet;

    //set of tiles that neighbour the current tile.
    public List<Tile> neighbourTiles;

    //set of tiles that represent the shortest distance.
    public List<Tile> tilePath = new List<Tile>();

    /// <summary>
    /// Finds the astar path.
    /// </summary>
    public void FindAstarPath()
    {
		if (TargetPosition.targetTile != null && TargetPosition.targetTile.isPassable)
        {
            if (PlayerPosition.player.transform.position.x != TargetPosition.targetTile.transform.position.x || PlayerPosition.player.transform.position.z != TargetPosition.targetTile.transform.position.z)
            {
				//Clears all lists in preparation for a new movement path.
                openSet.Clear();
                closedSet.Clear();
                neighbourTiles.Clear();
                tilePath.Clear();

                openSet = new List<Tile>();
                closedSet = new List<Tile>();

                openSet.Add(PlayerPosition.playerTile);

                while (openSet.Count > 0)
                {
                    Tile currentTile = openSet[0];

                    GetLowestFCostTile(ref currentTile);

					//transfer this new tile to the closed set, as its costs have already been calculated.
                    openSet.Remove(currentTile);
                    closedSet.Add(currentTile);

					//Check if the pathfinding has finished.
                    if (currentTile == TargetPosition.targetTile)
                    {
                        GetCalculatedPath(PlayerPosition.playerTile, TargetPosition.targetTile);
                        return;
                    }

					//Stores the neighbour tiles of the current tile in preparation of the calculation of their A* costs.
                    neighbourTiles = new List<Tile>();

                    PopulateNeighbourTiles(currentTile);

                    CalculateNewTileCosts(currentTile);
                }
            }
        }
		if (tilePath.Count == 0 && TargetPosition.targetTile.isPassable)
		{
			tilePath.Add (PlayerPosition.playerTile);
		}
    }

    /// <summary>
    /// Gets the lowest f cost tile.
    /// </summary>
    void GetLowestFCostTile(ref Tile currentTile)
    {
        for (int i = 1; i < openSet.Count; i++)
        {
			//Gets the tile with the lowest f cost that is the closest to the target tile.
            if 
            (
                openSet[i].FCost < currentTile.FCost
                || openSet[i].FCost == currentTile.FCost && openSet[i].hCost < currentTile.hCost
                || openSet[i].FCost == currentTile.FCost && openSet[i].hCost == currentTile.hCost && !openSet[i].isCrumble
            )
            {
                currentTile = openSet[i];
            }
        }
    }

    /// <summary>
    /// Gets the calculated path, ready to pass to the movement class.
    /// </summary>
    void GetCalculatedPath(Tile playerTile, Tile targetTile)
    {
        Tile currentTile = targetTile;

        while (currentTile != playerTile)
        {
            tilePath.Add(currentTile);
            currentTile = currentTile.parentTile;
        }
            
        tilePath.Add(PlayerPosition.playerTile);

        tilePath.Reverse();

        for (int i = 1; i < tilePath.Count; i++)
        {
            if (PlayerPosition.fixingTile == tilePath[i] && PlayerPosition.fixingTile.isCrumble)
            {
                tilePath.RemoveRange(1, tilePath.Count - 1);
            }
        }


//        if (PlayerPosition.fixingTile == tilePath && PlayerPosition.fixingTile.isCrumble)
//        {
//            tilePath.RemoveAt(tilePath.Count - 1);
//        }
    }

    /// <summary>
    /// Populates the neighbour tiles list, first checking if the tiles exist within the current tile grid.
    /// </summary>
    void PopulateNeighbourTiles(Tile currentTile)
    {
        if ((int)currentTile.GetTile().x > 0)
        {
            Tile leftTile = currentTileGrid[(int)currentTile.GetTile().x - 1, (int)currentTile.GetTile().y];
            neighbourTiles.Add(leftTile);
        }
        if ((int)currentTile.GetTile().y > 0)
        {
            Tile downTile = currentTileGrid[(int)currentTile.GetTile().x, (int)currentTile.GetTile().y - 1];
            neighbourTiles.Add(downTile);
        }
        if ((int)currentTile.GetTile().x < currentLevelGridSizeX - 1)
        {
            Tile rightTile = currentTileGrid[(int)currentTile.GetTile().x + 1, (int)currentTile.GetTile().y];
            neighbourTiles.Add(rightTile);
        }
        if ((int)currentTile.GetTile().y < currentLevelGridSizeY - 1)
        {
            Tile upTile = currentTileGrid[(int)currentTile.GetTile().x, (int)currentTile.GetTile().y + 1];
            neighbourTiles.Add(upTile);
        }
    }

    /// <summary>
    /// Calculates the new tile costs for all viable neighbour tiles.
    /// </summary>
    void CalculateNewTileCosts(Tile currentTile)
    {
        for (int i = 0; i < neighbourTiles.Count; i++)
        {
			//If the tile is not passable or its costs have already been calculated, it is skipped.
            if (!neighbourTiles[i].isPassable || closedSet.Contains(neighbourTiles[i]))
            {
                continue;
            }
            int newMovementCost = currentTile.gCost + Mathf.Abs((int)currentTile.GetTile().x - (int)neighbourTiles[i].GetTile().x) + Mathf.Abs((int)currentTile.GetTile().y - (int)neighbourTiles[i].GetTile().y);
            if (newMovementCost < neighbourTiles[i].gCost || !openSet.Contains(neighbourTiles[i]))
            {
                neighbourTiles[i].gCost = newMovementCost;
                neighbourTiles[i].hCost = Mathf.Abs((int)TargetPosition.targetTile.GetTile().x - (int)neighbourTiles[i].GetTile().x) + Mathf.Abs((int)TargetPosition.targetTile.GetTile().y - (int)neighbourTiles[i].GetTile().y);
                neighbourTiles[i].parentTile = currentTile;
				//checks the openset doesn't already contain the new neighbour tile before adding it.
                if (!openSet.Contains(neighbourTiles[i]))
                {
                    openSet.Add(neighbourTiles[i]);
                }
            }
        }
    }
}