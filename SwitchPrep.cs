using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SwitchPrep : MonoBehaviour
{
   

    // Use this for initialization
    void Start()
    {
        
    }
	
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.GetComponent<SwitchPrep>())
                {
                    PlayerPosition.GetPlayerTile();

                    //Checks if there are switchable tiles which can be switched, and if so, switches their states.
                    ColourSwitch.Instance.CheckSwitch();
                }
            }
           
        }
    }

    void SwitchTiles()
    {
        foreach (Animator tileAnim in ColourSwitch.Instance.tileAnims)
        {
            bool currentAnimState = tileAnim.GetBool("isSwitched");
            tileAnim.SetBool("isSwitched", !currentAnimState);
        }



        if (SceneManager.GetActiveScene().buildIndex < 13)
        {
            PlayerPosition.player.GetComponent<Animator>().SetBool("IsWheeling", false);
        }
        else if (SceneManager.GetActiveScene().buildIndex >= 13)
        {
            PlayerPosition.player.GetComponent<Animator>().SetBool("IsRoping", false);
        }



        //  PlayerPosition.player.GetComponent<Animator>().SetBool("IsWheeling", false);
        //   PlayerPosition.player.GetComponent<Animator>().SetBool("IsRoping", false);



        int xTile = (int)PlayerPosition.playerTile.GetTile().x;
        int yTile = (int)PlayerPosition.playerTile.GetTile().y;

        if (PathfindingManager.Instance.currentTileGrid[xTile + 1, yTile].isSwitch)
        {
            PathfindingManager.Instance.currentTileGrid[xTile + 1, yTile].GetComponentInChildren<Animator>().SetBool("IsTurning", false);
        }
        if (PathfindingManager.Instance.currentTileGrid[xTile - 1, yTile].isSwitch)
        {
            PathfindingManager.Instance.currentTileGrid[xTile - 1, yTile].GetComponentInChildren<Animator>().SetBool("IsTurning", false);
        }
        if (PathfindingManager.Instance.currentTileGrid[xTile, yTile + 1].isSwitch)
        {
            PathfindingManager.Instance.currentTileGrid[xTile, yTile + 1].GetComponentInChildren<Animator>().SetBool("IsTurning", false);
        }
        if (PathfindingManager.Instance.currentTileGrid[xTile, yTile - 1].isSwitch)
        {
            PathfindingManager.Instance.currentTileGrid[xTile, yTile - 1].GetComponentInChildren<Animator>().SetBool("IsTurning", false);
        }
    }
}
