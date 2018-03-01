using UnityEngine;
using System.Collections;

public class PatrolPlayerInteraction : MonoBehaviour
{
    PatrolPosition patrolPos;
    Animator patrolAnim;
    Animator playerAnim;

//    [SerializeField]
    float speedRot = 200f;

    void Start()
    {
        patrolPos = GetComponent<PatrolPosition>();
        patrolAnim = GetComponentInChildren<Animator>();
//        playerAnim = GetComponentInChildren<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        PathfindingManager.Instance.tilePath.Clear();

        InputManager.Instance.enabled = false;




      
        Vector3 targetDir = new Vector3(other.transform.position.x - transform.position.x, 0, other.transform.position.z - transform.position.z);
        float step = speedRot * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);

        Vector3 targetDirPlayer = new Vector3(transform.position.x - other.transform.position.x, 0, transform.position.z - other.transform.position.z);
        float stepPlayer = speedRot * Time.deltaTime;
        Vector3 newDirPlayer = Vector3.RotateTowards(other.transform.forward, targetDirPlayer, step, 0.0f);
        other.transform.rotation = Quaternion.LookRotation(newDirPlayer);







        patrolPos.enabled = false;
        patrolAnim.SetBool("IsWalking", false);
        patrolAnim.SetBool("IsAttacking", true);

        Invoke("AttackToFalse", 1f);

        playerAnim = other.GetComponent<Animator>();
        playerAnim.SetBool("IsDying", true);


     

     //   LevelManager.Instance.ReloadLevel();
    }

    void AttackToFalse()
    {
        patrolAnim.SetBool("IsAttacking", false);
        patrolAnim.SetBool("IsWalking", false);
    }
}