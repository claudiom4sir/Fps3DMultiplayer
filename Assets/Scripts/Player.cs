using UnityEngine.Networking;
using UnityEngine;

public class Player : NetworkBehaviour {

    public int startHealth = 100;

    [SyncVar]
    private bool isDead = false;
    [SyncVar] // used for change this value in every clients that are connected with the server 
    private int currentHeath;
    public Behaviour[] disableOnDeath;
    private bool[] wasEnabled;

    public void Setup()
    {
        wasEnabled = new bool[disableOnDeath.Length];
        for (int i = 0; i < disableOnDeath.Length; i++) // used for store the value
            wasEnabled[i] = disableOnDeath[i].enabled;
        currentHeath = startHealth;
        isDead = false;
        for (int i = 0; i < disableOnDeath.Length; i++) // used for restore the value when player respawns
            disableOnDeath[i].enabled = wasEnabled[i];
        Collider collider = GetComponent<Collider>(); // these 3 lines exist because Collider not inherits from Behavior, but Behavior and Collider inherit from Component
        if (collider != null)
            collider.enabled = true;
    }

    public bool IsDead()
    {
        return isDead;
    }

    protected void SetIsDead(bool value)
    {
        isDead = value;
    }

    [ClientRpc]
    public void RpcTakeDamage(int damage)
    {
        if (isDead)
            return;
        currentHeath = currentHeath - damage;
        if (currentHeath <= 0)
            Die();
        Debug.Log(name + "now have " + currentHeath + "health");
    }

    private void Die()
    {
        isDead = true;
        for (int i = 0; i < disableOnDeath.Length; i++)
            disableOnDeath[i].enabled = false;
        Collider collider = GetComponent<Collider>();
        if (collider != null)
            collider.enabled = false;
    }

}
