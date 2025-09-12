using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;


public class PickUpWeapon : MonoBehaviour
{
    public GameObject mainCamera;
    public float distance = 15f;
    public GameObject currentWeapon;
    public bool canPickUp;

    public Weapon[] weapons;
    public Dictionary<string, int> weaponIndex = new Dictionary<string, int> 
    {
        { "revolver", 0 },
        { "shotgun", 1 },
        { "carabine", 2 },
        { "mauser", 3 },
        { "volcanic", 4 },
    };

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) PickUp();

        if (Input.GetKeyDown(KeyCode.Q)) Drop();
    }

    public void PickUp() 
    {
        RaycastHit hit;

        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, distance)) 
        {
            if(hit.transform.tag == "Weapon") 
            {
                if (canPickUp) Drop();

                currentWeapon = hit.transform.gameObject;
                currentWeapon.GetComponent<Rigidbody>().isKinematic = true;
                currentWeapon.transform.parent = transform;
                currentWeapon.transform.localPosition = Vector3.zero;
                currentWeapon.transform.localEulerAngles = new Vector3(0f, 0f, 0f);

                if (currentWeapon.name == "carabine")
                    currentWeapon.transform.localPosition = new Vector3(-0.095f, -0.093f, -0.319f);

                if (currentWeapon.name == "mauser") 
                {
                    currentWeapon.transform.localPosition = new Vector3(0, -0.07f, 0);
                    currentWeapon.transform.localEulerAngles = new Vector3(0f, 0, 0f);
                }

                if (currentWeapon.name == "volcanic")
                {
                    currentWeapon.transform.localPosition = new Vector3(-0.059f, -0.111f, 0);
                    currentWeapon.transform.localEulerAngles = new Vector3(0f, 0, 0f);
                }


                canPickUp = true;

                Weapon weapon = GetComponentInChildren<Weapon>();
                if (weapon != null)
                {
                    Debug.Log(weapon.name);

                    if (weaponIndex.TryGetValue(weapon.name, out int index)) 
                    {
                        for (int i = 0; i < weapons.Length; i++) 
                        {
                            weapons[i].GetComponent<Weapon>().enabled = false;
                        }

                        weapons[index].GetComponent<Weapon>().enabled = true;
                        weapons[index].UpdateText();
                    }
                }
            }
        }
    }

    public void Drop() 
    {
        currentWeapon.transform.parent = null;
        currentWeapon.GetComponent<Rigidbody>().isKinematic = false;

        Weapon weapon = GetComponentInChildren<Weapon>();
        if (weapon != null)
        {
            Debug.Log(weapon.name);

            if (weaponIndex.TryGetValue(weapon.name, out int index))
            {
                for (int i = 0; i < weapons.Length; i++)
                {
                    weapons[i].GetComponent<Weapon>().enabled = false;
                }

                weapons[index].GetComponent<Weapon>().enabled = false;
                weapons[index].UpdateText();
            }
        }

        canPickUp = false;

        for (int i = 0; i < weapons.Length; i++) 
        {
            if (weapons[i].isReloading == true) 
            {
                switch (weapons[i].name)
                {
                    case "revolver":
                        weapons[0].CheckGunOnReload();
                        break;

                    case "shotgun":
                        weapons[1].CheckGunOnReload();
                        break;

                    case "carabine":
                        weapons[2].CheckGunOnReload();
                        break;

                    case "mauser":
                        weapons[3].CheckGunOnReload();
                        break;

                    case "volcanic":
                        weapons[4].CheckGunOnReload();
                        break;
                }
            }
        }


        currentWeapon = null;

        CloseText();
    }

    public void CloseText()
    {
        foreach (var weapon in weapons)
        {
            weapon.ammoTextCurrent.gameObject.SetActive(false);
            weapon.ammoTextMax.gameObject.SetActive(false);
            weapon.separationText.gameObject.SetActive(false);
        }
    }
}
