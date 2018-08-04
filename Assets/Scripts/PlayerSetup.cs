using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour {

    public Behaviour[] componentsToDisable;
    public GameObject playerGraphic;
    public GameObject playerUI;

    private const string REMOTEPLAYERLAYER = "RemotePlayer";
    private const string DONTDRAWLAYER = "DontDraw";
    private Camera mainCamera;
    private Player player;
    private GameObject playerUIInstance;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponents(); // these components are disabled because, in this mode, it's not possible to control another player
            AssigneRemotePlayerLayer();
        }
        else
        {
            player.Setup();
            CreateUI();
            mainCamera = Camera.main;
            if(mainCamera != null)
                mainCamera.gameObject.SetActive(false);
            SetAllLayer(playerGraphic, LayerMask.NameToLayer(DONTDRAWLAYER)); // NameToLayer give an index from the string layer
        }
    }

    private void CreateUI()
    {
        playerUIInstance = Instantiate(playerUI);
        playerUIInstance.name = playerUI.name;
    }

    // this method sets the layer of all compoments in graphic for don't be rendered by the camera
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
        player = GetComponent<Player>();
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

    private void AssigneRemotePlayerLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(REMOTEPLAYERLAYER);
    }

    private void DisableComponents() // it disable all components in the list
    {
        foreach (Behaviour _componentsToDisable in componentsToDisable)
            _componentsToDisable.enabled = false;
    }

}
