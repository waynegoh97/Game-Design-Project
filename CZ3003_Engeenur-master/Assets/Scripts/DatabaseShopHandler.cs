using System.Collections.Generic;
using FullSerializer;
using Proyecto26;


/// <summary>
/// Author : Ten simin <br/>
/// Methods to retrieve , create and delete items from the shop
/// </summary>

public class DatabaseShopHandler
{
    private static readonly string databaseURL = $"https://engeenur-17baa.firebaseio.com/";

    private static fsSerializer serializer = new fsSerializer();

    public delegate void DelItemCallback();
    public delegate void PostItemCallback();
    public delegate void PostUserCallback();
    public delegate void GetItemCallback(ShopItem shopitem);
    public delegate void GetItemsCallback(Dictionary<string, ShopItem> shopitems);
    public delegate void GetUsersCallback(Dictionary<string, UserData> userDatas);
    public delegate void GetInventoryCallback(Dictionary<string, Item> itemDict);


    /// <summary>
    /// Create an item in the Shop
    /// </summary>
    /// <param name="itemName"></param>
    /// <param name="callback"></param>
    //Create an item in Shop
    public static void PostShopItem(ShopItem shopitem, string itemName, PostItemCallback callback)
    {
        RestClient.Put<ShopItem>($"{databaseURL}shop/{itemName}.json", shopitem).Then(response => { callback(); });
    }

    /// <summary>
    /// Retrieve all the shop items
    /// </summary>
    /// <param name="callback"></param>
    //Retrieve all shop items
    public static void GetShopItems(GetItemsCallback callback)
    {
        RestClient.Get($"{databaseURL}shop.json").Then(response =>
        {
            var responseJson = response.Text;
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, ShopItem>), ref deserialized);

            var shopItems = deserialized as Dictionary<string, ShopItem>;
            callback(shopItems);
        });
    }

     /// <summary>
     /// Retrieve a shop item
     /// </summary>
     /// <param name="itemName"></param>
     /// <param name="callback"></param>

    //Retrieve a Shop Item
    public static void GetShopItem(string itemName, GetItemCallback callback)
    {
        RestClient.Get<ShopItem>($"{databaseURL}shop/{itemName}.json").Then(shopItem => { callback(shopItem); });
    }
    /// <summary>
    /// Delete a shop item
    /// </summary>
    /// <param name="itemName"></param>
    /// <param name="callback"></param>

    //Delete a shop item
    public static void DeleteShopItem(string itemName, DelItemCallback callback)
    {
        RestClient.Delete($"{databaseURL}shop/" + itemName + ".json").Then(response => { callback(); });
    }

    /// <summary>
    /// Retrieve all the user data
    /// </summary>
    /// <param name="callback"></param>
    //Retrieve all user details
    public static void GetUsers(GetUsersCallback callback)
    {
        RestClient.Get($"{databaseURL}students.json").Then(response =>
        {
            var responseJson = response.Text;
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, UserData>), ref deserialized);

            var userDatas = deserialized as Dictionary<string, UserData>;
            callback(userDatas);
        });
    }

    /// <summary>
    /// Update the user data
    /// </summary>
    /// <param name="key"></param>
    /// <param name="userData"></param>
    /// <param name="callback"></param>
     public static void PutUser(string key, UserData userData, PostUserCallback callback)
    {
        RestClient.Put($"{databaseURL}students/"+key+".json", userData).Then(response => { callback(); });
    }

    /// <summary>
    /// Get all dictionary of items the user has in his/ her inventory
    /// </summary>
    /// <param name="studentUsername"></param>
    /// <param name="callback"></param>
     public static void GetInventory(string studentUsername, GetInventoryCallback callback)
    {
        Dictionary<string, Item> itemsDict;
        uint i = 0;

        // ref: https://stackoverflow.com/questions/42315302/specify-condition-and-limit-for-firebase-realtime-database-request 
        // example: https://engeenur-17baa.firebaseio.com/inventory.json?orderBy=%22studentUsername%22&equalTo=%22jack%22
        RestClient.Get($"{databaseURL}inventory.json?orderBy=%22studentUsername%22&equalTo=%22{studentUsername}%22").Then(res =>
        {
            // parsing JSON into Item object
            var responseJson = res.Text;
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, Item>), ref deserialized);

            itemsDict = deserialized as Dictionary<string, Item>;
            if (itemsDict == null)
                itemsDict = new Dictionary<string, Item>();

            // to get the the type of each items from shop based on the item name
            foreach (string key in itemsDict.Keys)
            {
                Item item = itemsDict[key];
                RestClient.Get($"{databaseURL}shop/{item.name}/property.json").Then(res1 =>
                {
                    // without using serializer, res1.Text will include '\"' on both ends of the string so need to Trim them away
                    item.property = res1.Text.Trim('\"');

                    // check if retrieved type for last item before returning back to InventoryViewModel
                    if (++i == itemsDict.Count)
                        callback(itemsDict);
                });
            }
        });
    }
}
