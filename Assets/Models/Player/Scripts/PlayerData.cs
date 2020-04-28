using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable] //Used to write data and class objects to a file, for saving the game
public class PlayerData
{
    private int xp;
    private int requiredXp;
    private int levelBase;
    private int lvl;
    //private List<BasketBall_spotData> spots;

    //Getters-------------------------------------------------------

    public int Xp { get { return xp; } }
    public int RequiredXp { get { return requiredXp; } }  
    public int LevelBase { get { return levelBase; } }
    public int Lvl { get { return lvl; } }
    //public List<BasketBall_spotData> Spots { get { return spots; } }

    //--------------------------------------------------------------

    public PlayerData (Player player)
    {
        xp = player.Xp;
        requiredXp = player.RequiredXp;
        levelBase = player.LevelBase;
        lvl = player.Lvl;

        //foreach(GameObject spotObject in player.Spots) {
        //    BasketBall_spot basketBall_Spot = spotObject.GetComponent<BasketBall_spot>();
        //    if(basketBall_Spot != null) {
        //        BasketBall_spotData data = new BasketBall_spotData(basketBall_Spot);
        //        Spots.Add(data);
        //    }
        //}
    }

}
