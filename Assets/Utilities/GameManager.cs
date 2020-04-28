using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

//Class GameManager to manage all the variables, states, scenes... of the game

public class GameManager : Singleton<GameManager>
{

    private Player currentPlayer;
    private BasketBall_spot currentSpots;

    //Getters----------------------------------------------------------

    //Accessing all the variables related to the player (level, xp, XPRequired...)
    public Player CurrentPlayer
    {
        get {
            //Changing scenes there's not time to get the player, so if it's null we create a Player component
            if (currentPlayer == null) {
                currentPlayer = gameObject.AddComponent<Player>();
            }
            return currentPlayer;
        }
    }

    public BasketBall_spot CurrentSpots
    {
        get
        {
            //Changing scenes there's not time to get the player, so if it's null we create a Player component
            if (currentSpots == null)
            {
                currentSpots = gameObject.AddComponent<BasketBall_spot>();
            }
            return currentSpots;
        }
    }



    //------------------------------------------------------------------







}
