using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judge : MonoBehaviour {
    public bool judge;

    private void Start()
    {
        judge = false;
    }
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player"){
            judge = true;
        }
    }
}
