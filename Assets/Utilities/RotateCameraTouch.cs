using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCameraTouch : MonoBehaviour
{

    private Touch initTouch = new Touch();

    private float rotY = 0f;
    private Vector3 origRot;
    public float rotSpeed = 0.5f;

    private void Start()
    {
        origRot = transform.eulerAngles;
        rotY = origRot.y;
    }

    void FixedUpdate()
    {
        foreach(Touch touch in Input.touches) { //Detecting all the touches

            if(touch.phase == TouchPhase.Began) { //First touch on screen
                initTouch = touch; //our current touch
            }
            else if(touch.phase == TouchPhase.Moved) {
                //swiping
                float deltaX = initTouch.position.x - touch.position.x ; //Distance between initial touch and current touch
                rotY += deltaX * Time.deltaTime * rotSpeed;
                transform.eulerAngles = new Vector3(0f, rotY, 0f);
            }
            else if(touch.phase == TouchPhase.Ended) {
                //Reset touch
                initTouch = new Touch();
            }
        }
    }
}
