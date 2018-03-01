using UnityEngine;
using System.Collections;

/*
 ====================================================================
 Author:			Tom Clark

 Purpose:			To read player input and convert it to feedback.
 Notes:				
 
 ====================================================================
*/

public class InputManager : GenericSingleton<InputManager>
{
    float inputTimer;
    public AudioClip phaseSound;
    bool timerStarted = false;

    public bool tileIsPhasable = false;

    bool hasClicked = false;

	[SerializeField]
	float doubleClickTimingWindow = 0.2f;

    void Update()
    {
        //SpaceBar ();

        if (!PlayerPhasing.isPhasing && !PlayerPosition.player.GetComponent<Animator>().GetBool("IsWheeling") && !PlayerPosition.player.GetComponent<Animator>().GetBool("IsRoping"))
        { 
            Click();
        }

        if (timerStarted)
        {
            inputTimer += Time.deltaTime;
        }
    }

    void Click()
    {

        if (timerStarted && Input.GetMouseButtonDown(0))
        {
            timerStarted = false;

            inputTimer = 0;

            TargetPosition.SelectTile();

            PlayerPosition.GetPlayerTile();

            PlayerPhasing.PlayerPhase();

            AudioManager.Instance.PlayFX (phaseSound);

            if (tileIsPhasable && PlayerPosition.player.transform.position.x == PlayerPosition.playerTile.transform.position.x && PlayerPosition.player.transform.position.z == PlayerPosition.playerTile.transform.position.z)
            {
                PlayerPhasing.isPhasing = true;

                hasClicked = true;
            }


        }

        if (Input.GetMouseButtonDown(0))
        {  


			TargetPosition.SelectTile();

			PlayerPosition.GetPlayerTile();

			PlayerPhasing.PlayerPhase();

			if (!tileIsPhasable)
				inputTimer = 1;


			if (tileIsPhasable)
            timerStarted = true;
        }

        if (hasClicked && !PlayerPhasing.isPhasing)
        {
            hasClicked = false;

            timerStarted = false;

            inputTimer = 0;
        }

		if (inputTimer > doubleClickTimingWindow && !hasClicked)
        {
            timerStarted = false;

			tileIsPhasable = false;

            inputTimer = 0;

            TargetPosition.SelectTile();

            if (PathfindingManager.Instance.tilePath.Count == 0)
            {
                PlayerPosition.GetPlayerTile();
            }
            else if (PathfindingManager.Instance.tilePath.Count != 0)
            {
                PlayerPosition.playerTile = PathfindingManager.Instance.tilePath[0];
            }

            //If the player tile and target tile cords have both been found, the A* pathfinding begins.
            PathfindingManager.Instance.FindAstarPath();

        }



    }

//    void SpaceBar()
//    {
//        if (Input.GetKeyDown(KeyCode.Space))
//        {
//            PlayerPosition.GetPlayerTile();
//
//            //Checks if there are switchable tiles which can be switched, and if so, switches their states.
//            ColourSwitch.Instance.CheckSwitch();
//        }
//    }
}