using UnityEngine;
using System.Collections;

/*
 ====================================================================
 Author:            Tom Clark

 Purpose:           To handle main character animations.
 Notes:             
 
 ====================================================================
*/

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    [Range(1, 20)]
    public int randomMaxValue = 3;

    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(RandomiseIdle());
    }

    void Update()
    {
        if (PlayerMovement.isWalking)
        {
            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }

        if (PlayerPhasing.isPhasing)
        {
            anim.SetBool("IsPhasing", true);
        }
        else
        {
            anim.SetBool("IsPhasing", false);
        }
    }

    /// <summary>
    /// Randomises the idle animation state of the player.
    /// </summary>
    IEnumerator RandomiseIdle()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);

            int idleChance = Random.Range(1, randomMaxValue);
            anim.SetInteger("IsActive", idleChance);

            //Makes sure the active idle cannot be played more than once without a period of passive idol.
            if (idleChance == 1)
            {
                yield return new WaitForSeconds(2);
                anim.SetInteger("IsActive", 2);
                yield return new WaitForSeconds(3);
            }
        }
    }
}