using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.Networking;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]
public class JoinRoomButton : MonoBehaviour {

    private MatchInfoSnapshot matchInfo;
    private Text text;

    public void SetMatch(MatchInfoSnapshot match)
    {
        matchInfo = match;
        SetText();
    }

    private void SetText()  // used for setting the text of the button
    {
        text = GetComponentInChildren<Text>();
        if (text == null)
            Debug.LogError("The text of the button for joining game is null");
        text.text = matchInfo.name + " (" + matchInfo.currentSize + "/" + matchInfo.maxSize + ")";
    }

    public void JoinRoom()
    {
        NetworkManager.singleton.matchMaker.JoinMatch(matchInfo.networkId, "", "", "", 0, 0, NetworkManager.singleton.OnMatchJoined);
        text.text = "Joining";
    }
}