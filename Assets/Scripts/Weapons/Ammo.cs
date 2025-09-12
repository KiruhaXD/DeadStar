using UnityEngine;

public class Ammo : MonoBehaviour
{
    //[SerializeField] Weapon[] weapons;
    [SerializeField] GameObject handObject;

    public void AddAmmo() 
    {
        Weapon weapon = handObject.GetComponentInChildren<Weapon>();

        if (weapon != null) 
        {
            Debug.Log(weapon.name);

            if (weapon._maxAmmo == 0 || weapon._maxAmmo > 0)
            {
                weapon._maxAmmo = 100;
                weapon.UpdateText();
            }
        }




    }


}
