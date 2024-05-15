using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class WeaponInventoryScript : MonoBehaviour
{
    public List<WeaponClass> weapons = new List<WeaponClass>();
    public Text weaponInventoryText;
    public WeaponClass currentWeapon;
    public int selectedWeapon;
    private InputAction weaponSelect;

    public delegate void OnWeaponChanged(WeaponClass newWeapon);
    public event OnWeaponChanged onWeaponChanged;


    public void Initialize(InputAction weaponSelect) {
        this.weaponSelect = weaponSelect;
        this.weaponSelect.Enable();
        selectedWeapon = 0;
    }

    void Update(){
        weaponSelect.performed += _ =>{
            SelectWeapon(weaponSelect.ReadValue<float>());
        };
    }

    public void AddWeapon(WeaponClass weapon)
    {
        weapons.Add(weapon);
        if(weapons.Count == 1){
            currentWeapon = weapons[0];
            onWeaponChanged?.Invoke(currentWeapon);
            UpdateWeaponInventoryText();
        }
        
    }

    public void Removeweapon(WeaponClass weapon)
    {
        if (weapons.Contains(weapon))
        {
            weapons.Remove(weapon);
            UpdateWeaponInventoryText();
        }
    }

    public void SelectWeapon(float direction){
        if (weapons.Count > 1){
            selectedWeapon = (selectedWeapon + ((int)direction/120) + weapons.Count) % weapons.Count;
            currentWeapon = weapons[selectedWeapon];
            onWeaponChanged?.Invoke(currentWeapon);
            UpdateWeaponInventoryText();
        }
    }

    public WeaponClass FindWeaponByName(string weaponName)
    {
        foreach (WeaponClass weapon in weapons)
        {
            if (weapon.weaponName == weaponName)
            {
                return weapon;
            }
        }
        return null;
    }


    public void UseWeapon()
    {
        if (currentWeapon != null && currentWeapon.canAttack){
            currentWeapon.WeaponAttack();
            StartCoroutine(StartAttackTimer());
        }
    }

    IEnumerator StartAttackTimer(){
        WeaponClass thisWep = currentWeapon;
        thisWep.canAttack = false;

        yield return new WaitForSeconds(currentWeapon.attackInterval);

        thisWep.canAttack = true;
    }

    private void UpdateWeaponInventoryText()
    {
        if (weaponInventoryText != null)
        {
            weaponInventoryText.text = "Weapon: " + currentWeapon.weaponName;
        }
    }
}
