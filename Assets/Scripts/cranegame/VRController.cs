﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Platform;
using UnityEngine.UI;

public class VRController : MonoBehaviour {
    public static VRController instance;
   public OVRInput.Controller controller;
    public Text quicktext;
    Vector2 touchpoint;
    bool TriggerPush = false;
    bool TouchPush =false;
    void Awake()
    {
        instance = this;
        
    }  
 
    void Update()
    {
       
        OVRInput.Update();
        controller = OVRInput.GetActiveController();
        if (OVRInput.IsControllerConnected(controller))
        {
            touchpoint = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad, controller);

            TriggerPush = OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger);
            TouchPush = OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad);
        }

        if (TriggerPush)
            quicktext.text = "b: " + TriggerPush;
        else
            quicktext.text = "no";
        TriggerPull();
        TouchPress();
    }

    public void CraneMovement(Rigidbody rbody, float speed,Transform crane)
    {
        rbody.velocity = -crane.forward * touchpoint.y * (speed * Time.deltaTime) + -crane.right * touchpoint.x * (speed * Time.deltaTime);

    }

    void TriggerPull()
    {
        if(TriggerPush)
        {
            GrabberControl.instance.LetGoFull();

        }
    }

    void TouchPress()
    {
        if (TouchPush)
        {
            GrabberControl.instance.VRCraneLocation(TouchPush);
           
        }
    }
}
