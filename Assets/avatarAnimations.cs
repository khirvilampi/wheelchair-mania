using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class avatarAnimations : MonoBehaviour {

    public Animator anim;

    public bool move;

    public int idle = Animator.StringToHash("Base Layer.metarig|Action");
    public int left = Animator.StringToHash("LeftArmFor");

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        
    }
	
	// Update is called once per frame
	void Update () {
        //AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (Input.GetKeyDown(KeyCode.W))
        {
            move = true;
            anim.SetTrigger("spinWheel");
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            move = false;
        }

    }
}
