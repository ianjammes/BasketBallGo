using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Not instantiating, template for child classes
public abstract class BasketBallGOSceneManager : MonoBehaviour
{

    public abstract void PlayerTapped(GameObject player);
    public abstract void SpotTapped(GameObject spot);

}
