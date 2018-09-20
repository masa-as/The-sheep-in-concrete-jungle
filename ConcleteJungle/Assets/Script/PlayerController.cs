using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Vector3 pos, pos_ito;
    float dirY, dirX;
    float leng;
    public GameObject ceiling;
    public GameObject itoPrafab;
    GameObject ito;
    HingeJoint joint, joint_ito;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0)){
            pos = transform.position;
            pos_ito = pos;
            dirY = 10.0f - pos.y;
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
        }

	}
}
