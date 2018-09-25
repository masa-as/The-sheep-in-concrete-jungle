using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class femaleController : MonoBehaviour {

    public string sceneName;
    Animator animator;
	// Use this for initialization
	void Start () {
        this.animator = GetComponent<Animator>();
        animator.Play("female");
	}
	
	// Update is called once per frame
    public void Win(string sceneName){
        this.sceneName = sceneName;
        animator.Play("female_Jump");
        Invoke("changeScene", 1.2f);
    }

    void changeScene(){
        SceneManager.LoadScene(sceneName);
    }
}
