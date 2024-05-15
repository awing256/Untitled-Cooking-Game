using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHandler : MonoBehaviour
{
    public GunWeaponClass gun;
    public string weaponName;
    public Sprite icon;
    public string description;
    public GameObject attackPoint;
    public float attackInterval;
    public GameObject bulletPrefab;
    public float bulletSpeed;
   



    void Start(){
        // weaponInventory = GameObject.FindObjectOfType<WeaponInventoryScript>();
        gun = new GunWeaponClass(weaponName, icon, description, attackPoint, attackInterval, bulletPrefab, bulletSpeed);
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Player")) {
            GameObject.FindObjectOfType<PlayerController>().weaponInventory.AddWeapon(gun);
            Destroy(this.gameObject);
        }
    }

}
