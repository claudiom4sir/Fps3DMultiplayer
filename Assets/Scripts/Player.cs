using UnityEngine.Networking;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerSetup))]
public class Player : NetworkBehaviour {

    public int startHealth = 100;
    public Behaviour[] disableOnDeath;
    public GameObject[] goToDisableOnDeath;
    public GameObject deathEffect;

    [SyncVar]   // used for change this value in every clients that are connected with the server 
    private int currentHeath;
    [SyncVar]
    private bool isDead = false;
    private bool[] wasEnabled;
    private bool isFirstSetup = true;

    private void Update() // used for testing 
    {
        if (Input.GetKey(KeyCode.K))
            RpcTakeDamage(200);
    }

    public void PlayerSetup()
    {
        CmdBroadCastPlayerSetup();
    }

    [Command]
    private void CmdBroadCastPlayerSetup()
    {
        RpcPlayerSetupInAllClients();
    }

    [ClientRpc]
    private void RpcPlayerSetupInAllClients()
    {
        if (isFirstSetup)
        {
            wasEnabled = new bool[disableOnDeath.Length];
            for (int i = 0; i < disableOnDeath.Length; i++) // used for store the value
                wasEnabled[i] = disableOnDeath[i].enabled;
            isFirstSetup = false;
        }
        SetDefault();
    }

    private void SetDefault() // called for set the default setting of the player
    {
        if (isLocalPlayer)
        {
            GameManager.singleton.SetCameraState(false);
            GetComponent<PlayerSetup>().GetPlayerUIInstace().SetActive(true);
        }
        currentHeath = startHealth;
        isDead = false;
        for (int i = 0; i < goToDisableOnDeath.Length; i++)
            goToDisableOnDeath[i].SetActive(true);
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
        if (isLocalPlayer)
        {
            GameManager.singleton.SetCameraState(true);
            GetComponent<PlayerSetup>().GetPlayerUIInstace().SetActive(false);
        }
        for (int i = 0; i < goToDisableOnDeath.Length; i++)
            goToDisableOnDeath[i].SetActive(false);
        for (int i = 0; i < disableOnDeath.Length; i++)
            disableOnDeath[i].enabled = false;
        Collider collider = GetComponent<Collider>();
        if (collider != null)
            collider.enabled = false;
        GameObject _deathEffect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(_deathEffect, 2f);
        StartCoroutine(Respown()); // it respawns the player
    }

    private IEnumerator Respown() // wait fews seconds and start the procedure for respawn
    {
        yield return new WaitForSeconds(GameManager.singleton.matchSetting.respawnTime);
        Transform startPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = startPoint.position;
        transform.rotation = startPoint.rotation;
        PlayerSetup(); // not PlayerSetup() because it is already executed one time, when local player borns
        Debug.Log(gameObject.name + "respawned");
    }

}
