using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    public Image fuelBar;
    public PlayerController playerController;
    public GameObject pauseMenuUI;
    public GameObject scoreboardUI;

    private void Update()
    {
        float fuel = playerController.GetCurrentThrusterFuel();
        SetFuelBar(fuel);
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePauseMenuUI();
        if (Input.GetKeyDown(KeyCode.Tab))
            scoreboardUI.SetActive(true);
        else if (Input.GetKeyUp(KeyCode.Tab))
            scoreboardUI.SetActive(false);
    }

    public void TogglePauseMenuUI()
    {
        pauseMenuUI.SetActive(!pauseMenuUI.activeSelf);
        PauseMenuUI.isActive = pauseMenuUI.activeSelf;
    }

    public void SetFuelBar(float fuel)
    {
        if(fuel > 0f)
            fuelBar.transform.localScale = new Vector3(1f, fuel, 1f);
    }

}
