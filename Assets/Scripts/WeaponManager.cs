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
        weaponInstance = Instantiate(startWeapon.graphic, weaponHolder.position, weaponHolder.rotation);
        weaponInstance.transform.SetParent(weaponHolder);
        weaponGraphics = weaponInstance.GetComponent<WeaponGraphic>();
        if (isLocalPlayer)
            SetWeaponLayer(weaponInstance);
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
        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(currentWeapon.reloadingTime);
        currentWeapon.SetCurrentBullets(currentWeapon.maxBullets);
        isReloading = false;
    }

}
