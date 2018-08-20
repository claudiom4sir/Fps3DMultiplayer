using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanel : MonoBehaviour {

    public GameObject infoPanelItem;
    public GameObject infoPanelParent;
    public float destroyTime = 5f;

    // this method register InfoPanel methods in some delegate methods in GameManager
    // when this delegate methods are called in GameManager, the specific method of this object will be invoked
    private void Start()
    {
        GameManager.singleton.onPlayerKillsAnotherPlayerCallBack += KillPlayerInfo;
        GameManager.singleton.onPlayerJoinCallBack += JoinPlayerInfo;
        GameManager.singleton.onPlayerLeaveRoomCallBack += playerLeftInfo;
    }

    public void KillPlayerInfo(string whoKilled, string whoDied)
    {
        GameObject go = Instantiate(infoPanelItem, infoPanelParent.transform);
        go.GetComponent<InfoPanelItem>().SetKillText(whoKilled, whoDied);
        Destroy(go, destroyTime);
    }

    public void JoinPlayerInfo(string username)
    {
        GameObject go = Instantiate(infoPanelItem, infoPanelParent.transform);
        go.GetComponent<InfoPanelItem>().SetJoinPlayer(username);
        Destroy(go, destroyTime);
    }

    public void playerLeftInfo(string username)
    {
        GameObject go = Instantiate(infoPanelItem, infoPanelParent.transform);
        go.GetComponent<InfoPanelItem>().SetPlayerLeft(username);
        Destroy(go, destroyTime);
    }

}
