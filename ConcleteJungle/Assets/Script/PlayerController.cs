﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Vector3 pos, pos_ito, pos_camera;
    float dirY, dirX;
    float leng;
    float target_pos;
    public GameObject ceiling;
    public GameObject start_build;
    float ceiling_y;
    public GameObject itoPrafab;
    GameObject ito;
    HingeJoint joint, joint_ito;
//    public GameObject PauseScript;


	// Use this for initialization
	void Start () {
        ceiling_y = ceiling.transform.position.y;
        pos_camera = Camera.main.transform.position;
        target_pos = pos_camera.x;
	}
	
	// Update is called once per
    void Update () {
//        PauseScript pauseScript = PauseScript.GetComponent<PauseScript>();
        int ito_flag2 =PauseScript.GetItoFlag();
        if(Input.GetMouseButtonDown(0)){
            Destroy(start_build);
        }
        Debug.Log(ito_flag2);
        if (Input.GetMouseButtonDown(0) && ito_flag2 == 1){
            pos = transform.position;
            pos_ito = pos;
            dirY = ceiling_y - pos.y;
            dirX = dirY;
            leng = Mathf.Sqrt(dirX * dirX + dirY * dirY) * 0.065f;//画像差し替えたら調節
            ito = Instantiate(itoPrafab) as GameObject;
            ito.transform.Rotate(0f, 0f, -45.0f);
            ito.transform.localScale = new Vector3(0.4f, leng, 0f);
            pos_ito.x += dirX / 2.0f + 0.8f;//画像差し替えたら調節
            pos_ito.y += dirY / 2.0f + 0.8f;//画像差し替えたら調節
            ito.transform.position = pos_ito;

            joint = gameObject.AddComponent<HingeJoint>();
            Rigidbody rb = ito.GetComponent<Rigidbody>(); 
            joint.connectedBody = rb;
            joint.anchor = new Vector3(1.5f, 1.2f, 0);
            joint.axis = new Vector3(0, 0, 1);

            joint_ito = ito.gameObject.AddComponent<HingeJoint>();
            Rigidbody rb_ceil = ceiling.GetComponent<Rigidbody>();
            joint_ito.connectedBody = rb_ceil;
            joint_ito.anchor = new Vector3(0, dirY/2f+0.8f, 0);//画像差し替えたら調節
            joint_ito.axis = new Vector3(0, 0, 1);
        }

        if(Input.GetMouseButtonUp(0)){
            Destroy(ito);
            Destroy(joint);
            joint = null;
            target_pos = transform.position.x+20.0f;
        }

        if(pos_camera.x <= target_pos){
            pos_camera.x += 0.5f;
            Camera.main.transform.position = pos_camera;
        }
    }
}
