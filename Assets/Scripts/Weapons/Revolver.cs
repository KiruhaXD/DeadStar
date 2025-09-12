
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Revolver : Weapon
{
    private void Update()
    {
        if (pickUpWeapon.canPickUp == true)
        {
            //_animator.SetBool("isTaking", true);

            separationText.gameObject.SetActive(true);

            ammoTextCurrent.gameObject.SetActive(true);
            ammoTextMax.gameObject.SetActive(true);

            //if (isReloading)
            //  return;

            aimImage.gameObject.SetActive(true);

            if (_currentAmmo == 0 && _maxAmmo > 0)
            {
                stopSpawn.isCanSpawn = false;

                PlayAudioNoAmmo();

                if (Input.GetKeyDown(KeyCode.R))
                {
                    _animator.SetBool("isReloading", true);

                    StartCoroutine(Reload());
                    //return;
                }
            }

            if (_currentAmmo == 0 && _maxAmmo == 0)
            {
                stopSpawn.isCanSpawn = false;

                PlayAudioNoAmmo();
            }

            if (_currentAmmo > 0 && isReloading == false)
            {
                Shoot();
            }
        }

        else
            aimImage.gameObject.SetActive(false);
    }

    public void Shoot()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _distance))
        {
            _animator.SetBool("isShooting", false);

            if (Input.GetMouseButtonDown(0) && Time.time > _nextFire)
            {
                _currentAmmo--;
                ammoTextCurrent.text = _currentAmmo.ToString();

                _animator.SetBool("isShooting", true);

                ShootEffect();

                _nextFire = Time.time + 1f / _fireDelay;

                GameObject impact = Instantiate(_hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 2f);

                GameObject hole = Instantiate(_bulletHoleGraphic, hit.point, Quaternion.Euler(0, 180, 0));
                Destroy(hole, 2f);

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * _force);

                    if (hit.collider.tag == "ObjectTrigger") 
                    {
                        scoreManager.score += 1;

                        Destroy(hit.collider.gameObject);
                    }

                }

                Invoke("PlayShellCasingDrop", 0.5f);
            }
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;

        _audioReload.Play();

        if (_maxAmmo > 0)
        {

            _maxAmmo -= _maxCountAmmoForGun;

            _currentAmmo = _maxCountAmmoForGun;

            UpdateText();

        }

        yield return new WaitForSeconds(_reloadTime);

        _animator.SetBool("isReloading", false);

        isReloading = false;
        _audioReload.Stop();

        stopSpawn.isCanSpawn = true;
    }

    public void ShootEffect()
    {
        if (_muzzleFlash != null)
        {
            _muzzleFlash.Play();
        }

        if (_audioFire != null) 
        {
            _audioFire.Play();
        }
    }
}
