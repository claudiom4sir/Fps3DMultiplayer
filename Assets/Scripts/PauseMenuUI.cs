using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.SceneManagement;

public class PauseMenuUI : NetworkBehaviour {

    public static bool isActive = false; // if this variable is true, player can't do movement and shoot

    public void LeaveRoom()
    {
        if(UserAccountManager.singleton.isLoggedIn)
            CmdNotifyPlayerLeaveRoom(UserAccountManager.singleton.username);
        MatchInfo matchInfo = NetworkManager.singleton.matchInfo;
        NetworkManager.singleton.matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, 0, NetworkManager.singleton.OnDropConnection);
        NetworkManager.singleton.StopHost();
        PauseMenuUI.isActive = false;
        SceneManager.LoadScene("LobbyScene");
    }

    [Command]
    private void CmdNotifyPlayerLeaveRoom(string username) // it is used for notify all other players that this player left room
    {
        RpcNotifyPlayerLeaveRoom(username);
    }

    [ClientRpc]
    private void RpcNotifyPlayerLeaveRoom(string username) // all players are able to show on their playerUI that this player left room
    {
        GameManager.singleton.onPlayerLeaveRoomCallBack(username);
    }

}
