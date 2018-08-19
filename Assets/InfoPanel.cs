using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanel : MonoBehaviour {

    public GameObject infoPanelItem;
    public GameObject infoPanelParent;
    public float destroyTime = 5f;

    private void Start()
    {
        GameManager.singleton.onPlayerKillsAnotherPlayerCallBack += KillPlayer;
        GameManager.singleton.onPlayerJoinCallBack += JoinPlayer;
        GameManager.singleton.onPlayerJoinCallBack(UserAccountManager.singleton.username);
    }

    public void KillPlayer(string whoKilled, string whoDied)
    {
        GameObject go = Instantiate(infoPanelItem, infoPanelParent.transform);
        go.GetComponent<InfoPanelItem>().SetKillText(whoKilled, whoDied);
        Destroy(go, destroyTime);
    }

    public void JoinPlayer(string username)
    {
        GameObject go = Instantiate(infoPanelItem, infoPanelParent.transform);
        go.GetComponent<InfoPanelItem>().SetJoinPlayer(username);
        Destroy(go, destroyTime);
    }
}
