﻿using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerController))]
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
            DisableComponents();    // these components are disabled because, in this mode, it's not possible to control another player
            AssigneRemotePlayerLayer();
        }
        else
        { 
            SetAllLayer(playerGraphic, LayerMask.NameToLayer(DONTDRAWLAYER));   // NameToLayer give an index from the string layer
            CreateUI(); // only local player has a playerUI
            if (UserAccountManager.singleton.isLoggedIn)
                CmdSetUsername(UserAccountManager.singleton.username);
            player.PlayerSetup();
        }
    }

    [Command]
    private void CmdSetUsername(string _username)
    {
        player.username = _username;
        RpcShowPlayerJoinMessage(_username);
    }

    [ClientRpc]
    private void RpcShowPlayerJoinMessage(string _username) // this method might be in InfoPanel class
    {
        GameManager.singleton.onPlayerJoinCallBack(_username);
    }

    private void CreateUI()
    {
        // creation
        playerUIInstance = Instantiate(playerUI); 
        playerUIInstance.name = playerUI.name;

        // set playerController in playerUIInstance
        PlayerUI pUI = playerUIInstance.GetComponent<PlayerUI>();
        pUI.playerController = GetComponent<PlayerController>();
        if (pUI.playerController == null)
            Debug.Log("error");
    }

    public GameObject GetPlayerUIInstace()
    {
        return playerUIInstance;
    }

    // this method sets the layer of all components in graphic for don't be rendered by the camera
    private void SetAllLayer(GameObject graphic, int layerIndex)
    {
        graphic.layer = layerIndex;
        foreach (Transform obj in graphic.transform)
            SetAllLayer(obj.gameObject, layerIndex);
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        player = GetComponent<Player>();
        SetPlayerName();    // it called here because OnStartClient() will be called before Start()
        string playerID = GetPlayerName();
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

    private void OnDisable()    // when this player will be destroyed, the main camera will be activated
    {
        if(isLocalPlayer)
            GameManager.singleton.SetCameraState(true);
        GameManager.UnRegisterPlayer(gameObject.name);
        Destroy(playerUIInstance);
        if(!isLocalPlayer) // if is not local player, before be destroyed, it shows the message in InfoPanel 
            GameManager.singleton.onPlayerLeaveRoomCallBack(player.username);
    }

    private void AssigneRemotePlayerLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(REMOTEPLAYERLAYER);
    }

    private void DisableComponents()    // it disable all components in the list
    {
        foreach (Behaviour _componentsToDisable in componentsToDisable)
            _componentsToDisable.enabled = false;
    }

}
