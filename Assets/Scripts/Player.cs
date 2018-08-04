using UnityEngine.Networking;
using UnityEngine;
using System.Collections;

public class Player : NetworkBehaviour {

    public int startHealth = 100;
    public Behaviour[] disableOnDeath;

    [SyncVar] // used for change this value in every clients that are connected with the server 
    private int currentHeath;
    [SyncVar]
    private bool isDead = false;
    private bool[] wasEnabled;

    public void Setup()
    {
        wasEnabled = new bool[disableOnDeath.Length];
        for (int i = 0; i < disableOnDeath.Length; i++) // used for store the value
            wasEnabled[i] = disableOnDeath[i].enabled;
        SetDefault();
    }

    private void SetDefault() // called for set the default setting of the player
    {
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

    [ClientRpc] // this attribute says that this method will be called on clients from the server
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
        StartCoroutine(Respown()); // it respawns the player
    }

    private IEnumerator Respown() // wait fews seconds and start the procedure for respawn
    {
        yield return new WaitForSeconds(GameManager.singleton.matchSetting.respawnTime);
        SetDefault();
        Transform startPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = startPoint.position;
        transform.rotation = startPoint.rotation;
        Debug.Log(gameObject.name + "respawned");
    }

}
