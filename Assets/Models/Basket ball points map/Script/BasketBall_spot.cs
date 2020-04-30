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

    [SerializeField] private AudioClip spotTouchedSound;

    private bool closeEnough = false;
    private AudioSource audioSource;

    //private string path;

    //Getters-----------------------------------------------
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            closeEnough = true;
        }
    }

    //Checking if the currently active scene contains a BasketBallGOSceneManager on the tapped spot
    private void OnMouseDown()
    {
        if (closeEnough)
        {
            audioSource.PlayOneShot(spotTouchedSound);

            Destroy(gameObject); //Delete sprite

            for (int i = 0; i < BasketSpotsFactory.liveSpots.Count; i++)
            {
                if (BasketSpotsFactory.liveSpots[i].position == this.position)
                {
                    Debug.Log("El spot a eliminar està a la posició " + i);
                    BasketSpotsFactory.listaI.RemoveAt(BasketSpotsFactory.listaI[i]);

                    //Deleting from saved data
                    PlayerPrefs.DeleteKey("posX707" + i.ToString());
                    PlayerPrefs.DeleteKey("posY707" + i.ToString());
                    PlayerPrefs.DeleteKey("posZ707" + i.ToString());
                }
            }

            BasketSpotsFactory.liveSpots.Remove(this); //Delete from current spots on scene

            BasketSpotsFactory.contador--; //Delete from file
            Debug.Log(BasketSpotsFactory.contador);

            //Updating the spots number
            PlayerPrefs.SetInt("spots707", BasketSpotsFactory.contador); //Update for the new scene
            PlayerPrefs.Save();

            closeEnough = false;

            SceneManager.LoadScene(1); //Going to basketball scene
        }
    }

}
