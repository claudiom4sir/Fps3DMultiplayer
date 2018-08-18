
using UnityEngine;

public class ScoreboardUI : MonoBehaviour {

    private Player[] players;
    GameObject playerScoreboardItem;
    //GameObject

    private void OnEnable()
    {
        players = GameManager.GetPlayers();

    }

}
