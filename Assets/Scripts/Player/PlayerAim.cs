using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerAim
{
    private Vector2 mousePos;
    private float aimAngle, aimDistance;
    private Transform playerTransform, attackPointTransform, reticleTransform;
    private PlayerScript ps;
    private InputAction aim;


    public PlayerAim(float aimDistance, Transform playerTransform, Transform attackPointTransform, Transform reticleTransform,
     PlayerScript ps){
        this.aimDistance = aimDistance;
        this.playerTransform = playerTransform;
        this.attackPointTransform = attackPointTransform;
        this.reticleTransform = reticleTransform;
        this.ps = ps;
    }

    public void SetInput(InputAction aim){
        this.aim = aim;
        this.aim.Enable();
    }

    public float DoAim(){
        
        mousePos = Camera.main.ScreenToWorldPoint(aim.ReadValue<Vector2>());
        reticleTransform.position = mousePos;

        aimAngle = Mathf.Atan2((mousePos.y - playerTransform.position.y), (mousePos.x - playerTransform.position.x)) * Mathf.Rad2Deg;

        attackPointTransform.position = playerTransform.position + ((Quaternion.Euler(0, 0, aimAngle) * (playerTransform.right * aimDistance)));
        attackPointTransform.rotation = Quaternion.Euler(0, 0, aimAngle);

        return aimAngle;
    }


}
