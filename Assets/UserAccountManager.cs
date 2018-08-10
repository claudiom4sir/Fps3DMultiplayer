using DatabaseControl;
using UnityEngine;

public class UserAccountManager : MonoBehaviour {

    public static UserAccountManager singleton;
    public static string username = "";
    private static string password = "";
    public static bool isLoggedIn = false;

    private void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(singleton);
        }
        else
            Destroy(gameObject); // Awake will be called every time we turn on LoginScene
    }

    public string LogIn(string user, string pass)
    {
        username = user;
        password = pass;
        isLoggedIn = true;
    }

    public void Logout()
    {
        username = "";
        password = "";
        isLoggedIn = false;
    }
}
