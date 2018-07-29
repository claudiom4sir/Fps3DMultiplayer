using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {

    public Camera cam;
    public PlayerWeapon weapon;
    public LayerMask mask;

    private const string PLAYERTAG = "Player";

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Shoot();
    }

    [Client] // it is used for to say that this method will be invoked only on the client
    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, weapon.range, mask))
            if (hit.collider.tag == PLAYERTAG)
                CmdPlayerShoot(hit.collider.name);
    }

    [Command] // it is used for to say that this method will be invoked only on the server
    private void CmdPlayerShoot(string id)
    {
        Debug.Log(id + "hit!");
    }

}
