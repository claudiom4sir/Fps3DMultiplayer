using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

    public Behaviour[] componentsToDisable;
    private string remoteLayerName = "RemotePlayer";
    private Camera mainCamera;


    private void Start()
    {
        SetPlayerName();
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

    }

    private void SetPlayerName()
    {
        string id = GetComponent<NetworkIdentity>().netId.ToString();
        gameObject.name = "Player " + id;
    }

    private void OnDisable() // when this player will be destroyed, the main camera will be activated
    {
        if(mainCamera != null)
            mainCamera.gameObject.SetActive(true);
    }

    private void AssigneRemotePlayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    private void DisableComponents() // it disable all components in the list
    {
        foreach (Behaviour _componentsToDisable in componentsToDisable)
            _componentsToDisable.enabled = false;
    }

}
