using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Ingredient", menuName = "Items/Ingredient", order = 1)]
public class IngredientItemClass : ItemClass
{
    public enum IngredientType {
        Veggie,
        Fruit,
        Grain,
        Meat,
        Sugar
    }

    public IngredientType type;

}
