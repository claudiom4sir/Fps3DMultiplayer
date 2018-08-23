using UnityEngine;

[System.Serializable]
public class PlayerWeapon {

    public string name = "M4";
    public int damage = 10;
    public float range = 100f;
    [Tooltip("Fire rate in minutes, not in seconds")] // used for show this comment in inspector
    public float fireRate = 0f;
    public GameObject graphic;
    public int maxBullets = 31;
    public float reloadingTime = 1f;
    private int currentBullets;

    public PlayerWeapon() // it's the only way for setting the currentBullets
    {
        currentBullets = maxBullets;
    }

    public int GetCurrentBullets()
    {
        return currentBullets;
    }

    public void SetCurrentBullets(int value)
    {
        currentBullets = value;
    }
}
