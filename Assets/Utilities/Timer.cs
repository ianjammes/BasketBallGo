using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{

    public static float msToWait = 120000; 

    private Text timerText;
    private ulong hourGet0s;
    public static float secondsLeft;

    //private ulong horaAdeu;
    //private ulong horaActual;
    public static ulong sinceLastTime;
    //public static int tempsForaTotal;

    void Start()
    {
        timerText = GetComponentInChildren<Text>();

        //Getting the time player has been out of the app since last time
        //horaActual = (ulong)DateTime.Now.Ticks;
        //sinceLastTime = (horaActual - ulong.Parse(PlayerPrefs.GetString("horaAdeu151").ToString())) / TimeSpan.TicksPerMillisecond;

        //Time
        hourGet0s = ulong.Parse(PlayerPrefs.GetString("click342"));

        //Getting total time player out of the app
        //tempsForaTotal = PlayerPrefs.GetInt("tempsForatotal49");
        //tempsForaTotal += (int)sinceLastTime;
        //PlayerPrefs.SetInt("tempsForatotal49", tempsForaTotal);
    }

    void Update()
    {

        if (AreCheckpointsReady())
        {
            resetTimer(); //Reinitialize the timer
            return;
        }

        //Set the timer
        ulong hourSince0 = (ulong)DateTime.Now.Ticks - hourGet0s;
        ulong m = hourSince0 / TimeSpan.TicksPerMillisecond;
        secondsLeft = (float)(msToWait - m) / 1000.0f;


        string r = "";
        //Hours
        //r += ((int)secondsLeft / 3600).ToString() + "h ";
        //secondsLeft -= ((int)secondsLeft / 3600) * 3600;
        //Minutes
        r += ((int)secondsLeft / 60).ToString("00") + "m ";
        //Seconds
        r += (secondsLeft % 60).ToString("00") + "s ";

        timerText.text = r;
    }

    public void resetTimer() 
    {
        hourGet0s = (ulong)DateTime.Now.Ticks; //Canviat aixoooo 
        PlayerPrefs.SetString("click342", hourGet0s.ToString());
    }

    private bool AreCheckpointsReady() //Check if we reach 0
    {
        ulong diff = (ulong)DateTime.Now.Ticks - hourGet0s;
        ulong m = diff / TimeSpan.TicksPerMillisecond;
        secondsLeft = (float)(msToWait - m) / 1000.0f;

        if (secondsLeft < 0)
        {
            return true;
        }
        return false;
    }

    //private void OnApplicationQuit()
    //{
    //    horaAdeu = (ulong)DateTime.Now.Ticks;
    //    PlayerPrefs.SetString("horaAdeu151", horaAdeu.ToString());
    //}
}

