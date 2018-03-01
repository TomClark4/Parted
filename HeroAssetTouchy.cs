using UnityEngine;
using System.Collections;

public class HeroAssetTouchy : MonoBehaviour
{
	Animator anim;

	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update ()
	{


		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				if (hit.collider.GetComponent<HeroAssetTouchy>())
				{
					anim.SetBool ("IsTouched", true );
					Invoke ("MakeItFalse", 0.2f);
				}
			}
		}
	}

	void MakeItFalse()
	{
		anim.SetBool ("IsTouched", false );
	}
}
