using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GemImageActive : MonoBehaviour
{
    Image gemImage;

	// Use this for initialization
	void Start ()
    {
        gemImage = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (PlayerManager.Instance.hasGem)
        {
            gemImage.enabled = true;
        }
        else
        {
            gemImage.enabled = false;
        }
	}
}
