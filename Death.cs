using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour
{
    PhasingShaderControl phaseShader;
    public float overTimePhase;

    // Use this for initialization
    void Start()
    {
        phaseShader = GetComponent<PhasingShaderControl> ();
    }
	
    // Update is called once per frame
    void Update()
    {
	    
    }

    public void DissolveAway()
    {
        phaseShader.StartCoroutine(phaseShader.PartedShaderLerp(0, 1, overTimePhase));
    }

    public void DisableMesh()
    {
        gameObject.SetActive(false);
    }
}
