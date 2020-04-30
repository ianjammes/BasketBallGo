using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BasketBall_spotData
{

    private string droidTouchedSound;
    private Vector3Serialize position;

    //Getters-------------------------------------------------------

    public string DroidTouchedSound { get { return droidTouchedSound; } }
    public Vector3Serialize Position {  get { return position; } }

    //--------------------------------------------------------------

    public BasketBall_spotData(BasketBall_spot basketBall_Spot)
    {
        AudioClip clip;
        droidTouchedSound = basketBall_Spot.DroidTouchedSound.name;
        position = basketBall_Spot.Position;
    }
}