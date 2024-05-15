using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Dish", menuName = "Items/Dish", order = 1)]
public class DishItemClass : ItemClass
{
    
    public Dictionary<string, int> recipe;
    public List<string> ingredients;
    public List<int> amounts;

}
