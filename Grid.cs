using UnityEngine;
using System.Collections;

/*
 ====================================================================
 Author:			Tom Clark

 Purpose:			To initilaise the tile grid of the level.
 Notes:				
 
 ====================================================================
*/

public class Grid: MonoBehaviour
{
    Tile[,] tileGrid;

    [SerializeField]
    int gridSizeX = 0;
    [SerializeField]
    int gridSizeY = 0;

    void Start()
    {
        tileGrid = new Tile[gridSizeX, gridSizeY];

        PopulateTileArray();

        PathfindingManager.Instance.currentLevelGridSizeX = gridSizeX;
        PathfindingManager.Instance.currentLevelGridSizeY = gridSizeY;
        PathfindingManager.Instance.currentTileGrid = tileGrid;
    }

    /// <summary>
    /// Adds all tiles to the correct position in the array of tiles for the current level.
    /// </summary>
    void PopulateTileArray()
    {
        for (int y = 0; y < gridSizeY; y++)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                //populates the grid with tiles one row at a time from left to right along the x axis.
                tileGrid[x, y] = transform.GetChild(x + (y * gridSizeX)).GetComponent<Tile>();
                tileGrid[x, y].name = string.Format("Tile[{0},{1}]", x, y);

                //Sets the coordinates of each tile to the tile class.
                Tile initialisedTile = tileGrid[x, y];
                if (initialisedTile)
                {
                    initialisedTile.SetTile(x, y);
                }
            }
        }
    }
}