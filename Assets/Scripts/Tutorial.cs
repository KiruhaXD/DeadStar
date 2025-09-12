using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] MouseSensivity _mouseSensivity;
    [SerializeField] PlayerController _playerController;
    [SerializeField] PlayerInteraction _interaction;
    [SerializeField] PlayerHealth _playerHealth;
    [SerializeField] MenuSettings _menuSettingsPlayScenes;
    [SerializeField] ScoreManager _scoreManager;

    [SerializeField] GameObject _panelGreeting;
    [SerializeField] Image[] _images;
    [SerializeField] Button _closeWindowWithGreeting;
    [SerializeField] Button _nextWindowEnter;

    [SerializeField] GameObject[] _tips;

    [SerializeField] GameObject _handObject;

    private int keyPress = 1;
    private bool pressButtonEnter = false;
    public bool isPressR = true;
    public bool isStartTutorial;

    private void Awake()
    {   
        _scoreManager.bestScoreText.text = "" + PlayerPrefs.GetInt("score").ToString();
    }

    private void Start()
    {
        _mouseSensivity.GetComponent<MouseSensivity>().enabled = false;
        _playerController.GetComponent<PlayerController>().enabled = false;
        _panelGreeting.SetActive(true);

        if (_images[0].name == "Image (GreetingThirst)")
        {
            StartCoroutine(OnButtonEnterNextWindow());
        }

        Cursor.lockState = CursorLockMode.None;

        for (int i = 0; i < _tips.Length; i++) 
        {
            _tips[i].SetActive(false);
        }

        isStartTutorial = true;

        _interaction.startButtonObject.tag = "Untagged";
    }

    private void Update()
    {
        if (isStartTutorial == true) 
        {
            StartTutorial();
        }
    }

    public void EnterWindow() 
    {
        switch (pressButtonEnter) 
        {
            case false:
                _images[1].gameObject.SetActive(true);

                _images[0].gameObject.SetActive(false);

                pressButtonEnter = true;
                break;

            case true:
                _images[2].gameObject.SetActive(true);

                _images[1].gameObject.SetActive(false);

                StartCoroutine(OnButtonCloseWindow());
                _nextWindowEnter.gameObject.SetActive(false);
                break;
        }
    }

    public void CloseWindow() 
    {
        _panelGreeting.SetActive(false);
        _mouseSensivity.GetComponent<MouseSensivity>().enabled = true;
        _playerController.GetComponent<PlayerController>().enabled = true;
        _menuSettingsPlayScenes.LoadSettingSensivity();

        Cursor.lockState = CursorLockMode.Locked;

        for (int i = 0; i < _tips.Length; i++)
        {
            _tips[i].SetActive(false);
        }

        _tips[0].SetActive(true);
    }

    IEnumerator OnButtonCloseWindow() 
    {
        yield return new WaitForSeconds(2);
        _closeWindowWithGreeting.gameObject.SetActive(true);
    }

    IEnumerator OnButtonEnterNextWindow() 
    {
        yield return new WaitForSeconds(2);
        _nextWindowEnter.gameObject.SetActive(true);
    }

    public void StartTutorial() 
    {
        if (_interaction.nameGun.text == "revolver")
            _tips[4].SetActive(true);

        if (_interaction.OnUIInteraction == false)
            _tips[4].SetActive(false);

        Weapon weapon = _handObject.GetComponentInChildren<Weapon>();

        if (weapon != null)
        {
            _tips[4].SetActive(false);

            _tips[0].SetActive(false);
            _tips[1].SetActive(true);

            if (weapon._maxAmmo > 0)
            {
                if (isPressR == true)
                    _tips[5].SetActive(true);

                if (Input.GetKeyDown(KeyCode.R))
                {
                    isPressR = true;
                    _tips[5].SetActive(false);
                    _tips[6].SetActive(true);

                    isPressR = false;

                    if (isPressR == false)
                    {
                        _tips[8].SetActive(true);
                    }
                }

                if (Input.GetMouseButtonDown(0) && weapon._currentAmmo > 0)
                    _tips[8].SetActive(false);

                if (Input.GetKeyDown(KeyCode.Space) && isPressR == false)
                {
                    _tips[6].SetActive(false);

                    switch (keyPress)
                    {
                        case 1:
                            _tips[2].SetActive(true);

                            keyPress = 2;
                            break;

                        case 2:
                            _tips[2].SetActive(false);
                            _tips[3].SetActive(true);

                            keyPress = 3;
                            break;

                        case 3:
                            _tips[3].SetActive(false);
                            _tips[9].SetActive(true);

                            keyPress = 4;
                            break;

                        case 4:
                            _tips[9].SetActive(false);
                            _tips[10].SetActive(true);

                            keyPress = 5;
                            break;

                        case 5:
                            _tips[10].SetActive(false);
                            _tips[11].SetActive(true);

                            keyPress = 6;
                            break;

                        case 6:
                            _tips[11].SetActive(false);
                            _tips[12].SetActive(true);

                            keyPress = 7;
                            break;

                        case 7:
                            _tips[12].SetActive(false);
                            _tips[7].SetActive(true);
                            _interaction.startButtonObject.tag = "StartButton";
                            break;

                    }
                }

                if (_interaction.startButtonObject.tag == "StartButton") 
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        _tips[7].SetActive(false);
                        isStartTutorial = false;
                    }
                }

                _tips[1].SetActive(false);

            }
        }
    }
}
