using UnityEngine.Networking;
using UnityEngine;

public class Player : NetworkBehaviour {

    public int startHealth = 100;

    [SyncVar] // used for change this value in every clients that are connected with the server 
    private int currentHeath;

    private void Start()
    {
        currentHeath = startHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHeath = currentHeath - damage;
        Debug.Log(name + "now have " + currentHeath + "health");
    }

}
