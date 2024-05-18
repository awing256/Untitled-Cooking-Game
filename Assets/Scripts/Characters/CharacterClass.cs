using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterClass
{

    public string characterName;
    public Sprite icon;
    public string description;
    public GameObject player;
    public PlayerScript playerScript;
    public PlayerController playerController;
    public bool canAttack = true;
    public CharacterClass(){
        
    }

    public abstract void PrimaryAttack();
    public abstract void SecondaryAttack();
    public abstract void UseUtility();
    public abstract void UseSpecial();
    public abstract void UseUltimate();
    public abstract void Interact();
}
