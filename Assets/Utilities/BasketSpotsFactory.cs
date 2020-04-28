using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

[Serializable]
public class BasketSpotsFactory : Singleton<BasketSpotsFactory>
{

    [SerializeField] private BasketBall_spot basketballGameSpot; //BasketBall spot we're spawning
    //[SerializeField] private float waitTime = 3600.0f; //How long we'll wait before generating spots (every 1 hour)
    [SerializeField] private int maximumSpots = 50; //Initial number of spots created
    [SerializeField] private float minRange = 5.0f; //Minimum range for the spots to appear
    [SerializeField] private float maxRange = 50.0f; //Maximum range for the spots to appear

    public static List<BasketBall_spot> liveSpots = new List<BasketBall_spot>(); //Reference in the script to actual spots showed in screen
    public static int contador;
    private BasketBall_spot selectedSpot;
    private Player player; //The instance of our player

    public static int i;
    private int newI;
    public static int index = 0;

    private int firstTime = 0; //Checking if first time in the app

    private bool moreSpots; //Knowing if need of more spots

    public static List<int> listaI = new List<int>(); //Reference in the script to actual spots showed in screen


    //Animation
    public GameObject playerCoords;
    private Vector3 last_player_pos;
    private Vector3 current_player_pos;
    [SerializeField] private Animator animator;



    //Getters----------------------------------------------------------

    public List<BasketBall_spot> LiveDroids
    {
        get { return liveSpots; }
    }

    public BasketBall_spot SelectedDroid
    {
        get { return selectedSpot; }
    }

    //------------------------------------------------------------------

    //Assertions: make sure we always got a player and list of droids before running the app
    private void Awake()
    {
        Assert.IsNotNull(basketballGameSpot);
    }

    //---------------------------------------------------------------------------------

    void Start()
    {
        player = GameManager.Instance.CurrentPlayer; //Putting a player in the map
        Assert.IsNotNull(player);


        last_player_pos = playerCoords.transform.position; //Last position of the player

        firstTime = PlayerPrefs.GetInt("savedFirstTime631"); //First time player playing the game ?

        i = 0;
        index = 0;
        newI = 0;

        liveSpots.Clear();
        contador = PlayerPrefs.GetInt("spots631", 0);


        //First time player runnig the game
        if (firstTime == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                Debug.Log("Els 5 primers");
                InstantiateSpot();
                PlayerPrefs.SetInt("spots631", liveSpots.Count);
            }
        }

        //Resetting position and names when we come back from mini game
        if (BallControl.movingScene == true)
        {

            for (int j = 0; j < contador; j++)
            {
             
                if (listaI.Contains(j))
                {
                    // Debug.Log("Te l'indice" + j);
                    Debug.Log("iii" + i);

                    float x = player.transform.position.x + GenerateRange(); //From the player position adding a random range
                    float y = player.transform.position.y + 2.0f;
                    float z = player.transform.position.z + GenerateRange(); //From the player position adding a random range
                    liveSpots.Add(Instantiate(basketballGameSpot, new Vector3(PlayerPrefs.GetFloat("posX631" + i.ToString(), x), PlayerPrefs.GetFloat("posY631" + i.ToString(), y), PlayerPrefs.GetFloat("posZ631" + i.ToString(), z)), Quaternion.identity));

                    PlayerPrefs.SetFloat("posX631" + i.ToString(), liveSpots[index].Position.x);
                    PlayerPrefs.SetFloat("posY631" + i.ToString(), liveSpots[index].Position.y);
                    PlayerPrefs.SetFloat("posZ631" + i.ToString(), liveSpots[index].Position.z);

                    //listaI.Add(i);

                    i++;
                    index++;
                    PlayerPrefs.Save();

                }
                else
                {
                    //Debug.Log("No té l'indice, no el poso" + j);
                    //i++;
                }

            }
            PlayerPrefs.SetInt("spots631", liveSpots.Count);

            Debug.Log("Live spooots" + liveSpots.Count);
            Debug.Log("contadooor" + contador);
            BallControl.movingScene = false;
        }

    }

    //------------------------------------------------------------------------------------------
    private void Update()
    {

        current_player_pos = playerCoords.transform.position;

        if (current_player_pos != last_player_pos)
        {
            StartCoroutine(AnimationTiming());
        }

        last_player_pos = current_player_pos; //Updating the player position



        if (Timer.secondsLeft <= 0)
        {
            Debug.Log("aqui ha passat 1 hora");
            moreSpots = true;
            AreCheckpointsReady();
        }
        //else if (liveSpots.Count == 0 & Timer.secondsLeft > 0) //
        //{
        //    Debug.Log("A esperar brother");
        //}
        else if (liveSpots.Count == 0)
        {
            Debug.Log("aqui acabem d'arribar");
            AreCheckpointsReady();
        }

        StartCoroutine(waitASecondBeforeUpdate());
       


    }

    private bool AreCheckpointsReady() //Check if we reach 0
    {           
        contador = PlayerPrefs.GetInt("spots631", 0);
         
        //Fins que no estem al tope de checkpoints---------------------------------
        if (PlayerPrefs.GetInt("spots631", 0) < maximumSpots)
        {

            if (firstTime != 0 & contador <= 45 & liveSpots.Count != 0) //Estan a l'escena i en hi ha 45 max
            {
                

                for (int i = 0; i < 5; i++)
                {
                    Debug.Log("estem a 45 o menys poso 5");
                    InstantiateSpot();
                    contador += 1;
                    PlayerPrefs.SetInt("spots631", liveSpots.Count);
                }
                //yield return new WaitForSeconds(waitTime);
            }

            else if (liveSpots.Count == 0 & moreSpots == true) //No estan a l'escena i ha passat 1 hora (+5 dispo)
            {
                int newContador = contador + 5;
                //i++;
                for (int i = 0; i < newContador; i++)
                {
                    Debug.Log("en tinc de disponibles i acabo d'entrar a l'app amb spots de recompensa");
                    //contador += 1;
                    InstantiateSpot();
                    PlayerPrefs.SetInt("spots631", liveSpots.Count);
                }
            }

            else if (liveSpots.Count == 0) //No estan a l'escena i no ha passat 1 hora encara
            {
                for (int i = 0; i < contador; i++)
                {
                    Debug.Log("en tinc de disponibles i acabo d'entrar a l'app");
                    InstantiateSpot();
                    PlayerPrefs.SetInt("spots631", liveSpots.Count);
                }
            }

        


            else
            {
                Debug.Log("Res a fer");
            }

            PlayerPrefs.Save();
            firstTime = 1;
            PlayerPrefs.SetInt("savedFirstTime631", firstTime);
            moreSpots = false;

        }
        //-------------------------------------------------------------------------

        //Si ja estem al tope de 50 checkpoints------------------------------------
        else
        {
            if (liveSpots.Count == 0) //No estan a l'escena
            {
                for (int i = 0; i < contador; i++)
                {
                    Debug.Log("DDDDDDD");
                    InstantiateSpot();
                    PlayerPrefs.SetInt("spots631", liveSpots.Count);
                }
            }
            else
            {
                Debug.Log("Estem a 50");
                //yield return new WaitForSeconds(waitTime);
            }
            PlayerPrefs.Save();
        }

        Debug.Log(liveSpots.Count);
        Debug.Log(contador);


        return true;

      
    }

    //Put spots in the map
    private void InstantiateSpot()
    {

        float x = player.transform.position.x + GenerateRange(); //From the player position adding a random range
        float y = player.transform.position.y + 2.0f;
        float z = player.transform.position.z + GenerateRange(); //From the player position adding a random range
        liveSpots.Add(Instantiate(basketballGameSpot, new Vector3(PlayerPrefs.GetFloat("posX631" + i.ToString(), x), PlayerPrefs.GetFloat("posY631" + i.ToString(), y), PlayerPrefs.GetFloat("posZ631" + i.ToString(), z)), Quaternion.identity));

        PlayerPrefs.SetFloat("posX631" + i.ToString(), liveSpots[index].Position.x);
        PlayerPrefs.SetFloat("posY631" + i.ToString(), liveSpots[index].Position.y);
        PlayerPrefs.SetFloat("posZ631" + i.ToString(), liveSpots[index].Position.z);

        listaI.Add(i);

        i++;
        index++;
        PlayerPrefs.Save();       

       
    }

    //Randomness of the droid location
    private float GenerateRange()
    {
        float randomNum = UnityEngine.Random.Range(minRange, maxRange); //Only positive values
        bool isPositive = UnityEngine.Random.Range(0, 10) < 5; //50% chances to get under 5 or above (negative or positive)
        return randomNum * (isPositive ? 1 : -1);
    }



    //Settings for the animation of the character--------------------------------------------------


    private IEnumerator AnimationTiming()
    {
        animator.SetBool("moving", true);
        
        yield return new WaitForSeconds(3.0f);

        animator.SetBool("moving", false);
    }

    //----------------------------------------------------------------------------------------------

    private IEnumerator waitASecondBeforeUpdate()
    {
        yield return new WaitForSeconds(1f);
    }

    private void OnApplicationQuit()
    {
        for(int k = 0; k < liveSpots.Count; k++)
        {
            PlayerPrefs.SetFloat("posX631" + k.ToString(), liveSpots[k].Position.x);
            PlayerPrefs.SetFloat("posY631" + k.ToString(), liveSpots[k].Position.y);
            PlayerPrefs.SetFloat("posZ631" + k.ToString(), liveSpots[k].Position.z);
        }

    }

}




/* Recordatori !
 * Canvia nom de PlayerPrefs a :
 * BasketBall_spot : PlayerPrefs.SetInt("spots631")
 * BasketSpotsFactory : "posX631"; "savedFirstTime631"; "spots173"
 * Timer : "horaAdeu3", "tempsTotal30"
 */
