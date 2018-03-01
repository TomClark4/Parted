using UnityEngine;
using System.Collections;

public class TilePassability : MonoBehaviour
{
    Tile tileToMakePassable;

    // Use this for initialization
    void Start()
    {
        tileToMakePassable = GetComponentInParent<Tile>();
    }

    public void MakeTileImpassable()
    {
        tileToMakePassable.isPassable = false;
    }
	
    public void MakeTilePassable()
    {
        tileToMakePassable.isPassable = true;
    }
}
