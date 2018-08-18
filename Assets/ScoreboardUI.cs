
using UnityEngine;

public class ScoreboardUI : MonoBehaviour {

    private Player[] players;
    public GameObject playerScoreboardItem;
    public GameObject scorePanel;

    private void OnEnable()
    {
        players = GameManager.GetPlayers();
        foreach(Player p in players)
        {
            Debug.Log(p.username + " " + p.kills + " " + p.deaths);
        }
    }

}
