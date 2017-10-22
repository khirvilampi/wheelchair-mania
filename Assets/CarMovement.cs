using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour {

    public float MotorForceFR;
    public float MotorForceFL;
    public float SteerForce;
    public float torque;
    public float brakeTorque;
    public float v;
    public float a;
    public WheelCollider WheelColFR;
    public WheelCollider WheelColFL;
    public WheelCollider WheelColRR;
    public WheelCollider WheelColRL;
    public int ti;
    public bool leftBreakOn;
    public bool rightBreakOn;


    public float startPosX;
    public float startPosLeft;
    public float endPosLeft;
    public float startPosRight;
    public float endPosRight;
    public float distLeft;
    public float distRight;

    private GUIStyle guiStyle = new GUIStyle(); //create a new variable


    void Start()
    {
    }
    // Update is called once per frame
    void Update () {

        /*if (Input.touchCount > 0)
        {
            // Get movement of the finger since last frame
            //touch1 = Input.GetTouch(0);
            // Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            //Debug.Log(touch1);
        }*/
        /*if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            // Handle finger movements based on touch phase.
            switch (touch.phase)
            {
                // Record initial touch position.
                case TouchPhase.Began:
                    startPos = touch.position.y;
                    break;

                // Report that a direction has been chosen when the finger is lifted.
                case TouchPhase.Ended:
                    endPos = touch.position.y;
                    if (endPos > startPos)
                    {
                        dist = endPos - startPos;
                    } else
                    {
                         dist = startPos - endPos;
                    }
                    break;
            }
        }*/
        Touch[] myTouches = Input.touches;

                
        for (ti = 0; ti < Input.touchCount; ti++)
        {
            switch (myTouches[ti].phase)
            {
                // Record initial touch position.
                case TouchPhase.Began:
                    if (myTouches[ti].position.x > (Screen.width / 2))
                    {
                        startPosRight = myTouches[ti].position.y;
                    } else
                    {
                        startPosLeft = myTouches[ti].position.y;
                    }
                    break;

                case TouchPhase.Stationary:
                    if (myTouches[ti].position.x > (Screen.width / 2))
                    {
                        WheelColFR.brakeTorque = brakeTorque;
                        rightBreakOn = true;
                        if (a > 0)
                        {
                            a = a - 10;
                        }
                        else if (a < 0)
                        {
                            a = a + 10;
                        } else
                        {
                            a = 0;
                        }

                    }
                    else
                    {
                        WheelColFL.brakeTorque = brakeTorque;
                        leftBreakOn = true;
                        if(v > 0) {
                        v = v - 10;
                        } else if (v < 0) {
                            v = v + 10;
                        } else
                        {
                            a = 0;
                        }


                    }
                    break;

                case TouchPhase.Moved:
                    if (myTouches[ti].position.x > (Screen.width / 2))
                    {
                        WheelColFR.brakeTorque = 0;
                    }
                    else
                    {
                        WheelColFL.brakeTorque = 0;
                    }
                    break;


                case TouchPhase.Ended:
                    if (myTouches[ti].position.x > (Screen.width / 2))
                    {
                        endPosRight = myTouches[ti].position.y;
                        distRight = endPosRight - startPosRight;
                        WheelColFR.brakeTorque = 0;
                        if (distRight > 20)
                        {
                            a = a + torque;
                            avatarAnimations.anim.SetTrigger("spinWheelRight");
                        } else if(distRight < -20)
                        {
                            a = a - torque;
                        }
                        

                    } else
                    {
                        endPosLeft = myTouches[ti].position.y;
                        distLeft = endPosLeft - startPosLeft;
                        WheelColFL.brakeTorque = 0;
                        if (distLeft > 20) {
                            v = v + torque;
                            avatarAnimations.anim.SetTrigger("spinWheelLeft");

                        } else if(distLeft < -20)
                        {
                            v = v - torque;
                        }
                    }
                        //distRight = endPosRight - startPosRight;
                        //distLeft = endPosLeft - startPosLeft;
                        //a =+ distRight;
                        //v =+ distLeft;
                    break;
            }
            

        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            v = v + torque;
            avatarAnimations.anim.SetTrigger("spinWheelLeft");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            a = a + torque;
            avatarAnimations.anim.SetTrigger("spinWheelRight");
        }
        /*if (Input.GetKeyUp(KeyCode.W))
        {
            v = 0;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            a = 0;
        }*/

        if (Input.GetKeyDown(KeyCode.S))
        {
            v = v + (torque * -1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            a = a + (torque * -1);
        }
        /*if (Input.GetKeyUp(KeyCode.S))
        {
            v = 0;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            a = 0;
        }*/

        if (leftBreakOn)
        {
            //v = 0;
            leftBreakOn = false;
        }
        if (rightBreakOn)
        {
            //a = 0;
            rightBreakOn = false;
        }
        WheelColFL.motorTorque = v;
        WheelColFR.motorTorque = a;
        WheelColRL.motorTorque = v;
        WheelColRR.motorTorque = a;
        if (v > 0)
        {
            v--;
        } else if(v == 0) {
            v = 0;
        } else
        {
            v++;
        }
        if (a > 0)
        {
            a--;
        }
        else if (a == 0)
        {
            a = 0;
        }
        else
        {
            a++;
        }
    }

    void OnGUI()
    {

        guiStyle.fontSize = 50; //change the font size
        GUI.Label(new Rect(0, 0, 100, 100), "left side " + v, guiStyle);
        GUI.Label(new Rect(0, 100, 500, 800), "right side " + a, guiStyle);
        GUI.Label(new Rect(0, 200, 500, 800), "brake left " + WheelColFL.brakeTorque, guiStyle);
        GUI.Label(new Rect(0, 300, 500, 800), "brake right " + WheelColFR.brakeTorque, guiStyle);
        GUI.Label(new Rect(0, 400, 500, 800), "screen width " + Screen.width, guiStyle);
        GUI.Label(new Rect(0, 500, 500, 800), "y movement right " + a, guiStyle);
        GUI.Label(new Rect(0, 600, 500, 800), "y movement left " + v, guiStyle);

    }
}
