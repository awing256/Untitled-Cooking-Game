using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHandler : MonoBehaviour
{
    public MeleeWeaponClass weapon;
    public string weaponName;
    public Sprite icon;
    public string description;
    public GameObject attackPoint;
    public GameObject player;
    public float attackInterval;
    public GameObject weaponPrefab;
    public float swingAmount;
   



    void Start(){
        // weaponInventory = GameObject.FindObjectOfType<WeaponInventoryScript>();
        weapon = new MeleeWeaponClass(weaponName, icon, description, attackPoint, attackInterval, weaponPrefab, swingAmount);
    }

    // void OnTriggerEnter2D(Collider2D other){
    //     if (other.gameObject.CompareTag("Player")) {
    //         GameObject.FindObjectOfType<PlayerController>().weaponInventory.AddWeapon(weapon);
    //         Destroy(this.gameObject);
    //     }
    // }

}
