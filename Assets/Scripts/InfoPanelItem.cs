using UnityEngine.UI;
using UnityEngine;

public class InfoPanelItem : MonoBehaviour {

	public void SetKillText(string whoKilled, string whoDied)
    {
        GetComponent<Text>().text = whoKilled + " killed " + whoDied;
    }

    public void SetJoinPlayer(string username)
    {
        GetComponent<Text>().text = username + " joined";
    }

    public void SetPlayerLeft(string username)
    {
        GetComponent<Text>().text = username + " left";
    }

}
