using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponClass
{
    public string weaponName;
    public Sprite icon;
    public string description;
    public GameObject attackPoint;
    public GameObject player;
    public float attackInterval;
    public bool canAttack = true;
    

    public abstract void WeaponAttack();
    public WeaponClass(string weaponName, Sprite icon, string description, GameObject attackPoint, float attackInterval){
        this.weaponName = weaponName;
        this.icon = icon;
        this.description = description;
        this.attackPoint = attackPoint;
        this.attackInterval = attackInterval;
        player = GameObject.FindWithTag("Player");
    }
}
