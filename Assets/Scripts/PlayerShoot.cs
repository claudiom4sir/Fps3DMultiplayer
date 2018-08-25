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
        if (!isLocalPlayer)
            return;
        if (PauseMenuUI.isActive)
            return;
        if (weaponManager.isReloading)
            return;
        currentWeapon = weaponManager.GetCurrentWeapon();
        if (currentWeapon.GetCurrentBullets() < currentWeapon.maxBullets)
            if (Input.GetKeyDown(KeyCode.R) || currentWeapon.GetCurrentBullets() <= 0)
                weaponManager.Reload();
        if (Input.GetKeyDown(KeyCode.K)) // used for suiciding
            CmdPlayerShoot(gameObject.name, 99999, gameObject.name);
        if (Input.GetButtonDown("Fire1"))
            InvokeRepeating("Shoot", 0f, 60f / currentWeapon.fireRate);
        if (Input.GetButtonUp("Fire1"))
            CancelInvoke("Shoot");
    }

    [Command] // it is called from the player on th server when he shoots
    private void CmdOnShoot()
    {
        RpcDoShootEffects();
    }

    [Command] // it is called from the client to the server when client hits something
    private void CmdOnHit(Vector3 position, Vector3 direction)
    {
        RpcDoHitEffect(position, direction);
    }

    [ClientRpc] // it is called from the server to all players for display the shoot effect
    private void RpcDoShootEffects()
    {
        ParticleSystem muzzleFlash = weaponManager.GetCurrentWeaponGraphics().muzzleFlash;
        muzzleFlash.Play();
    }

    [ClientRpc] // it is called from the server to all clients for show the hit effect on every clients
    private void RpcDoHitEffect(Vector3 position, Vector3 direction)
    {
        ParticleSystem hitEffect = (ParticleSystem) Instantiate(weaponManager.GetCurrentWeaponGraphics().hitEffect, position, Quaternion.LookRotation(direction));
        hitEffect.Play();
        Destroy(hitEffect.gameObject, 0.5f);
    }

    [Client] // it is used for say that this method will be invoked only on the client
    private void Shoot()
    {
        if (weaponManager.isReloading)
            return;
        CmdOnShoot();
        currentWeapon.SetCurrentBullets(currentWeapon.GetCurrentBullets() - 1);
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, currentWeapon.range, mask))
        {
            CmdOnHit(hit.point, hit.normal); // call this method with the point hitten and the perpendicular vector of this point
            if (hit.collider.tag == PLAYERTAG)
                CmdPlayerShoot(hit.collider.name, currentWeapon.damage, gameObject.name);
        }
    }

    [Command] // it is used for say that this method will be invoked only on the server by clients
    private void CmdPlayerShoot(string playerID, int damage, string whoShootsID)
    {
        Debug.Log(playerID + "hit!");
        Player player = GameManager.GetPlayer(playerID);
        player.RpcTakeDamage(damage, whoShootsID);
    }

}
