using UnityEngine.UI;
using UnityEngine;

public class PlayerScoreboardItem : MonoBehaviour {

    public Text usernameText;
    public Text killsText;
    public Text deathsText;

    public void Setup(string username, int kills, int deaths)
    {
        usernameText.text = username;
        killsText.text = "KILLS: " + kills;
        deathsText.text = "DEATHS: " + deaths;
    }

}
