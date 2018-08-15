using UnityEngine;
using UnityEngine.UI;

public class UserAccount : MonoBehaviour {

    public Text username;
    public Text kills;
    public Text deaths;

    private void Start()
    {
        if (UserAccountManager.singleton.isLoggedIn)
        {
            username.text = UserAccountManager.singleton.username;
            UserAccountManager.singleton.GetData(OnDataReceived);
        }
    }

    public void OnDataReceived(string data)
    {
        kills.text = "Kills: " + DataTranslator.GetInfo(data, "[KILLS]");
        deaths.text = "Deaths: " + DataTranslator.GetInfo(data, "[DEATHS]");
    }

    public void Logout()
    {
        if(UserAccountManager.singleton.isLoggedIn)
            UserAccountManager.singleton.Logout();
    }
}
