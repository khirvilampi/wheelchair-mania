﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class avatarAnimations : MonoBehaviour {

    public static Animator anim;

   // public int idle = Animator.StringToHash("Base Layer.metarig|Action");
   // public int left = Animator.StringToHash("LeftArmFor");

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        
    }
	
	// Update is called once per frame
	void Update () {

    }
}
