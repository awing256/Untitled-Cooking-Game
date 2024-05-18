using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement
{
    private float speed, dodgeSpeed;
    private Rigidbody2D rb;
    private PlayerScript ps;

    public PlayerMovement(float speed, float dodgeSpeed, Rigidbody2D rb, PlayerScript ps){
        this.speed = speed;
        this.dodgeSpeed = dodgeSpeed;
        this.rb = rb;
        this.ps = ps;
    }

    public void DoMove(Vector2 movement){
        if (movement != Vector2.zero){
            ps.UpdateState(PlayerScript.PlayerState.Walking);
        }else{
            ps.UpdateState(PlayerScript.PlayerState.Idle);
        }
        rb.velocity = movement * speed;
    }

    public void DoDodge(){
        rb.velocity = rb.velocity.normalized * dodgeSpeed;
    }

}
