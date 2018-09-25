﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    Vector3 pos, pos_ito, pos_camera;
    float dirY, dirX;
    float leng;
    float target_pos;
    public GameObject ceiling;
    public GameObject woolPrefab;
    GameObject wool;
    Vector3 ceiling_pos;
    public GameObject itoPrafab;
    GameObject ito;
    HingeJoint joint, joint_ito;
    Rigidbody rb_player;
    GameObject female;

    public float wool_count;
    public int p; //woolの出現確率
    Slider _slider;
    // Use this for initialization
    void Start () {
        female = GameObject.Find("female");
        ceiling = GameObject.Find("ceiling");
        ceiling_pos = ceiling.transform.position;
        pos_camera = Camera.main.transform.position;
        target_pos = pos_camera.x;
        rb_player = GetComponent<Rigidbody>();
        rb_player.AddForce(30, 0, 0, ForceMode.Impulse);
        // スライダーを取得する
        _slider = GameObject.Find("WoolBar").GetComponent<Slider>();

    }

    // Update is called once per
    void Update () {
        int ito_flag2 =PauseScript.GetItoFlag();
        if (Input.GetMouseButtonDown(0) && ito_flag2 == 1)
        {
            pos = transform.position;
            pos_ito = pos;
            dirY = ceiling_pos.y - pos.y;
            dirX = dirY;
            leng = Mathf.Sqrt(dirX * dirX + dirY * dirY) * 0.095f;//画像差し替えたら調節
            if ((wool_count - leng * 20f) > 0)
            {
                ito = Instantiate(itoPrafab) as GameObject;
                ito.transform.Rotate(0f, 0f, -45.0f);
                ito.transform.localScale = new Vector3(0.4f, leng, 0f);
                pos_ito.x += dirX / 2.0f + 1.4f;//画像差し替えたら調節
                pos_ito.y += dirY / 2.0f + 1.4f;//画像差し替えたら調節
                ito.transform.position = pos_ito;

                joint = gameObject.AddComponent<HingeJoint>();
                Rigidbody rb = ito.GetComponent<Rigidbody>();
                joint.connectedBody = rb;
                joint.anchor = new Vector3(0.2f, 0.0f, 0);
                joint.axis = new Vector3(0, 0, 1);

                joint_ito = ito.gameObject.AddComponent<HingeJoint>();
                Rigidbody rb_ceil = ceiling.GetComponent<Rigidbody>();
                joint_ito.connectedBody = rb_ceil;
                joint_ito.anchor = new Vector3(0, dirY / 2f + 0.6f, 0);//画像差し替えたら調節
                joint_ito.axis = new Vector3(0, 0, 1);

                //糸の長さによる
                rb_player.AddForce(leng * 10, -leng * 10, 0, ForceMode.Impulse);
                wool_count -= leng * 7;
            }
        }
        if (Input.GetMouseButtonUp(0) && ito_flag2 == 1)
        {
            Destroy(ito);
            Destroy(joint);
            joint = null;
            if (Random.Range(0, 100) < p)
            {
                wool = Instantiate(woolPrefab) as GameObject;
                wool.transform.position = new Vector3(transform.position.x + 30f, Random.Range(5.0f, 10.0f), 0f);
            } 
        }
        pos_camera.x = transform.position.x;
        Camera.main.transform.position = pos_camera;

        //ウールバー長さ更新
        Debug.Log(wool);
        _slider.value = wool_count/100;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "ground" || collision.gameObject.name == "wolf")
        {
            SceneManager.LoadScene("result");
        }
        if (collision.gameObject.name == "Goal")
        {
            pos = transform.position;
            female.transform.position = new Vector3(pos.x+10, 0.5f, 0);

            //SceneManager.LoadScene("result");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "wool")
        {
            wool_count += 20f;
            Destroy(other.gameObject);
        }
    }

}
