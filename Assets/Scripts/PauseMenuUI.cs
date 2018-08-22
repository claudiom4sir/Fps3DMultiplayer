using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.SceneManagement;

public class PauseMenuUI : NetworkBehaviour
{

    public static bool isActive = false; // if this variable is true, player can't do movement and shoot

    public void LeaveRoom()
    {
        MatchInfo matchInfo = NetworkManager.singleton.matchInfo;
        NetworkManager.singleton.matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, 0, NetworkManager.singleton.OnDropConnection);
        NetworkManager.singleton.StopHost();
        PauseMenuUI.isActive = false;
        SceneManager.LoadScene("LobbyScene");
    }

}
