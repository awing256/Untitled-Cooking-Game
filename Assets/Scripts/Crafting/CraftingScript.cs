using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CraftingScript : MonoBehaviour
{
    public InventoryScript inventory;
    public List<DishItemClass> dishes;
    private int selectedDish;
    [SerializeField] private GameObject timingCheck;
    public Text craftingText;
    private Slider QTESlider;
    private InputAction craft;
    private InputAction selectCraft;
    private InputAction startCraft;
    private bool isCrafting = false;
    private DishItemClass currentDish;
    private float correctVal, range;

    void Start(){
        inventory = GameObject.FindObjectOfType<InventoryScript>();
        // dishes = (List<DishItemClass>) Resources.LoadAll("Items/Dishes");



        // foreach (DishItemClass dish in dishes){
            
        // }

        selectedDish = 0;
        UpdateCraftingText();

        correctVal = .75f;
        range = .1f;

        QTESlider = timingCheck.GetComponent<Slider>();
    }

    public void Initialize(InputAction craft, InputAction selectCraft, InputAction startCraft) {
        this.craft = craft;
        this.craft.Enable();
        this.selectCraft = selectCraft;
        this.selectCraft.Enable();
        this.startCraft = startCraft;
        this.startCraft.Enable();
    }

    void Update(){

        startCraft.performed += _ => {
            if (!isCrafting){
                StartCrafting(dishes[selectedDish]);
            }
        };

        selectCraft.performed += _ =>{
            SelectDish((int)selectCraft.ReadValue<float>());
        };

        if (isCrafting && QTESlider.value < 1){
            craft.performed += _ => {
                CheckQTE();
            };
            QTESlider.value += .003f;

            if (QTESlider.value >= 1){
                CheckQTE();
            }
        }


    }

    private void CheckQTE(){
        if (Mathf.Abs(QTESlider.value - correctVal) < range){
            CraftDish(currentDish);
        }else{
            Debug.Log("FAIL");
        }

        QTESlider.value = 0;
        isCrafting = false;
        timingCheck.SetActive(false);

    }

    public void SelectDish(int direction){
        //change index of crafting recipes, loops if <0
        selectedDish = (selectedDish + direction + dishes.Count) % dishes.Count;

        UpdateCraftingText();
    }

    private void StartCrafting(DishItemClass dish){
        isCrafting = CheckCraftable(dish);

        if (!isCrafting){
            Debug.Log("Cannot craft this");
        }else{
            currentDish = dish;
            timingCheck.SetActive(true);
        }
    }

    public void CraftDish(DishItemClass dish){
        foreach (KeyValuePair<string, int> ingredient in dish.recipe){
            inventory.ChangeItemQuantity(ingredient.Key, -1 * ingredient.Value);
        }
        inventory.AddItem(dish);
    }

    //returns true if dish can be made
    private bool CheckCraftable(DishItemClass dish){
        if (dish == null) {return false;}
        foreach (KeyValuePair<string, int> ingredient in dish.recipe){
            if (inventory.GetCount(ingredient.Key) < ingredient.Value){
                return false;
            }
        }
        return true;
    }

    private void UpdateCraftingText()
    {
        if (craftingText != null)
        {
            craftingText.text = "<  Crafting: " + dishes[selectedDish].itemName + "   >\n";
            foreach (KeyValuePair<string, int> ingredient in dishes[selectedDish].recipe){
                craftingText.text += ingredient.Key + ": " + ingredient.Value + "\n";
            }
        }
    }

}
