using UnityEngine;

public class ExitButton : MonoBehaviour {

    public void Quit()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
	
}
