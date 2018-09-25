using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wolfController : MonoBehaviour {

    private Vector3 pos;
    private int sign;
    private float dx;
	// Use this for initialization
	void Start () {
        dx = 20f;
        sign = 1;
	}
	
	// Update is called once per frame
	void Update () {
        pos = transform.position;
        pos.x = Camera.main.transform.position.x + 10 * sign - dx * sign;
        transform.localScale = new Vector3(sign, 1, 1);
        transform.position = pos;
        dx -= 0.1f;
        if(dx <= 0){
            sign *= -1;
            dx = 20f;
        }
	}
}
