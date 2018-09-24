using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fieldController : MonoBehaviour {

    public GameObject build_a_pre;
    public GameObject build_b_pre;

    public int goal_appear;

    GameObject build;

    public GameObject goal;
    public GameObject ceiling;
    public GameObject ground;


    private Vector3 pos_building;
    private Vector3 pos_player;

    float length;
    int build_counter;
	// Use this for initialization
    void Start (){
        Time.timeScale = 1f;
        PauseScript.ito_flag=1;
        build_counter = 0;
        for (int i = 0; i < goal_appear; i++){
            int v = Random.Range(0, 2);
            if(v == 0){
                build = Instantiate(build_a_pre) as GameObject;
                pos_building = new Vector3(build_counter * 27f, 3f, 0f);//本素材にした時に調節
            }
            else if(v == 1){
                build = Instantiate(build_b_pre) as GameObject;
                pos_building = new Vector3(build_counter * 27f, 5f, 0f);//本素材にした時に調節
            }
            build.transform.position = pos_building;
            build_counter++;
        }
        pos_building = new Vector3(build_counter * 27f, -5f, 0f);
        goal.transform.position = pos_building;
        length = pos_building.x;
        ceiling.transform.localScale = new Vector3(length+50f, 1f, 20.0f);
        ceiling.transform.position = new Vector3((length + 50f) / 2f, 20f, 0f);

        ground.transform.localScale = new Vector3(length + 50f, 1f, 10.0f);
        ground.transform.position = new Vector3((length + 50f) / 2f, -10f, 0f);

	}
	
}
