using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeThrowSceneManager : BasketBallGOSceneManager
{

    public override void PlayerTapped(GameObject player)
    {
        print("CaptureSceneManager.PlayerTapped activated");
    }

    public override void SpotTapped(GameObject spot)
    {
        print("CaptureSceneManager.DroidTapped activated");
    }




    /*
     * In the function that detects when the game is over
     * And we want to go back to the map :
     *                  SceneTransitionManager.Instance.GoToScene(BasketBallGOConstants.SCENE_WORLD, new List<GameObject>());;

    */
}
