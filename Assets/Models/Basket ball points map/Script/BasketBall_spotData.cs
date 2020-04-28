using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BasketBall_spotData
{

    private float spawnRate;
    private float catchRate;
    private int attack;
    private int defense;
    private int index;
    private string droidTouchedSound;
    private Vector3Serialize position;

    //Getters-------------------------------------------------------

    public float SpawnRate { get { return spawnRate; } }
    public float CatchRate { get { return catchRate; } }
    public int Attack { get { return attack; } }
    public int Defense { get { return defense; } }
    public int Index { get { return index; } }
    public string DroidTouchedSound { get { return droidTouchedSound; } }
    public Vector3Serialize Position {  get { return position; } }

    //--------------------------------------------------------------

    public BasketBall_spotData(BasketBall_spot basketBall_Spot)
    {
        spawnRate = basketBall_Spot.SpawnRate;
        catchRate = basketBall_Spot.CatchRate;
        attack = basketBall_Spot.Attack;
        defense = basketBall_Spot.Defense;
        index = basketBall_Spot.Index;
        AudioClip clip;
        droidTouchedSound = basketBall_Spot.DroidTouchedSound.name;
        position = basketBall_Spot.Position;
    }
}