﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ballScored : MonoBehaviour
{
    public static int scoreRound;
    private Text score;

    private Animator scoredTextAnim;

    private Text ballValueTxt;
    private int ballValue; //Ball value when shot was made


    private void Start()
    {
        score = GameObject.Find("Text").GetComponent<Text>();
        ballValueTxt = GameObject.Find("DistanceTest").GetComponent<Text>();
        scoredTextAnim = GameObject.Find("Scored Text").GetComponent<Animator>();
        scoreRound = 0;
    }

    void Update()
    {
        //Total points display of the round------------------------------------
        string r = "";
        r += scoreRound.ToString("0 0");
        score.text = r;

        //Defining value of the shot---------------------------------------------

        //If finger was not released yet
        if (BallControl.finalBallValue == 0)
        {

            if (BallControl.distHoopCam <= 4.0f)
            {
                //Ball value is 1 point
                ballValue = 1;
            }
            else if (BallControl.distHoopCam > 4.0f & BallControl.distHoopCam <= 7.0f)
            {
                //Ball value is 2 point
                ballValue = 2;
            }
            else
            {
                //Ball value is 3 point
                ballValue = 3;
            }
            ballValueTxt.text = "+" + ballValue.ToString();
        }

        //If shot comes opposite direction
        if(triggerWasIn2.wasHit == true)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            StartCoroutine("trigerred");
        }
    }

    //Adding a point; it's in
    private IEnumerator trigerred()
    {
        GameManager.Instance.CurrentPlayer.AddXp(BallControl.finalBallValue);
        scoreRound += BallControl.finalBallValue;
        scoredTextAnim.SetBool("scored", true);
        PlaceHoop.scoredText.text = "+" + BallControl.finalBallValue.ToString();
        PlaceHoop.scoredText.enabled = true;
        gameObject.GetComponent<BoxCollider>().enabled = false;

        yield return new WaitForSeconds(2);

        scoredTextAnim.SetBool("scored", false);
        PlaceHoop.scoredText.enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = true;

    }
}
