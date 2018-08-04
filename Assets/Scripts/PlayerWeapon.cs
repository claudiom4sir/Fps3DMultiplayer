using UnityEngine;

[System.Serializable]
public class PlayerWeapon {

    public string name = "M4";
    public int damage = 10;
    public float range = 100f;
    [Tooltip("Fire rate in minutes, not in seconds")] // used for show this comment in inspector
    public float fireRate = 0f;
    public GameObject graphic;

}
