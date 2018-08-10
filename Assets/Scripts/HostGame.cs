using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking.Match;
using System;
using UnityEngine.SceneManagement;

public class HostGame : MonoBehaviour { // this script doesn't need networkbehavior because it needs nothing by this class

    private uint maxPlayerOnRoom = 5;
    private string roomName;


    public void SetRoomName(string name)    // it is invoked by TextField, in response of the OnEndEdit event (when you click Enter)
    {
        if (name != null && !name.Equals(""))
        {
            roomName = name;
            Debug.Log("New Room's Name : " + roomName);
        }
    }

    public void CreateRoom()
    {
        if (roomName == null)
        {
            roomName = "Default Room";
        }
        if (NetworkManager.singleton.matchMaker == null)
            NetworkManager.singleton.StartMatchMaker();
        NetworkManager.singleton.matchMaker.CreateMatch(roomName, maxPlayerOnRoom, true, "", "", "", 0, 0, NetworkManager.singleton.OnMatchCreate);
    }

    public void Quit()
    {
        Debug.Log("Quitting from the application");
        Application.Quit();
    }

}
