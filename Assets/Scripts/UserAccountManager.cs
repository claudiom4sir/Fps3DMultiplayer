using DatabaseControl;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UserAccountManager : MonoBehaviour {

    public static UserAccountManager singleton;
    public string username = "";
    private string password = "";
    public bool isLoggedIn = false;
    public string loginScene = "LobbyScene";
    public string logoutScene = "LoginScene";
    public delegate void OnDataReceivedCallBack(string data);

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

    public void LogIn(string user, string pass)
    {
        username = user;
        password = pass;
        isLoggedIn = true;
        SceneManager.LoadScene(loginScene);
    }

    public void Logout()
    {
        username = "";
        password = "";
        isLoggedIn = false;
        SceneManager.LoadScene(logoutScene);
    }

    public void SetData(string data)
    {
        if (isLoggedIn)
            StartCoroutine(SetDataI(data));
        else
            Debug.LogError("You are not logged");
    }

    public void GetData(OnDataReceivedCallBack callback)
    {
        if(isLoggedIn)
            StartCoroutine(GetDataI(callback));
    }

    private IEnumerator SetDataI(string data)
    {
        IEnumerator e = DCF.SetUserData(username, password, data); // << Send request to set the player's data string. Provides the username, password and new data string
        while (e.MoveNext())
        {
            yield return e.Current;
        }
        string response = e.Current as string; // << The returned string from the request

        if (response == "Success")
            Debug.Log("Operation done");
        else
            Debug.Log("SetData - Operation denied");
    }

    private IEnumerator GetDataI(OnDataReceivedCallBack callBack)
    {
        IEnumerator e = DCF.GetUserData(username, password); // << Send request to get the player's data string. Provides the username and password
        while (e.MoveNext())
        {
            yield return e.Current;
        }
        string response = e.Current as string; // << The returned string from the request

        if (response == "Error")
            Debug.Log("GetData - Operation denied");
        else
            callBack.Invoke(response);
    }
}
