using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {

    public Camera cam;
    public LayerMask mask;
    public PlayerWeapon weapon;

    private const string PLAYERTAG = "Player";
    
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Shoot();
    }

    [Client] // it is used for say that this method will be invoked only on the client
    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, weapon.range, mask))
            if (hit.collider.tag == PLAYERTAG)
                CmdPlayerShoot(hit.collider.name, weapon.damage);
    }

    [Command] // it is used for say that this method will be invoked only on the server by clients
    private void CmdPlayerShoot(string playerID, int damage)
    {
        Debug.Log(playerID + "hit!");
        Player player = GameManager.GetPlayer(playerID);
        player.RpcTakeDamage(damage);
    }

}
