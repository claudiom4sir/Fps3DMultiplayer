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
        string[] info = DataTranslator.ForGetting(data); // this method translate the data from the database to wanted format for showing data
        kills.text = info[0];
        deaths.text = info[1];
    }

    public void Logout()
    {
        if(UserAccountManager.singleton.isLoggedIn)
            UserAccountManager.singleton.Logout();
    }
}
