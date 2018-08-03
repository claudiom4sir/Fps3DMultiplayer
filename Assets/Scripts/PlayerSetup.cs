using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour {

    public Behaviour[] componentsToDisable;
    public string dontDrawLayer = "DontDraw";
    public GameObject graphic;
    public GameObject playerUI;

    private const string REMOTEPLAYERNAME = "RemotePlayer";
    private Camera mainCamera;
    private Player player;
    private GameObject playerUIInstance;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponents(); // these components are disabled because, in this mode, it's not possible to control another player
            AssigneRemotePlayer();
        }
        else
        {
            mainCamera = Camera.main;
            if(mainCamera != null)
                mainCamera.gameObject.SetActive(false);
            SetAllLayer(graphic, LayerMask.NameToLayer(dontDrawLayer)); // NameToLayer give an index from the string layer
        }
        player = GetComponent<Player>();
        player.Setup();
        CreateUI();
    }

    private void CreateUI()
    {
        playerUIInstance = Instantiate(playerUI);
        playerUIInstance.name = playerUI.name;
    }

    // this method sets the layer of all compoments in graphic for don't be represented by the camera
    private void SetAllLayer(GameObject graphic, int layerIndex)
    {
        graphic.layer = layerIndex;
        foreach (Transform obj in graphic.transform)
            SetAllLayer(obj.gameObject, layerIndex);
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
        Destroy(playerUIInstance);
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
