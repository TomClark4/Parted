using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 ====================================================================
 Author:            Tom Clark

 Purpose:           To initialise the patrol waypoints.
 Notes:             
 
 ====================================================================
*/

public class PatrolPosition : MonoBehaviour
{
    [SerializeField]
    float speed = 0;

    [SerializeField]
    float speedRot = 20f;

	[SerializeField]
	float waitTime = 0.5f;

    int i = 0;

    public List<GameObject> patrolWaypoints = new List<GameObject>();

	bool canMove = false;

    Animator anim;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        PatrolMovement();
    }

    /// <summary>
    /// Controls the movement of patrols.
    /// </summary>
    void PatrolMovement()
    {
        if (canMove)
        {
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(patrolWaypoints[i].transform.position.x, transform.position.y, patrolWaypoints[i].transform.position.z), speed * Time.deltaTime);
            Vector3 targetDir = new Vector3(patrolWaypoints[i].transform.position.x - transform.position.x, 0, patrolWaypoints[i].transform.position.z - transform.position.z);
            float step = speedRot * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
        }

        if (new Vector3(transform.position.x, transform.position.y, transform.position.z) == new Vector3(patrolWaypoints[i].transform.position.x, transform.position.y, patrolWaypoints[i].transform.position.z) && i == patrolWaypoints.Count - 1)
        {
            patrolWaypoints.Reverse();
            i = 0;
        }
        else if (new Vector3(transform.position.x, transform.position.y, transform.position.z) == new Vector3(patrolWaypoints[i].transform.position.x, transform.position.y, patrolWaypoints[i].transform.position.z))
        {
			StartCoroutine (Halt());

            i++;
        }
    }

	IEnumerator Halt()
	{
        anim.SetBool("IsWalking", false);

		canMove = false;
		yield return new WaitForSeconds (waitTime);

        anim.SetBool("IsWalking", true);
		canMove = true;
	}
}