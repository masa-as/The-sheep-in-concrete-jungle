using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fieldController : MonoBehaviour {

    public GameObject build_a_pre;
    public GameObject build_b_pre;
    public GameObject build_c_pre;

    public int goal_appear;

    GameObject[] build;

    public GameObject ground;
    public GameObject player;
    public GameObject goal;

    private Vector3 pos_ground;
    private Vector3[] pos_building;
    private Vector3 pos_player;

    int build_counter;
    bool one_goal = true;
	// Use this for initialization
	void Start () {
        Time.timeScale = 1f;
        PauseScript.ito_flag=1;
        build_counter = 0;
        pos_ground = ground.transform.position;
        pos_player = player.transform.position;
        build = new GameObject[10];
        pos_building = new Vector3[10];
        for (int i = 0; i < 10; i++){
            int v = Random.Range(0, 3);
            if(v == 0){
                build[i] = Instantiate(build_a_pre) as GameObject;
                float build_scale = Random.Range(0.7f, 1.2f);//本素材にした時調節
                build[i].transform.localScale = new Vector3(build_scale, build_scale, 0f);
                pos_building[i] = new Vector3(build_counter * 7.5f, 0f+(build_scale-1)*5f, 0f);//本素材にした時に調節
            }
            else if(v == 1){
                build[i] = Instantiate(build_b_pre) as GameObject;
                float build_scale = Random.Range(0.7f, 1.2f);//本素材にした時調節
                build[i].transform.localScale = new Vector3(build_scale, build_scale, 0f);
                pos_building[i] = new Vector3(build_counter * 7.5f, -1.0f+(build_scale-1) * 5f, 0f);//本素材にした時に調節
            }
            else if(v == 2){
                build[i] = Instantiate(build_c_pre) as GameObject;
                float build_scale = Random.Range(0.7f, 1.2f);//本素材にした時調節
                build[i].transform.localScale = new Vector3(build_scale, build_scale, 0f);
                pos_building[i] = new Vector3(build_counter * 7.5f, 4.5f+(build_scale-1) *3.5f, 0f);//本素材にした時に調節
            }
            build[i].transform.position = pos_building[i];
            build_counter++;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonUp(0) && one_goal){
            pos_player = player.transform.position;
            while (pos_building[build_counter % 10].x < pos_player.x - 10f)
            {
                Destroy(build[build_counter % 10]);
                int v = Random.Range(0, 3);
                if (v == 0)
                {
                    build[build_counter % 10] = Instantiate(build_a_pre) as GameObject;
                    float build_scale = Random.Range(0.7f, 1.2f);//本素材にした時調節
                    build[build_counter % 10].transform.localScale = new Vector3(build_scale, build_scale, 0f);
                    pos_building[build_counter % 10] = new Vector3(build_counter * 7.5f, 0f + (build_scale - 1) * 5f, 0f);//本素材にした時に調節
                }
                else if (v == 1)
                {
                    build[build_counter % 10] = Instantiate(build_b_pre) as GameObject;
                    float build_scale = Random.Range(0.7f, 1.2f);//本素材にした時調節
                    build[build_counter % 10].transform.localScale = new Vector3(build_scale, build_scale, 0f);
                    pos_building[build_counter % 10] = new Vector3(build_counter * 7.5f, -1.0f + (build_scale - 1) * 5f, 0f);//本素材にした時に調節
                }
                else if (v == 2)
                {
                    build[build_counter % 10] = Instantiate(build_c_pre) as GameObject;
                    float build_scale = Random.Range(0.7f, 1.2f);//本素材にした時調節
                    build[build_counter % 10].transform.localScale = new Vector3(build_scale, build_scale, 0f);
                    pos_building[build_counter % 10] = new Vector3(build_counter * 7.5f, 4.5f + (build_scale - 1) * 3.5f, 0f);//本素材にした時に調節
                }
                build[build_counter % 10].transform.position = pos_building[build_counter % 10];
                build_counter++;
            }
            pos_ground.x = pos_player.x;
            ground.transform.position = pos_ground;
        }
        if(build_counter >= goal_appear && one_goal){
            goal.transform.position = new Vector3(build_counter * 7.5f, 0f, 0f);
            for (int i = 0; i < 10; i++){
                Destroy(build[i]);
            }
            one_goal = false;
        }
	}
}
