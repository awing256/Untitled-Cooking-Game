using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAim
{
    private Vector2 mousePos;
    private float aimAngle, aimDistance;
    private Transform playerTransform, attackPointTransform, reticleTransform;
    private PlayerController pc;


    public PlayerAim(float aimDistance, Transform playerTransform, Transform attackPointTransform, Transform reticleTransform,
     PlayerController pc){
        this.aimDistance = aimDistance;
        this.playerTransform = playerTransform;
        this.attackPointTransform = attackPointTransform;
        this.reticleTransform = reticleTransform;
        this.pc = pc;
    }

    public float DoAim(Vector2 input){
        mousePos = Camera.main.ScreenToWorldPoint(input);
        reticleTransform.position = mousePos;

        aimAngle = Mathf.Atan2((mousePos.y - playerTransform.position.y), (mousePos.x - playerTransform.position.x)) * Mathf.Rad2Deg;

        attackPointTransform.position = playerTransform.position + ((Quaternion.Euler(0, 0, aimAngle) * (playerTransform.right * aimDistance)));
        attackPointTransform.rotation = Quaternion.Euler(0, 0, aimAngle);

        return aimAngle;
    }


}
