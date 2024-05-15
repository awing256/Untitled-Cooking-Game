using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientHandler : MonoBehaviour
{
    private InventoryScript inventory;
    public IngredientItemClass item;

    void Start(){
        inventory = GameObject.FindObjectOfType<InventoryScript>();
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Player")) {
            inventory.AddItem(item);
            Destroy(this.gameObject);
        }
    }

}
