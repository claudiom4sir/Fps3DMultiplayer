using UnityEngine;
using UnityEngine.Networking;

public class WeaponManager : NetworkBehaviour {

    public PlayerWeapon startWeapon;
    public Transform weaponHolder;
    private const string weaponLayerName = "Weapon";

    private PlayerWeapon currentWeapon;
    private GameObject weaponInstance;

	private void Start ()
    {
        SetWeapon(startWeapon);
	}

    private void SetWeaponLayer()
    {
        weaponInstance.layer = LayerMask.NameToLayer(weaponLayerName);
    }

    private void SetWeapon(PlayerWeapon _weapon)
    {
        currentWeapon = _weapon;
        weaponInstance = Instantiate(startWeapon.graphic, weaponHolder.position, weaponHolder.rotation);
        weaponInstance.transform.SetParent(weaponHolder);
        if (isLocalPlayer)
            SetWeaponLayer();
    }

    public PlayerWeapon GetCurrentWeapon()
    {
        return currentWeapon;
    }
	
	
}
