using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour {

    public static GameManager singleton;
    public MatchSetting matchSetting;
    public GameObject mainCamera;
    public delegate void OnPlayerKillsAnotherPlayerCallBack(string whoKilld, string whoDied);
    public OnPlayerKillsAnotherPlayerCallBack onPlayerKillsAnotherPlayerCallBack;
    public delegate void OnPlayerJoinCallBack(string username);
    public OnPlayerJoinCallBack onPlayerJoinCallBack;
    public delegate void OnPlayerLeaveRoomCallBack(string username);
    public OnPlayerLeaveRoomCallBack onPlayerLeaveRoomCallBack;

    private void Awake()
    {
        if (singleton == null) // implementation of singleton pattern, so exists only one instance of this object
            singleton = this;
    }

    public void SetCameraState(bool value)
    {
        if(mainCamera != null)
            mainCamera.SetActive(value);
    }

    #region Player registration/unregistration
    private static Dictionary<string, Player> players = new Dictionary<string, Player>(); // the list of all online player

    public static void RegisterPlayer(string playerID, Player player) // this method will be called everytime a player enter on the server
    {
        players.Add(playerID, player);
    }

    public static void UnRegisterPlayer(string playerID) // called when player leaves the server
    {
        players.Remove(playerID);
    }

    public static Player GetPlayer(string playerID)
    {
        return players[playerID];
    }

    public static Player[] GetPlayers()
    {
        return players.Values.ToArray();
    }

    //private void OnGUI()
    //{
    //    GUILayout.BeginArea(new Rect(200, 200, 200, 500));
    //    GUILayout.BeginVertical();
    //    foreach (string id in players.Keys)
    //        GUILayout.Label(id + " - " + GetPlayer(id).name);
    //    GUILayout.EndVertical();
    //    GUILayout.EndArea();
    //}


    #endregion

}
