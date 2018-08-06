using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    public Image fuelBar;
    public PlayerController playerController;

    private void Update()
    {
        float fuel = playerController.GetCurrentThrusterFuel();
        SetFuelBar(fuel);
    }

    public void SetFuelBar(float fuel)
    {
        if(fuel > 0f)
            fuelBar.transform.localScale = new Vector3(1f, fuel, 1f);
    }

}
