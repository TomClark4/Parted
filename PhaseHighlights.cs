using UnityEngine;
using System.Collections;

public class PhaseHighlights : MonoBehaviour
{
	Renderer rend;

    public Tile tileUnderneath;

	// Use this for initialization
	void Start ()
	{
		rend = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	void OnTriggerStay(Collider coll)
	{
        if (coll.gameObject.tag == "Player" && !PlayerPhasing.isPhasing && tileUnderneath == null)
		{
			rend.enabled = true;
		}
        if (coll.gameObject.tag == "Player" && !PlayerPhasing.isPhasing && tileUnderneath != null)
        {
            if (tileUnderneath.isPassable && tileUnderneath.isCrumble)
            {
                rend.enabled = true;
            }

            if (!tileUnderneath.isPassable && tileUnderneath.isCrumble)
            {
                rend.enabled = false;
            }

            if (tileUnderneath.isPassable && tileUnderneath.isSwitchable)
            {
                rend.enabled = true;
            }

            if (!tileUnderneath.isPassable && tileUnderneath.isSwitchable)
            {
                rend.enabled = false;
            }
        }
	}

	void OnTriggerExit(Collider coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			rend.enabled = false;
		}	
	}
}
