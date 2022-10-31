using System;
using System.Collections.Generic;
using UnityEngine;

//Control the weapons that the player is holding
//Change the current holding weapon
public class WeaponController : MonoBehaviour
{
    private List<Weapon> weapons = new List<Weapon>();
    private Weapon currentWeapon;
    private int currentweaponIndex = 0;
    public Action<Weapon> OnWeaponChange;
    
    //Add weapons
    private void Awake()
    {
        
        SingleMissileWeapon singleMissileWeapon = new SingleMissileWeapon(transform);
        MultiMissileWeapon multiMissileWeapon = new MultiMissileWeapon(transform);
        NukeWeapon nukeWeapon = new NukeWeapon(transform);
        
        weapons.Add(singleMissileWeapon);
        weapons.Add(multiMissileWeapon);
        weapons.Add(nukeWeapon);
    }

    //Set first weapon added as current weapon
    private void Start()
    {
        currentWeapon = weapons[currentweaponIndex];
        currentWeapon.OnWeaponSelect();
        OnWeaponChange?.Invoke(currentWeapon);
    }

    //Update current weapon
    private void Update()
    {
        if (currentWeapon != null)
        {
            currentWeapon.Update();
        }
    }

    public void NextWeapon()
    {
        currentWeapon.OnWeaponDeselect();
        if (currentweaponIndex < weapons.Count - 1)
        {
            currentweaponIndex++;
        }
        else
        {
            currentweaponIndex = 0;
        }

        currentWeapon = weapons[currentweaponIndex];
        currentWeapon.OnWeaponSelect();
        OnWeaponChange?.Invoke(currentWeapon);
    }

    public void Previousweapon()
    {
        currentWeapon.OnWeaponDeselect();
        if (currentweaponIndex > 0)
        {
            currentweaponIndex--;
        }
        else
        {
            currentweaponIndex = weapons.Count - 1;
        }

        currentWeapon = weapons[currentweaponIndex];
        currentWeapon.OnWeaponSelect();
        OnWeaponChange?.Invoke(currentWeapon);
    }
}
