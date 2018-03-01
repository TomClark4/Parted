using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 ====================================================================
 Author:			Tom Clark

 Purpose:			To store the position of the tile within the tile grid
					and to calculate and return an f cost when called upon by the
					A* pathfinding class.
 Notes:

 ====================================================================
*/

public class Tile: MonoBehaviour
{
    int tileX;
    int tileY;

    public int gCost;
    public int hCost;

    int fCost;

    public Tile parentTile;

    public bool isPassable = true;

    public bool isCrystalWall = false;

	public bool isSwitch = false;

    public bool isSwitchable = false;

    public bool isUnphasable = false;

    public bool isCrumble = false;


    public Renderer[] diamondRenderer;


    void Start()
    {
        diamondRenderer = GetComponentsInChildren<Renderer>();


    }

    void Update()
    {
        foreach (Renderer diamond in diamondRenderer)
        {
            if (diamond.gameObject.tag == "Diamond")
            {
                if (!isPassable && isCrumble)
                {
                    diamond.enabled = false;
                }
                if (!isPassable && isSwitchable)
                {
                    diamond.enabled = false;
                }
                if (isPassable && isCrumble)
                {
                    diamond.enabled = true;
                }
                if (isPassable && isSwitchable)
                {
                    diamond.enabled = true;
                }
            }
        }
			
    }


    //The lowest f cost tile path represents the shortest distance between the start and end tiles.
    //The f cost is always returned as the g cost (distance from starting tile) plus the h cost (distance to end tile).
    public int FCost
    {
        get
        {
            fCost = gCost + hCost;
            return fCost;
        }
        set
        {
            fCost = value;
        }
    }

    /// <summary>
    /// Sets the tile.
    /// </summary>
    public void SetTile(int x, int y)
    {
        tileX = x;
        tileY = y;
    }

    /// <summary>
    /// Gets the tile.
    /// </summary>
    public Vector2 GetTile()
    {
        return new Vector2(tileX, tileY);
    }
}