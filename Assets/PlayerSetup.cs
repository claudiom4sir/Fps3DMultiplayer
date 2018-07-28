using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

    public Behaviour[] componentsToDisable;
    public Camera mainCamera;

    private void Start()
    {
        if (!isLocalPlayer)
            foreach (Behaviour _componentsToDisable in componentsToDisable)
                _componentsToDisable.enabled = false;
        else
        {
            mainCamera = Camera.main;
            mainCamera.gameObject.SetActive(false);
        }

    }

    private void OnDisable() // when this player will be destroyed, the main camera will be activated
    {
        mainCamera.gameObject.SetActive(true);
    }

}
