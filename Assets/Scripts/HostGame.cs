using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking.Match;
using System;

public class HostGame : MonoBehaviour { // this script doesn't need networkbehavior because it needs nothing by this class

    private uint maxPlayerOnRoom = 5;
    private string roomName;

    public void SetRoomName(string name)
    {
        if (name != null && !name.Equals(""))
        {
            roomName = name;
            Debug.Log("New Room's Name : " + roomName);
        }
        else
            Debug.Log("name is null or empty");
    }

    public void CreateRoom()
    {
        Debug.Log("create room called");
        if (roomName == null)
            roomName = "Default Room";
        if (NetworkManager.singleton.matchMaker == null)
            NetworkManager.singleton.StartMatchMaker();
        NetworkManager.singleton.matchMaker.CreateMatch(roomName, maxPlayerOnRoom, true, "", "", "", 0, 0, NetworkManager.singleton.OnMatchCreate);
    }

}
