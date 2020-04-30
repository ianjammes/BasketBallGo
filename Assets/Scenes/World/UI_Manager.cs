using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{

    [SerializeField] private Text xpText;
    [SerializeField] private Text levelText;
    [SerializeField] private AudioClip menuBtnSound;

    private AudioSource audioSouce;

    //Make sure all the variabes are set and none is empty
    private void Awake()
    {
        audioSouce = GetComponent<AudioSource>();

        Assert.IsNotNull(audioSouce);
        Assert.IsNotNull(xpText);
        Assert.IsNotNull(levelText);
        Assert.IsNotNull(menuBtnSound);
    }

    private void Update()
    {
        UpdateLevel();
        UpdateXP();
    }

    //Keep updating the levelText
    //Directly through the GameManger and Player classes
    //We're using this method, better for memory intensive situations, saving computer power, more control
    public void UpdateLevel()
    {
        levelText.text = GameManager.Instance.CurrentPlayer.Lvl.ToString();
    }

    //Keep updating the xpText
    public void UpdateXP()
    {
        xpText.text = GameManager.Instance.CurrentPlayer.Xp + " / " + GameManager.Instance.CurrentPlayer.RequiredXp;
    }

    //public void MenuBtnClick()
    //{
    //    audioSouce.PlayOneShot(menuBtnSound);
    //}

}
