using UnityEngine;
using System.Collections;

/*
 ====================================================================
 Author:            Tom Clark

 Purpose:           To contain the functionality of the gem.
 Notes:             
 
 ====================================================================
*/

public class GemPickup : MonoBehaviour
{	
    void OnTriggerEnter(Collider other)
    {
        PlayerManager.Instance.hasGem = true;
        gameObject.SetActive(false);
        PlayerManager.Instance.gemsInInventory.Add(this.gameObject);
    }
}