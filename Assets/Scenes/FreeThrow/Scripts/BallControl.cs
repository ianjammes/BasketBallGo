using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class BallControl : MonoBehaviour
{
    //Force of the throw
    public float throwForce = 100.0f;

    //X and Y axis damping factors for the throw direction
    public float throwDirectionX = 0.20f;
    public float throwDirectionY = 0.50f;

    //Offset of the ball's position in relation to camera's position
    public Vector3 ballCameraOffset = new Vector3(0f, -0.4f, -1f);

    //Information about the current throw
    private Vector3 startPosition;
    private Vector3 direction;
    private float startTime;
    private float endTime;
    private float duration;
    public static bool directionChosen = false;
    private bool throwStarted = false;
    private bool firsTime;
    public static bool movingScene = false;
    private int shotsLeft = 1;
    public static float distHoopCam; //Distance between hoop and player
    private float finalPosition;
    public static int finalBallValue = 0;


    [SerializeField] GameObject ARCam;
    [SerializeField] ARSessionOrigin sessionOrigin;
    [SerializeField] ParticleSystem trailObject;

    Rigidbody rb;

    private Transform hoopPos;





    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        sessionOrigin = GameObject.Find("AR Session Origin").GetComponent<ARSessionOrigin>();
        ARCam = sessionOrigin.transform.Find("AR Camera").gameObject;
        transform.parent = ARCam.transform; //Position is where the camera is
        trailObject = GameObject.Find("Trail").GetComponent<ParticleSystem>();
        firsTime = true;
        trailObject.Play(false);
        ResetBall();



        hoopPos = GameObject.Find("hoop").GetComponent<Transform>();

    }

    private void Update()
    {

        //Just touched the screen, the throw will start
        if (Input.GetMouseButtonDown(0) & firsTime == false) //When we press
        {
            startPosition = Input.mousePosition;
            startTime = Time.time;
            throwStarted = true;
            directionChosen = false;
        }
        //Touch with the screen ended, this will throw the ball
        else if (Input.GetMouseButtonUp(0) & firsTime == false) //When we release
        {
            endTime = Time.time; //Time we release the finger
            duration = endTime - startTime;
            direction = Input.mousePosition - startPosition; //Where we released finger - start finger position
            directionChosen = true;
        }

        //If direction was chosen the ball is released
        if (directionChosen)
        {
            rb.mass = 1;
            rb.useGravity = true;
            trailObject.Play(true);

            rb.AddForce(
                ARCam.transform.forward * throwForce / duration +
                ARCam.transform.up * direction.y * throwDirectionY +
                ARCam.transform.right * direction.x * throwDirectionX);

            startTime = 0.0f;
            duration = 0.0f;

            startPosition = new Vector3(0, 0, 0);
            direction = new Vector3(0, 0, 0);
            throwStarted = false;
            directionChosen = false;
            shotsLeft--;

            //Setting the ball value for the shot
            finalPosition = distHoopCam;

            if (finalPosition <= 4.0f)
            {
                //Ball value is 1 point
                finalBallValue = 1;
            }
            else if (finalPosition > 4.0f & finalPosition <= 7.0f)
            {
                //Ball value is 2 point
                finalBallValue = 2;
            }
            else
            {
                //Ball value is 3 point
                finalBallValue = 3;
            }

        }

        //5 seconds after throwing the ball, we reset its position
        if (Time.time - endTime >= 4)
        {
            if (shotsLeft > 0)
            {
                ResetBall();
            }
            else
            {
                //Move to the main scene
                movingScene = true;
                SceneManager.LoadScene(0);

            }
    }


        //distHoopCam = PlaceHoop.spawnedHoop.transform.position.z - gameObject.transform.position.z; //Distance to the hoop
        distHoopCam = hoopPos.transform.position.z - gameObject.transform.position.z; //Test computer

        firsTime = false;
    }



    public void ResetBall()
    {
        //Ball floating in front of the camera until next throw
        rb.mass = 0;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        endTime = 0.0f;
        trailObject.Play(false);

        Vector3 ballPos = ARCam.transform.position +
                          ARCam.transform.forward * ballCameraOffset.z +
                          ARCam.transform.up * ballCameraOffset.y;

        transform.position = ballPos;
        finalBallValue = 0;
    }
}
