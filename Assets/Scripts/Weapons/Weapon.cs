using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public Image aimImage;
    public Transform pointFire;

    [Header("Audio")]
    [SerializeField] protected AudioSource _audioFire;
    [SerializeField] protected AudioSource _audioSourceNoAmmo;
    [SerializeField] public AudioSource _audioReload;
    [SerializeField] protected AudioSource _audioShellCasingDrop; // звук падения гильзы

    [Header("Count bullets")]
    public int _maxAmmo = 100;
    public int _currentAmmo;

    [SerializeField] public int _maxCountAmmoForGun;

    [SerializeField] protected float _reloadTime = 2f;
    public TMP_Text ammoTextCurrent;
    public TMP_Text ammoTextMax;

    public TMP_Text separationText; // это знак разделение между текущими патронами и максимальными

    public bool isReloading = false;

    [Header("Particle System")]
    [SerializeField] protected ParticleSystem _muzzleFlash;
    [SerializeField] protected GameObject _hitEffect;

    [Header("Graphics")]
    [SerializeField] protected GameObject _bulletHoleGraphic;

    [Header("Settings")]
    [SerializeField] protected float _fireDelay = 1f;
    [SerializeField] protected float _distance = 15f;
    [SerializeField] protected float _force = 155f;
    [SerializeField] protected float _nextFire = 0f;

    [Header("Animator")]
    [SerializeField] public Animator _animator;

    [Header("Camera")]
    public Camera cam;

    [Header("Reference")]
    public PickUpWeapon pickUpWeapon;
    public Spawn stopSpawn;
    [SerializeField] protected ScoreManager scoreManager;

    // доделать анимации(дробовика и револьвера) и перезарядку
    private void Start()
    {
        separationText.gameObject.SetActive(false);

        _maxAmmo = 0;
        _currentAmmo = 0;

        ammoTextCurrent.gameObject.SetActive(false);
        ammoTextMax.gameObject.SetActive(false);
    }

    public void UpdateText() 
    {
        ammoTextMax.text = _maxAmmo.ToString();
        ammoTextCurrent.text = _currentAmmo.ToString();
    }

    public void PlayAudioNoAmmo()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _audioSourceNoAmmo.Play();
        }
    }

    public void CheckGunOnReload() 
    {
        isReloading = false;
        _animator.SetBool("isReloading", false);
        _audioReload.Stop();
        _currentAmmo = 0;
        _maxAmmo += _maxCountAmmoForGun;
        UpdateText();
    }

    public void PlayShellCasingDrop() => _audioShellCasingDrop.Play();
}
