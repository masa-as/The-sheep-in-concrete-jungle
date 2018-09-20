using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fieldController : MonoBehaviour {

    public GameObject build_a_pre;
    public GameObject build_b_pre;
    public GameObject build_c_pre;

    GameObject[] build;

    public GameObject ceiling;
    public GameObject ground;

    private Vector3 pos_ceiling;
    private Vector3 pos_ground;
    private Vector3[] pos_building;

    int build_counter = 0;
	// Use this for initialization
	void Start () {
        pos_ceiling = ceiling.transform.position;
        pos_ground = ground.transform.position;
        build = new GameObject[10];
        pos_building = new Vector3[10];
        for (int i = 0; i < 10; i++){
            int v = Random.Range(0, 3);
            if(v == 0){
                build[i] = Instantiate(build_a_pre) as GameObject;
                float build_scale = Random.Range(0.7f, 1.2f);//本素材にした時調節
                build[i].transform.localScale = new Vector3(build_scale, build_scale, 0f);
                pos_building[i] = new Vector3(build_counter * 5.0f, 0f, 0f);//本素材にした時に調節
            }
            else if(v == 1){
                build[i] = Instantiate(build_b_pre) as GameObject;
                float build_scale = Random.Range(0.7f, 1.2f);//本素材にした時調節
                build[i].transform.localScale = new Vector3(build_scale, build_scale, 0f);
                pos_building[i] = new Vector3(build_counter * 5.0f, -1.0f, 0f);//本素材にした時に調節
            }
            else if(v == 2){
                build[i] = Instantiate(build_c_pre) as GameObject;
                float build_scale = Random.Range(0.7f, 1.2f);//本素材にした時調節
                build[i].transform.localScale = new Vector3(build_scale, build_scale, 0f);
                pos_building[i] = new Vector3(build_counter * 5.0f, 4.0f, 0f);//本素材にした時に調節
            }
            build[i].transform.position = pos_building[i];
            build_counter++;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
