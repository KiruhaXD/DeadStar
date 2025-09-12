using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInGame : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject settingsMenu;
    public GameObject handObject;

    [Header("Reference")]
    [SerializeField] PlayerController playerController;
    [SerializeField] PlayerInteraction playerInteraction;
    [SerializeField] MouseSensivity mouseSensivity;
    [SerializeField] GameObject bestScoreObject;
    [SerializeField] GameObject scoreObject;
    [SerializeField] Ammo ammo;
    [SerializeField] Timer timer;
    public Weapon[] weapons;
    [SerializeField] PickUpWeapon pickUpWeapon;

    private void Start()
    {
        menuPanel.SetActive(false);
        settingsMenu.SetActive(false);
    }

    private void Update()
    {
        EscapePressInGame();
    }

    public void EscapePressInGame()
    {        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsMenu.activeSelf == true)
            {
                bestScoreObject.SetActive(false);
                scoreObject.SetActive(false);

                EscapePressInMenu();
                return;
            }

            if (menuPanel.activeSelf == true)
            {
                bestScoreObject.SetActive(true);
                scoreObject.SetActive(true);

                menuPanel.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                EnabledScripts();
                return;
            }

            bestScoreObject.SetActive(false);
            scoreObject.SetActive(false);

            menuPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            DisableScripts();
        }
    }

    public void ContinueButton()
    {
        bestScoreObject.SetActive(true);
        scoreObject.SetActive(true);

        menuPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        EnabledScripts();
    }

    public void SettingButton() => settingsMenu.SetActive(true);

    public void EscapePressInMenu() => settingsMenu.SetActive(false);


    public void ExitGameInMenuButton() 
    {
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            SceneTransition.SwitchToScene("SampleScene");
        }

        else 
        {
            Application.Quit();
            Debug.Log("Exit succesful");
        }
    }

    public void EnabledScripts() 
    {
        playerController.GetComponent<PlayerController>().enabled = true;
        playerInteraction.GetComponent<PlayerInteraction>().enabled = true;
        ammo.GetComponent<Ammo>().enabled = true;
        timer.GetComponent<Timer>().enabled = true;
        mouseSensivity.GetComponent<MouseSensivity>().enabled = true;

        Weapon weapon = handObject.GetComponentInChildren<Weapon>();
        if (weapon != null) 
        {
            Debug.Log(weapon.name);

            weapon.GetComponent<Weapon>().enabled = true;
            weapon.UpdateText();
        }
    }

    public void DisableScripts() 
    {
        playerController.GetComponent<PlayerController>().enabled = false;
        playerInteraction.GetComponent<PlayerInteraction>().enabled = false;
        ammo.GetComponent<Ammo>().enabled = false;
        timer.GetComponent<Timer>().enabled = false;
        mouseSensivity.GetComponent<MouseSensivity>().enabled = false;

        foreach (var weapon in weapons)
        {
            weapon.GetComponent<Weapon>().enabled = false;
            weapon.UpdateText();
        }
    }
}
