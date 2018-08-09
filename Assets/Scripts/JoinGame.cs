using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Networking.Match;
using System;

public class JoinGame : MonoBehaviour {

    private List<GameObject> roomList = new List<GameObject>();
    public Transform roomListContent;
    public GameObject matchInfoPrefab;

    private void Start()
    {
        if (NetworkManager.singleton.matchMaker == null)
            NetworkManager.singleton.StartMatchMaker();
        RefreshRoomList();
    }

    public void RefreshRoomList()
    {
       NetworkManager.singleton.matchMaker.ListMatches(0, 20, "", false, 0, 0, OnMatchList);
    }

    // this callback function has 3 argument, and the third is a list of matches there are and their info
    private void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> responseData)
    {
        if (!success) // if this operation gives nevative result, it does nothing
        {
            Debug.Log("Error in OnMatchList method - success variable is false");
            return;
        }
        ClearRoomList();
        foreach(MatchInfoSnapshot info in responseData) // update the button with info on the matches
        {
            GameObject go = Instantiate(matchInfoPrefab, transform.position, Quaternion.identity);
            go.transform.SetParent(roomListContent);
            Text t = go.GetComponentInChildren<Text>();
            if(t != null)
                t.text = info.name + " (" + info.currentSize + "/" + info.maxSize + ")";
            roomList.Add(go);
        }
    }

    private void ClearRoomList() // before it remove the buttons that rappresents the room in game, and than it clears the list of rooms
    {
        foreach (GameObject go in roomList)
            Destroy(go);
        roomList.Clear();
    }

}
