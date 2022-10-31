using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeaponCanvas : MonoBehaviour
{
    [SerializeField]private Image weaponImage;
    private WeaponController weaponController;

    private void Awake()
    {
        weaponController = FindObjectOfType<WeaponController>();
        weaponController.OnWeaponChange += OnWeaponChange;

    }
    private void OnWeaponChange(Weapon weapon)
    {

        weaponImage.sprite = weapon.WeaponIcon;
        weaponImage.preserveAspect = true;
    }

    public void NextWeapon()
    {
        weaponController.NextWeapon();
    }

    public void PreviousWeapon()
    {
        weaponController.Previousweapon();
    }
}
