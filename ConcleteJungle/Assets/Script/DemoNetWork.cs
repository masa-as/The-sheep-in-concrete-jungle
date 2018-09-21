using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoNetWork : MonoBehaviour {
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(null);
        //PhotonNetwork.ConnectUsingSettings(null);
    }

    void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby()");
    }
	
}
