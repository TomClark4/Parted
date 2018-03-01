using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 ====================================================================
 Author:            Tom Clark

 Purpose:           To hold information about the player.
 Notes:             
 
 ====================================================================
*/

public class PlayerManager : GenericSingleton<PlayerManager>
{
    public List<GameObject> gemsInInventory = new List<GameObject>();

    public bool hasGem = false;
    public bool usedGem = false;
}