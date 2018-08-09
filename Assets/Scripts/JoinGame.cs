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
        RefreshRoomList();
    }

    public void RefreshRoomList()
    {
        if (NetworkManager.singleton.matchMaker == null)
            NetworkManager.singleton.StartMatchMaker();
        NetworkManager.singleton.matchMaker.ListMatches(0, 20, "", false, 0, 0, OnMatchList);
    }

    // this callback function has 3 argument, and the third is a list of matches there are and their info
    private void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> responseData)
    {
        if (!success || responseData == null) // if this operation gives nevative result, it does nothing
        {
            Debug.Log("Error in OnMatchList method - success variable is false or responseData is null");
            return;
        }
        ClearRoomList();
        foreach(MatchInfoSnapshot info in responseData) // update the button with info on the matches
        {
            GameObject go = Instantiate(matchInfoPrefab, roomListContent.position, Quaternion.identity);
            go.transform.SetParent(roomListContent);
            JoinRoomButton jrb = go.GetComponent<JoinRoomButton>();
            if (jrb == null)
                Debug.LogError("jrb is null");
            else
            {
                jrb.SetMatch(info);
                roomList.Add(go);
            }
        }
    }

    private void ClearRoomList() // before it remove the buttons that rappresents the room in game, and than it clears the list of rooms
    {
        foreach (GameObject go in roomList)
        {
            Destroy(go);
            roomList.Remove(go);
        }
    }

}
