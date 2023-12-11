using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityWeld.Binding;
using System.ComponentModel;
using System;

/// <summary>
/// Author: Ang Hao Jie <br/>
/// Controller class for Inventory UI
/// </summary>
[Binding]
public class InventoryViewModel : MonoBehaviour, INotifyPropertyChanged
{
    public InventoryInterface inventoryInt;
    public MainMenuControllerScript mainMenuControllerScript;
    private UserData userData;
    private EquippedItems equippedItems = new EquippedItems();
    private Dictionary<string, Item> inBagList;
    private List<Button> instantiatedUI = new List<Button>();
    public event PropertyChangedEventHandler PropertyChanged;
    public GameObject goldAmount;
    public GameObject equippedWeapon;

    public Button inbagRowTemplate;

    /// <summary>
    /// Executes when the canvas is loaded.
    /// </summary>
    void Start()
    {

    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {

    }

    /// <summary>
    /// Executes whenever user navigates to the Inventory page.
    /// </summary>
    void OnEnable() 
    {
        Init();
    }

    /// <summary>
    /// Fetch required data from database and set the values on the UI.
    /// </summary>
    public void Init() 
    {
        // StartCoroutine(inventoryInt.getInventoryDetails((success, allResults) => {
        //     if (success) 
        //     {
        //         this.inBagList = allResults;
        //         this.populateInBagItems();
        //     }
        // }));

        // StartCoroutine(inventoryInt.getEquippedItemsDetails((success, allResults) => {
        //     if (success) 
        //     {
        //         this.equippedItems = allResults;
        //     }
        // }));
        
        // get userdata from mainmenu
        this.userData = this.mainMenuControllerScript.getUserData();

        // incase server have issues and never fetch the user data entity
        if (this.userData == null)
            return;

        // get items list
        InventoryDBHandler.GetInventory(userData.getName(), itemsDict => {
            // set to local dictionary of items in inventory
            this.inBagList = itemsDict;
            // display list of items in inventory on the UI
            this.populateInBagItems();
        });

        InventoryDBHandler.GetEquippedItem(userData.getName(), equippedObj => {
            // set to local object of equipped items
            this.equippedItems = equippedObj;

            // set weapon name on the UI
            this.equippedWeapon.GetComponent<Text>().text = equippedObj.weapon.name;
        });

        // set the gold amount to the UI
        goldAmount.GetComponent<Text>().text = this.userData.getCoin().ToString();
    }

    /// <summary>
    /// Display rows of user owned items on the inventory on the UI.
    /// </summary>
    private void populateInBagItems() 
    {
        // Clear Results from previously selected level
        foreach (Button item in instantiatedUI) 
        {
            // can only destroy if is a gameObject so get button's gameObject
            DestroyImmediate(item.gameObject);
        }
        instantiatedUI.Clear();

        // Instantiate new results
        foreach (KeyValuePair<string, Item> item in this.inBagList) 
        {
            // don't need to generate empty items on the UI
            // still need to keep those QTY=0 as we need to know what are they to delete them from the Database
            if (item.Value.quantity != 0)
            {
                Button inbagRow = Instantiate<Button>(this.inbagRowTemplate, transform);
                inbagRow.gameObject.SetActive(true);
                inbagRow.name = item.Value.name;  // set name of the button
                inbagRow.transform.GetChild(0).GetComponent<Text>().text = item.Value.name;   // set the item name to the row
                inbagRow.transform.GetChild(0).GetComponent<Text>().fontStyle = 0; // set to unbold
                inbagRow.transform.GetChild(1).GetComponent<Text>().text = item.Value.quantity.ToString();    // set the quantity of the item to the row
                inbagRow.transform.GetChild(1).GetComponent<Text>().fontStyle = 0; // set to unbold
                // add button listener to know which item is being clicked
                inbagRow.onClick.AddListener(delegate
                    {
                        onInBagItemClick(inbagRow);
                    });
                instantiatedUI.Add(inbagRow);
            }
        }
    }

    /// <summary>
    /// Triggers when user select which item to equip from the list of items in inventory.
    /// </summary>
    /// <param name="itemClicked">The button object that was clicked by the user.</param>
    public void onInBagItemClick(Button itemClicked)
    {
        bool toAppendItem = true;
        Item previousEquippedItem = this.equippedItems.weapon;
        Item item;

        // search for the item that is being clicked
        foreach (string key in this.inBagList.Keys)
        {
            item = this.inBagList[key];

            if (item.name.Equals(itemClicked.name))
            {
                // check if clicked item to be equipped is a weapon as can only equip weaponm not potions
                if (item.property.Equals(Item.WEAPON))
                {
                    Debug.Log("Equipping item...");
                    // update the newly equipped weapon to the UI
                    this.equippedWeapon.GetComponent<Text>().text = item.name;
                    // set Model of EquippedItems to the newly equipped item
                    equippedItems.weapon = item;
                    
                    // remove item from the inventory since quantity of it becomes 0 after adding it to the item list
                    // if (item.quantity - 1 == 0)
                    //     this.inBagList.Remove(key);
                    // // reduce quantity of that equipped item from the inventory
                    // else
                        item.quantity -= 1;

                    // found the item so don't need to continue to search
                    break;
                }
                else
                    // can exit loop because only can equip weapons, not any of the potions.
                    return;
            }
        }

        // just incase there isn't anything
        if (previousEquippedItem != null)
        {
            // search item in inventory that contains the same item as the previously equipped item so can increase its quantity in the inventory
            foreach (string key in this.inBagList.Keys)
            {
                item = this.inBagList[key];

                // check if found the inventory item to increase the quantity
                if (item.name.Equals(previousEquippedItem.name))
                {
                    // increment item's quantity by 1 since adding previous equipped item back into the inventory
                    ++item.quantity;
                    // since increased the quantity, no need to append the item to the inventory list
                    toAppendItem = false;

                    // can return since no more task
                    break;
                }
            }

            // check if need to append item into the inventory list
            if (toAppendItem)
                // append previous equipped item since inventory doesn't have that item
                this.inBagList.Add(userData.getName() + previousEquippedItem.name, previousEquippedItem);
        }

        // update the UI in the list of items in the inventory
        this.populateInBagItems();
        InventoryDBHandler.PutEquippedItem(userData.getName(), this.equippedItems);
        InventoryDBHandler.PutInventory(this.inBagList);
    }

    /// <summary>
    /// Clone item by item of inventory from dictionary to dictionary.
    /// </summary>
    /// <param name="original">The dictionary object to be cloned.</param>
    /// <returns>The cloned dictionary object.</returns>
    public Dictionary<string, Item> CloneInventory(Dictionary<string, Item> original)
    {
        Dictionary<string, Item> ret = new Dictionary<string, Item>();

        // clone item by item of inventory from dictionary to dictionary
        foreach (KeyValuePair<string, Item> entry in original)
        {
            Item item = new Item();
            item.name = entry.Value.name;
            item.quantity = entry.Value.quantity;
            item.studentUsername = entry.Value.studentUsername;
            ret.Add(entry.Key, item);
        }

        return ret;
    }

    /// <summary>
    /// To get the rows of buttons of the list of items in the inventory. <br/>
    /// For test suite.
    /// </summary>
    /// <returns>Instances of the buttons of the list of items in the inventory.</returns>
    public List<Button> getInstantiatedUI()
    {
        return this.instantiatedUI;
    }

    /// <summary>
    /// To set the items that the user is equipping. <br/>
    /// For test suite.
    /// </summary>
    /// <param name="equippedItems">Items the user is currently equipping.</param>
    public void setEquippedItems(EquippedItems equippedItems)
    {
        this.equippedItems = equippedItems;
        this.equippedWeapon.GetComponent<Text>().text = equippedItems.weapon.name;
    }

    /// <summary>
    /// Set the list of items the user has. <br/>
    /// For test suite.
    /// </summary>
    /// <param name="inBagList">Dictionary of items the user has.</param>
    public void setInBagList(Dictionary<string, Item> inBagList)
    {
        this.inBagList = inBagList;
        this.populateInBagItems();
    }

    /// <summary>
    /// Set the user's data.
    /// </summary>
    /// <param name="userData">The object that contains user's basic information.</param>
    public void setUserData(UserData userData)
    {
        this.userData = userData;
    }

    /// <summary>
    /// To be triggered when there is a change in property values. <br/>
    /// For MVVM architecture.
    /// </summary>
    /// <param name="propertyName">The property name of the proprety whose value has just changed.</param>
    private void OnPropertyChanged(string propertyName)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
