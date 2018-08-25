using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class WeaponManager : NetworkBehaviour {

    public PlayerWeapon startWeapon;
    public Transform weaponHolder;
    private const string weaponLayerName = "Weapon";
    public bool isReloading = false;

    private PlayerWeapon currentWeapon;
    private GameObject weaponInstance;
    private WeaponGraphic weaponGraphics;
    private Animator animator;

	private void Start ()
    {
        SetWeapon(startWeapon);
	}

    private void SetWeaponLayer(GameObject obj)
    {
        obj.layer = LayerMask.NameToLayer(weaponLayerName);
        foreach (Transform tr in obj.transform)
            SetWeaponLayer(tr.gameObject);
    }

    private void SetWeapon(PlayerWeapon _weapon)
    {
        currentWeapon = _weapon;
        weaponInstance = Instantiate(startWeapon.graphic, weaponHolder.position, Quaternion.identity);
        weaponInstance.transform.SetParent(weaponHolder);
        weaponGraphics = weaponInstance.GetComponent<WeaponGraphic>();
        if (isLocalPlayer)
            SetWeaponLayer(weaponInstance);
        animator = weaponInstance.GetComponent<Animator>();
        if (animator == null)
            Debug.LogError("Animator is null");
    }

    public PlayerWeapon GetCurrentWeapon()
    {
        return currentWeapon;
    }
	
	public WeaponGraphic GetCurrentWeaponGraphics()
    {
        return weaponGraphics;
    }

    public void Reload()
    {
        isReloading = true;
        CmdReload();
    }

    [Command]
    private void CmdReload() // those Cmd and Rpc methods are used to sincronize the reload animation across network
    {
        RpcReload();
    }

    [ClientRpc]
    private void RpcReload()
    {
        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        Debug.Log("Reloading...");
        animator.SetTrigger("isReloading"); // start reload animation
        yield return new WaitForSeconds(currentWeapon.reloadingTime);
        animator.SetTrigger("isReloading"); // stop reload animation
        currentWeapon.SetCurrentBullets(currentWeapon.maxBullets);
        isReloading = false;
    }

}
