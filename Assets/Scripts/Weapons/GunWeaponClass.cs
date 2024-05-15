using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWeaponClass : WeaponClass
{
    public GameObject bulletPrefab;
    public float bulletSpeed;

    
    public GunWeaponClass(string itemName, Sprite icon, string description, GameObject attackPoint, float attackInterval, GameObject bulletPrefab, float bulletSpeed)
    :base(itemName, icon, description, attackPoint, attackInterval){
        this.bulletPrefab = bulletPrefab;
        this.bulletSpeed = bulletSpeed;

    }

    public override void WeaponAttack(){
        if(canAttack){
            GameObject bullet = Object.Instantiate(bulletPrefab, attackPoint.transform.position, Quaternion.identity);
            bullet.transform.right = Quaternion.Euler(0, 0, playerController.aimAngle) * player.transform.right;
            bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * bulletSpeed;
        }
    }

}
