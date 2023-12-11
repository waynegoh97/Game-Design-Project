using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using System.Threading.Tasks;
using System.Diagnostics;
using Debug = UnityEngine.Debug;


/// <summary>
/// Author : Ten simin <br/>
/// Students can purchase items from the shop to assist him/her in the battle
/// </summary>

public class ShopViewModel : MonoBehaviour
{
    public GameObject itemParent, item, formCreate, messageBox;
    public Text coinTxt; 
    public Dictionary<string, Item> itemdict = new Dictionary<string, Item>();
    public string itemtobepurchased;
    private int buyCost;
    private int qty = 1;
    private int totalCost;
    public MainMenuControllerScript mainMenuController;
    private UserData player;

    public void OnEnable()
    {
        this.player = mainMenuController.getUserData();
        coinTxt.text = player.getCoin().ToString();
    }


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Read()); //Wait for shop item buttons to be created
        coinTxt.text = player.getCoin().ToString();
        Debug.Log("Coin Balance: "+ player.getCoin().ToString()); //Check for current coin balance
        Init();
    }
    /// <summary>
    /// method to create shopitem buttons
    /// </summary>
    IEnumerator Read() {
        int count = itemParent.transform.childCount;
        if (count != null)
        {
            for (int i = 0; i < count; i++)
            {
                Debug.Log("itemparent");
                Destroy(itemParent.transform.GetChild(i).gameObject);
            }
        }
        DatabaseShopHandler.GetShopItems(shopItems =>
        {
            foreach (var shopItem in shopItems)
            {
                Debug.Log($"{shopItem.Key} {shopItem.Value.cost} {shopItem.Value.property} {shopItem.Value.no} {shopItem.Value.description}");
                GameObject tmp_btn = Instantiate(item, itemParent.transform);
                tmp_btn.name = shopItem.Key;
                Debug.Log("item name: " + tmp_btn.name);
                tmp_btn.transform.GetChild(1).GetComponent<Text>().text = (shopItem.Key).ToString();
                tmp_btn.transform.GetChild(2).GetComponent<Text>().text = (shopItem.Value.cost).ToString();
                tmp_btn.transform.GetChild(3).GetComponent<Text>().text = (shopItem.Value.description).ToString();
            }
        });
        yield return new WaitForSeconds(0.01f);
    }

    //public async Task Read()
    //{
    //    int count = itemParent.transform.childCount;
    //    if (count != null) {
    //        for(int i=0;i < count; i++)
    //        {
    //            Debug.Log("itemparent");
    //            Destroy(itemParent.transform.GetChild(i).gameObject);
    //        }
    //    }
    //    DatabaseShopHandler.GetShopItems(shopItems =>
    //    {
    //        foreach (var shopItem in shopItems)
    //        {
    //            Debug.Log($"{shopItem.Key} {shopItem.Value.cost} {shopItem.Value.property} {shopItem.Value.no} {shopItem.Value.description}");


    //            GameObject tmp_btn = Instantiate(item, itemParent.transform);
    //            tmp_btn.name = shopItem.Key;
    //            Debug.Log("item name: " + tmp_btn.name);
    //            tmp_btn.transform.GetChild(1).GetComponent<Text>().text = (shopItem.Key).ToString();
    //            tmp_btn.transform.GetChild(2).GetComponent<Text>().text = (shopItem.Value.cost).ToString();
    //            tmp_btn.transform.GetChild(3).GetComponent<Text>().text = (shopItem.Value.description).ToString();
    //        }
    //    });
    //    Stopwatch sw = Stopwatch.StartNew();
    //    var delay = Task.Delay(200).ContinueWith(_ =>
    //                               {
    //                                   sw.Stop();
    //                                   return sw.ElapsedMilliseconds;
    //                               });
    //    await delay;
    //    int sec = (int)delay.Result;
    //    Debug.Log("Read elapsed milliseconds: {0}" + sec);
    //}

    /// <summary>
    /// Select a shop item (When a shop item is clicked)
    /// </summary>
    /// <param name="item"></param>
    public void clickShopItem(GameObject item)
    {
        formCreate.transform.GetChild(1).GetComponent<Text>().text = item.name; //Shop Item Name
        formCreate.transform.GetChild(3).GetComponent<Text>().text = "1"; //Quantity
        //Retrieve a shop item to get the name and cost
        DatabaseShopHandler.GetShopItem(item.name, shopItem =>
        {
            formCreate.transform.GetChild(6).GetComponent<Text>().text = shopItem.cost.ToString();
            buyCost = shopItem.cost;
            itemtobepurchased = item.name;
        });
    }

    /// <summary>
    /// To increase the quantity of the item when the arrow up is selected (when click arrow up)
    /// </summary>
    public void UpQty()
    {
        qty++;
        formCreate.transform.GetChild(3).GetComponent<Text>().text = qty.ToString(); //Update qty       
        totalCost = qty * buyCost;//Calculate total cost
        formCreate.transform.GetChild(6).GetComponent<Text>().text = totalCost.ToString(); // total cost
    }
    /// <summary>
    /// To decrease the quantity of the item when the arrow down is selected (When clicked qty down)
    /// </summary>
    public void DownQty()
    {
        if (qty == 1)
        {
            messageBox.SetActive(true);
            messageBox.transform.GetChild(1).GetComponent<Text>().text = "Quantity cannot be less than 1.";
        }
        else
        {
            qty--;
            formCreate.transform.GetChild(3).GetComponent<Text>().text = qty.ToString(); //Update qty
            totalCost = qty * buyCost; //Calculate total cost
            formCreate.transform.GetChild(6).GetComponent<Text>().text = totalCost.ToString(); // total cost
        }
    }
    /// <summary>
    /// When a student buy an item ,the method will check if the student have sufficient coins 
    /// if the item bought exist in the student inventory, quantity of that item will be increased.
    /// if the item bought does not exist in the student inventory, the item will be created in the inventory.
    /// </summary>
    //Buy
    public void Buy()
    {
        UserData player = mainMenuController.getUserData();
        Debug.Log("coin" + player.getCoin().ToString());
        coinTxt.text = player.getCoin().ToString();
        bool notExist = true;
        string coin = coinTxt.text;
        buyCost = buyCost * qty;
        int coinBalance = int.Parse(coin, System.Globalization.NumberStyles.Integer);
        if (coinBalance >= buyCost)
        {
            //ADD IN INVENTORY (update inventory db)
            foreach (var key in this.itemdict.Keys)
            {
                var item = this.itemdict[key];
                if (this.itemtobepurchased.Equals(item.name))
                {
                    item.quantity += (uint)this.qty;
                    notExist = false;
                    break;
                }
            }

            if (notExist)
            {
                Item newItem = new Item();
                newItem.name = this.itemtobepurchased;
                newItem.quantity = (uint)this.qty;
                newItem.studentUsername = player.userName;   
                this.itemdict.Add(player.userName + this.itemtobepurchased, newItem);
            }

            InventoryDBHandler.PutInventory(this.itemdict);
            coinBalance = coinBalance - buyCost; //calculate coin left
            player.coin = coinBalance; //update user coin balance
            DatabaseShopHandler.PutUser(player.localId, player, () => { });//Update coin in User DB
            coinTxt.text = coinBalance.ToString(); //update coin text in UI
        }
        else
        {
            messageBox.SetActive(true);
            messageBox.transform.GetChild(1).GetComponent<Text>().text = "Inefficient coins.";
        }
        buyCost = 0; //reset total purchase cost inside form create
        qty = 1; //reset qty
    }

    private void Init()
    {
        UserData player = mainMenuController.getUserData();
        // retrieve inventory details 
        DatabaseShopHandler.GetInventory(player.userName,inventoryItems =>
        {
            itemdict = inventoryItems;
            Debug.Log("ItemDict = " + itemdict.Count);
        });
    }

}
