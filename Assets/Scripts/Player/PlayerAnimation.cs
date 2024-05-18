using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimation
{
    private Animator animator;
    private SpriteRenderer playerSR, attackPointSR;
    private PlayerScript ps;
    private bool isFlipped;
    


    public PlayerAnimation(Animator animator, SpriteRenderer playerSR, SpriteRenderer attackPointSR, PlayerScript ps){
        this.animator = animator;
        this.playerSR = playerSR; 
        this.attackPointSR = attackPointSR;
        this.ps = ps;

        ps.onStateChanged += OnStateChanged;

    }

    public void CheckFlip(float aimAngle){
        isFlipped = !(aimAngle < 90 && aimAngle > -90);
        playerSR.flipX = isFlipped;
        attackPointSR.flipY = isFlipped;
    }

    private void OnStateChanged(PlayerScript.PlayerState newState){
        animator.Play(newState.ToString());
    }

    public void OnWeaponChanged(WeaponClass newWeapon){
        attackPointSR.sprite = newWeapon.icon;
    }

}
