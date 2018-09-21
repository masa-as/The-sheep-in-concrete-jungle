using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    Vector3 pos, pos_ito, pos_camera;
    float dirY, dirX;
    float leng;
    float target_pos;
    public GameObject ceiling;
    Vector3 ceiling_pos;
    public GameObject itoPrafab;
    GameObject ito;
    HingeJoint joint, joint_ito;
    Rigidbody rb_player;
    public float wool;


	// Use this for initialization
	void Start () {
        wool = 100f;
        ceiling_pos = ceiling.transform.position;
        pos_camera = Camera.main.transform.position;
        target_pos = pos_camera.x;
        rb_player = GetComponent<Rigidbody>();
        rb_player.AddForce(30, 0, 0, ForceMode.Impulse);
        ceiling_pos.x = transform.position.x;
        ceiling.transform.position = ceiling_pos;
	}
	
	// Update is called once per
    void Update () {
        int ito_flag2 =PauseScript.GetItoFlag();
        if (Input.GetMouseButtonDown(0) && ito_flag2 == 1){
            pos = transform.position;
            pos_ito = pos;
            dirY = ceiling_pos.y - pos.y;
            dirX = dirY;
            leng = Mathf.Sqrt(dirX * dirX + dirY * dirY) * 0.065f;//画像差し替えたら調節
            if((wool - leng*20f) > 0){
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
                joint_ito.anchor = new Vector3(0, dirY / 2f + 0.8f, 0);//画像差し替えたら調節
                joint_ito.axis = new Vector3(0, 0, 1);
                target_pos = transform.position.x + 20.0f;
                //糸の長さによる
                rb_player.AddForce(leng * 20, -leng * 20, 0, ForceMode.Impulse);
                wool -= leng * 20;
            }
        }

        if(Input.GetMouseButtonUp(0)){
            Destroy(ito);
            Destroy(joint);
            joint = null;
            ceiling_pos.x = transform.position.x;
            ceiling.transform.position = ceiling_pos;
            target_pos = transform.position.x + 20.0f;
        }

        if(pos_camera.x <= target_pos){
            pos_camera.x += 2.5f;
            Camera.main.transform.position = pos_camera;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "ground")
        {
            SceneManager.LoadScene("result");
        }
        if (collision.gameObject.name == "Goal")
        {
            SceneManager.LoadScene("result");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "wool")
        {
            wool += 20f;
            Destroy(other.gameObject);
        }
    }

}
