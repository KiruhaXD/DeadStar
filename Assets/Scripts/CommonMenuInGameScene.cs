using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CommonMenuInGameScene : MonoBehaviour
{
    public GameObject graphicsPanel;
    public GameObject audioPanel;
    public GameObject keybinPanel;

    [SerializeField] SwitchCameras _switchCameras;
    [SerializeField] TMP_Text tMP_Text;
    [SerializeField] AudioSource _backgroundMusic;
    [SerializeField] GameObject _menuPanel;
    [SerializeField] GameObject _interfacePanel;
    [SerializeField] MouseSensivity _mouseSensivity;
    [SerializeField] ScoreManager _scoreManager;

    private void Start()
    {
        _interfacePanel.SetActive(false);
    }

    private void Update()
    {
        if (_menuPanel.activeSelf)
            _mouseSensivity.GetComponent<MouseSensivity>().enabled = false;

        if (Input.GetKeyDown(KeyCode.Escape) && _menuPanel.activeSelf) 
        {
            SwitchToPlayerCamera();
            _scoreManager.bestScoreText.text = "" + PlayerPrefs.GetInt("score").ToString();
            Invoke("OnMouseSensivity", 2f);
        }

    }

    private void OnMouseDown()
    {
        switch (tMP_Text.name) 
        {
            case "Text (Play)":

                break;

            case "Text (Tutorial) ":
                SceneTransition.SwitchToScene("Tutorial");
                Debug.Log("you are in tutorial");
                break;

            case "Text (Settings)":
                _switchCameras._virtualCameras[3].gameObject.SetActive(true);
                break;

            case "Text (Author)":
                _switchCameras._virtualCameras[2].gameObject.SetActive(true);
                break;

            case "Text (Exit)":
                Application.Quit();
                Debug.Log("Exit succesful");
                break;

            case "Text (ReturnFromSettings)":
                _switchCameras._virtualCameras[3].gameObject.SetActive(false);
                break;

            case "Text (ReturnFromAuthor)":
                _switchCameras._virtualCameras[2].gameObject.SetActive(false);
                break;

            case "Text (Usual) ":
                SwitchToPlayerCamera();
                _scoreManager.bestScoreText.text = "" + PlayerPrefs.GetInt("score").ToString();
                Invoke("OnMouseSensivity", 2f);
                break;
        }
    }

    public void SwitchToPlayerCamera() 
    {
        _switchCameras.SwitchCinemachineCameras();
        _menuPanel.SetActive(false);
        _backgroundMusic.Stop();
        _interfacePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnMouseSensivity() 
    {
        _mouseSensivity.GetComponent<MouseSensivity>().enabled = true;
    }
}
