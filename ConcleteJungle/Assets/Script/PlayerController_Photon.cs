using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController_Photon : MonoBehaviour {

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
    public float wool_count;
    public int p; //woolの出現確率
    PhotonView myPhotonView;
    Slider _slider;
    GameObject female;
    string sceneName;
    bool achieve, swing, jump;


	// Use this for initialization
	void Start () {
        achieve = false;
        swing = false;
        jump = false;
        this.myPhotonView = GetComponent<PhotonView>();
        _slider = GameObject.Find("WoolBar").GetComponent<Slider>();
        ceiling = GameObject.Find("ceiling");
        female = GameObject.Find("female");
        ceiling_pos = ceiling.transform.position;
        pos_camera = Camera.main.transform.position;
        target_pos = pos_camera.x;
        rb_player = GetComponent<Rigidbody>();
        rb_player.AddForce(30, 0, 0, ForceMode.Impulse);
	}
	
	// Update is called once per
    void Update () {
        int ito_flag2 =PauseScript.GetItoFlag();
        if (this.myPhotonView.isMine)
        {
            if (Input.GetMouseButtonDown(0) && ito_flag2 == 1){
                swing = true;
            }
            if(Input.GetMouseButtonUp(0) && ito_flag2 == 1){
                jump = true;
                if (Random.Range(0, 100) < p){
                    wool = Instantiate(woolPrefab) as GameObject;
                    wool.transform.position = new Vector3(transform.position.x + 30f, Random.Range(5.0f, 10.0f), 0f);
                }

            }
        }
    }

    private void FixedUpdate()
    {
        if(swing){
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
                swing = false;
            }
        }
        if(jump){
            Destroy(ito);
            Destroy(joint);
            joint = null;
            jump = false;
        }
        pos_camera.x = transform.position.x+20;
        Camera.main.transform.position = pos_camera;
        _slider.value = wool_count / 100;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (this.myPhotonView.isMine)
        {
            if (collision.gameObject.name == "ground")
            {
                Invoke("restart", 1.0f);
            }
        }

        if (collision.gameObject.name == "Goal")
        {
            if(this.myPhotonView.isMine){
                pos = transform.position;
                female.transform.position = new Vector3(pos.x + 10, 0.5f, 0);
                sceneName = "Win";
                waitChangeScene(1.2f);
            }
            else{
                pos = transform.position;
                female.transform.position = new Vector3(pos.x + 10, 0.5f, 0);
                sceneName = "Lose";
                waitChangeScene(0.2f);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.myPhotonView.isMine){
            if (other.gameObject.tag == "wool")
            {
                wool_count += 20f;
                Destroy(other.gameObject);
            }
        }

    }

    private void waitChangeScene(float time)
    {
        Invoke("changeScene", time);
    }

    private void changeScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    private void restart(){
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(ito);
            Destroy(joint);
            joint = null;
        }
        wool_count = 50;
        pos.y = 15f;
        transform.position = pos;
    }
}
