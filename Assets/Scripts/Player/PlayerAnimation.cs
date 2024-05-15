using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimation
{
    private Animator animator;
    private SpriteRenderer playerSR, attackPointSR;
    private PlayerController pc;

    private bool isFlipped;
    


    public PlayerAnimation(Animator animator, SpriteRenderer playerSR, SpriteRenderer attackPointSR, PlayerController pc){
        this.animator = animator;
        this.playerSR = playerSR; 
        this.attackPointSR = attackPointSR;
        this.pc = pc;

        pc.onStateChanged += OnStateChanged;

    }

    public void CheckFlip(float aimAngle){
        isFlipped = !(aimAngle < 90 && aimAngle > -90);
        playerSR.flipX = isFlipped;
        attackPointSR.flipY = isFlipped;
    }

    private void OnStateChanged(PlayerController.PlayerState newState){
        animator.Play(newState.ToString());
    }

    public void OnWeaponChanged(WeaponClass newWeapon){
        attackPointSR.sprite = newWeapon.icon;
    }

}
