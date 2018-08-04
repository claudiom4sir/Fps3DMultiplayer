using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(WeaponManager))]
public class PlayerShoot : NetworkBehaviour {

    public Camera cam;
    public LayerMask mask;

    private PlayerWeapon currentWeapon;
    private WeaponManager weaponManager;
    private const string PLAYERTAG = "Player";

    private void Start()
    {
        weaponManager = GetComponent<WeaponManager>();
    }

    private void Update()
    {
        currentWeapon = weaponManager.GetCurrentWeapon();
        if (Input.GetButtonDown("Fire1"))
            InvokeRepeating("Shoot", 0f, 60f / currentWeapon.fireRate);
        else if (Input.GetButtonUp("Fire1"))
            CancelInvoke("Shoot");
    }

    [Client] // it is used for say that this method will be invoked only on the client
    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, currentWeapon.range, mask))
            if (hit.collider.tag == PLAYERTAG)
                CmdPlayerShoot(hit.collider.name, currentWeapon.damage);
    }

    [Command] // it is used for say that this method will be invoked only on the server by clients
    private void CmdPlayerShoot(string playerID, int damage)
    {
        Debug.Log(playerID + "hit!");
        Player player = GameManager.GetPlayer(playerID);
        player.RpcTakeDamage(damage);
    }

}
