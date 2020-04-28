using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

[Serializable]
public class BasketBall_spot : MonoBehaviour
{
    ////Vector3 position workaround-----------------------------------------------

    [SerializeField] private Vector3 position;
    public Vector3 someVector3Info
    {
        get
        {
            if (position == null)
            {
                return Vector3.zero;
            }
            else
            {
                return (Vector3)position;
            }
        }
        set
        {
            position = (Vector3)value;
        }
    }

    ////--------------------------------------------------------------------------

    [SerializeField] private float spawnRate = 0.10f; //How often they spawn
    [SerializeField] private float catchRate = 0.10f;
    [SerializeField] private int attack = 0; //Damgage of the attack
    [SerializeField] private int defense = 0;
    [SerializeField] private int index;
    [SerializeField] private AudioClip spotTouchedSound;
    

    private AudioSource audioSource;

    //private string path;

    //Getters-----------------------------------------------
    public float SpawnRate
    {
        get { return spawnRate; }
    }

    public float CatchRate
    {
        get { return catchRate; }
    }

    public int Attack
    {
        get { return attack; }
    }

    public int Defense
    {
        get { return defense; }
    }

    public int Index
    {
        get { return index; }
    }

    public Vector3 Position
    {
        get { return position; }
    }

    public AudioClip DroidTouchedSound
    {
        get { return spotTouchedSound; }
    }
    //------------------------------------------------------

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        Assert.IsNotNull(audioSource);
        Assert.IsNotNull(spotTouchedSound);

        position = transform.position;
    }

    //Checking if the currently active scene contains a BasketBallGOSceneManager on the tapped spot
    private void OnMouseDown()
    {

        audioSource.PlayOneShot(spotTouchedSound);

        Destroy(gameObject); //Delete sprite

        for(int i = 0; i < BasketSpotsFactory.liveSpots.Count; i++)
        {
            if(BasketSpotsFactory.liveSpots[i].position == this.position)
            {
                Debug.Log("El spot a eliminar està a la posició " + i);
                BasketSpotsFactory.listaI.RemoveAt(BasketSpotsFactory.listaI[i]);
                
            }
        }

        BasketSpotsFactory.liveSpots.Remove(this); //Delete from current spots on scene

       // BasketSpotsFactory.contador--; //Delete from file
        Debug.Log(BasketSpotsFactory.contador);
       // BasketSpotsFactory.index--;

        //Updating the spots number
        PlayerPrefs.SetInt("spots631",BasketSpotsFactory.contador); //Update for the new scene
        PlayerPrefs.Save();

        SceneManager.LoadScene(1); //Going to basketball scene
    }

}
