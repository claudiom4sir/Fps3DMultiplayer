using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour {

    public Behaviour[] componentsToDisable;

    private const string REMOTEPLAYERNAME = "RemotePlayer";
    private Camera mainCamera;
    private Player player;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssigneRemotePlayer();
        }
        else
        {
            mainCamera = Camera.main;
            if(mainCamera != null)
                mainCamera.gameObject.SetActive(false);
        }
        player = GetComponent<Player>();
        player.Setup();
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        SetPlayerName(); // it called here because OnStartClient() will be called before Start()
        string playerID = GetPlayerName();
        Player player = GetComponent<Player>();
        GameManager.RegisterPlayer(playerID, player);
    }

    public void SetPlayerName()
    {
        string id = GetComponent<NetworkIdentity>().netId.ToString();
        gameObject.name = "Player " + id;
    }

    private string GetPlayerName()
    {
        return gameObject.name;
    }

    private void OnDisable() // when this player will be destroyed, the main camera will be activated
    {
        if(mainCamera != null)
            mainCamera.gameObject.SetActive(true);
        GameManager.UnRegisterPlayer(gameObject.name);
    }

    private void AssigneRemotePlayer()
    {
        gameObject.layer = LayerMask.NameToLayer(REMOTEPLAYERNAME);
    }

    private void DisableComponents() // it disable all components in the list
    {
        foreach (Behaviour _componentsToDisable in componentsToDisable)
            _componentsToDisable.enabled = false;
    }

}
