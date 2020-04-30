using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceHoop : MonoBehaviour
{
    //Hoop-------------------------------------------------------------------
    [SerializeField]
    [Tooltip("Instantiates the hoop on a plane at the touch location.")]
    GameObject hoopPrefab;

    // The prefab to instantiate on touch.
    public GameObject placedHoop
    {
        get { return hoopPrefab; }
        set { hoopPrefab = value; }
    }

    // The object instantiated as a result of a successful raycast intersection with a plane.
    public static GameObject spawnedHoop { get; private set; }

    //-----------------------------------------------------------------------

    //Ball-------------------------------------------------------------------
    [SerializeField]
    [Tooltip("Instantiates the ball in front of the AR Camera")]
    GameObject ballPrefab;

    // The prefab to instantiate on touch.
    public GameObject placedBall
    {
        get { return ballPrefab; }
        set { ballPrefab = value; }
    }

    // The object instantiated as a result of a successful raycast intersection with a plane.
    public static GameObject spawnedBall { get; private set; }

    //----------------------------------------------------------------------

    // Invoked whenever an object is placed in on a plane.
    public static event Action onPlacedObject;

    private bool isPlaced = false;

    public static Text scoredText;


    public static GameObject summary;
    public static Text totalPoints;

    ARRaycastManager m_RaycastManager;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();

        scoredText = GameObject.Find("Scored Text").GetComponent<Text>();
        scoredText.enabled = false;

        summary = GameObject.Find("Summary");
        totalPoints = GameObject.Find("Total Points").GetComponent<Text>();
        summary.SetActive(false);
    }

    void Update()
    {
        if (isPlaced) //Making sure the hoop wasn't already placed
            return;


        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (m_RaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = s_Hits[0].pose;

                    //Instantiating the hoop
                    //spawnedHoop = Instantiate(hoopPrefab, hitPose.position, Quaternion.AngleAxis(180,Vector3.up));
                    spawnedHoop = Instantiate(hoopPrefab, new Vector3(hitPose.position.x, hitPose.position.y - 1, hitPose.position.z + 4), Quaternion.AngleAxis(180, new Vector3(0,1,0)));
                    spawnedHoop.transform.parent = transform.parent; //Setting the parent as the AR Session origin

                    isPlaced = true;

                    //Instantiating the ball
                    spawnedBall = Instantiate(ballPrefab);
                    spawnedBall.transform.parent = m_RaycastManager.transform.Find("AR Camera").gameObject.transform;

                    if (onPlacedObject != null)
                    {
                        onPlacedObject();
                    }
                }
            }
        }

    }
}
