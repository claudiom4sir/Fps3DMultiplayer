using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class PauseMenuUI : MonoBehaviour {

    public static bool isActive = false;

    public void LeaveRoom()
    {
        MatchInfo matchInfo = NetworkManager.singleton.matchInfo;
        NetworkManager.singleton.matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, 0, NetworkManager.singleton.OnDropConnection);
        NetworkManager.singleton.StopHost();
        PauseMenuUI.isActive = false;
    }

	
}
