using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{

    public GameObject StartButton;
    public GameObject StartButton_push;
    public AudioClip audioClip1;
    private AudioSource audioSource;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartButtonDown()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = audioClip1;
        audioSource.Play();
//        GetComponent<AudioSource>().Play();

        StartButton.SetActive(!StartButton.activeSelf);
        StartButton_push.SetActive(!StartButton_push.activeSelf);
        SceneManager.LoadScene("Select");
    }

    public void Sound(){

    }

}
