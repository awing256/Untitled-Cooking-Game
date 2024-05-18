using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButcherClass : CharacterClass
{

    public ButcherClass(){
        this.characterName = "Butcher";
    }

    public override void PrimaryAttack(){
        Debug.Log("Primary!");
    }
    public override void SecondaryAttack(){
        Debug.Log("Secondary!");
    }
    public override void UseUtility(){
        Debug.Log("Util!");
    }
    public override void UseSpecial(){
        Debug.Log("Special!");
    }
    public override void UseUltimate(){
        Debug.Log("Ultimate!");
    }
    public override void Interact(){
        Debug.Log("Interact!");
    }
}
