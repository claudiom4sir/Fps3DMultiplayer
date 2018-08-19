
using UnityEngine;

public class ScoreboardUI : MonoBehaviour {

    private Player[] players;
    public GameObject playerScoreboardItem;
    public GameObject scorePanelParent;

    private void OnEnable()
    {
        players = GameManager.GetPlayers();
        foreach(Player p in players)
        {
            GameObject scoreboardItem = Instantiate(playerScoreboardItem, scorePanelParent.transform);
            scoreboardItem.GetComponent<PlayerScoreboardItem>().Setup(p.username, p.kills, p.deaths);
        }
    }

    private void OnDisable()
    {
        foreach (Transform child in scorePanelParent.transform)
            Destroy(child.gameObject);
    }

}
