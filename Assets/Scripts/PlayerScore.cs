using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerScore : MonoBehaviour {

    private Player player;
    public float syncTime = 10f;

    private void Start() // only local player can use this script, because if it's not, PlayerSetup will disable it
    {
        player = GetComponent<Player>();
        SyncScore();
        StartCoroutine(SendStatsData());
    }

    private IEnumerator SendStatsData() // used for send data to the server every syncTime seconds
    {
        while (true)
        {
            yield return new WaitForSeconds(syncTime);
            SendStatsDataNow();
        }
    }

    public void SendStatsDataNow() // used if you want to send the data right now
    {
        string data = DataTranslator.SetData(player.kills, player.deaths);
        Debug.Log("Data sent are : " + data);
        UserAccountManager.singleton.SetData(data);
    }

    private void OnDestroy() // if gameobject will be destroy, before this it send his data
    {
        if(player != null)
        SendStatsDataNow();
    }

    private void SyncScore()
    {
        if (UserAccountManager.singleton.isLoggedIn)
        {
            UserAccountManager.singleton.GetData(OnDataReceived);
        }
    }

    private void OnDataReceived(string data)
    {
        player.kills = DataTranslator.GetInfo(data, "[KILLS]");
        player.deaths = DataTranslator.GetInfo(data, "[DEATHS]");
    }
}
