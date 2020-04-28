using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Manager to move to correct scenes
public class WorldSceneManager : BasketBallGOSceneManager
{

    public override void PlayerTapped(GameObject player)
    {

    }

    //Loading the capture scene when the droid is tapped
    public override void SpotTapped(GameObject spot)
    {
        //SceneManager.LoadScene(PocketDroidsConstants.SCENE_CAPTURE, LoadSceneMode.Additive);
        List<GameObject> list = new List<GameObject>();
        //list.Add(spot);
        SceneTransitionManager.Instance.GoToScene(BasketBallGOConstants.SCENE_THROW, list);
    }

}
