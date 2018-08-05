using UnityEngine;
using UnityEngine.Networking;

public class WeaponManager : NetworkBehaviour {

    public PlayerWeapon startWeapon;
    public Transform weaponHolder;
    private const string weaponLayerName = "Weapon";

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
}
