using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponClass : WeaponClass
{
    public GameObject weaponPrefab;
    public float swingAmount;
    
    public MeleeWeaponClass(string itemName, Sprite icon, string description, GameObject attackPoint, 
                            float attackInterval, GameObject weaponPrefab, float swingAmount)
    :base(itemName, icon, description, attackPoint, attackInterval){
        this.weaponPrefab = weaponPrefab;
        this.swingAmount = swingAmount;
    }

    public override void WeaponAttack(){
        Transform attackPivot = attackPoint.transform;
        GameObject weapon = Object.Instantiate(weaponPrefab, attackPivot.position, Quaternion.identity);
        weapon.transform.SetParent(attackPivot);
        WeaponInstanceScript weaponScript = weapon.GetComponent<WeaponInstanceScript>();
        if (weaponScript != null){
            weaponScript.Initialize(this);
        }
    }
}
