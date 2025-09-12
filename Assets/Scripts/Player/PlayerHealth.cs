using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int _currentHealth = 5;

    [SerializeField] GameObject _losePanel;
    [SerializeField] Image[] imageHealth;

    [SerializeField] Timer _timer;
    [SerializeField] PlayerController _playerController;
    [SerializeField] Weapon[] _weapons;
    [SerializeField] MouseSensivity _mouseSensivity;
    //public Animator[] animator;

    private void Start()
    {
        _losePanel.SetActive(false);
    }

    public void TakeDamage() 
    {
        if (_currentHealth < 0)
            throw new ArgumentOutOfRangeException();

        DestroyImages();

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;

            _losePanel.SetActive(true);
            OffScripts();

            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void CloseLoseWindow() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        _losePanel.SetActive(false);
    }

    public void OffScripts() 
    {
        _timer.GetComponent<Timer>().enabled = false;
        _playerController.GetComponent<PlayerController>().enabled = false;
        _mouseSensivity.GetComponent<MouseSensivity>().enabled = false;

        foreach (Weapon weapon in _weapons) 
        {
            weapon.GetComponent<Weapon>().enabled = false;
        }
    }

    public void DestroyImages() 
    {
        if (imageHealth[4] != null)
        {
            Destroy(imageHealth[4]);
            _currentHealth = 4;
            //animator[4].SetBool("isDamage", true);
        }

        else if (imageHealth[3] != null)
        {
            Destroy(imageHealth[3]);
            _currentHealth = 3;
            //animator[3].SetBool("isDamage", true);
        }

        else if (imageHealth[2] != null)
        {
            Destroy(imageHealth[2]);
            _currentHealth = 2;
            //animator[2].SetBool("isDamage", true);
        }

        else if (imageHealth[1] != null)
        {
            Destroy(imageHealth[1]);
            _currentHealth = 1;
            //animator[1].SetBool("isDamage", true);
        }

        else if (imageHealth[0] != null)
        {
            Destroy(imageHealth[0]);
            _currentHealth = 0;
            //animator[0].SetBool("isDamage", true);
        }
    }
}
