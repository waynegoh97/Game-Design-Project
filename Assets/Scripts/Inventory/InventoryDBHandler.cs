using System.Collections.Generic;
using FullSerializer;
using Proyecto26;
using UnityEngine;

/// <summary>
/// Author: Ang Hao Jie <br/>
/// Database handler for inventory to conduct CRUD.
/// </summary>
public static class InventoryDBHandler
{
    private static readonly string databaseURL = $"https://engeenur-17baa.firebaseio.com/";

    private static fsSerializer serializer = new fsSerializer();

    public delegate void GetEquippedItemCallback(EquippedItems itemsObj);
    public delegate void GetInventoryCallback(Dictionary<string, Item> itemDict);

    /// <summary>
    /// Read currently equipped item by the user from the database.
    /// </summary>
    /// <param name="studentUsername">Student's username used as the key for JSON.</param>
    /// <param name="callback">The callback method to be called after obtaining the data from database due to coroutine.</param>
    public static void GetEquippedItem(string studentUsername, GetEquippedItemCallback callback)
    {
        EquippedItems equippedItems = new EquippedItems();
        // set up instance of equipped weapon item object
        Item equippedItem = new Item();
        equippedItem.quantity = 1;
        equippedItem.studentUsername = studentUsername;

        // get weapon item into list of equipped items
        equippedItems.weapon = equippedItem;

        // get equipped item from from database
        RestClient.Get($"{databaseURL}equipment/{studentUsername}.json").Then(res => 
        {
            // without using serializer, res1.Text will include '\"' on both ends of the string so need to Trim them away
            equippedItem.name = res.Text.Trim('\"');
            

            RestClient.Get($"{databaseURL}shop/{equippedItem.name}/property.json").Then(res1 =>
            {
                // without using serializer, res1.Text will include '\"' on both ends of the string so need to Trim them away
                equippedItem.property = res1.Text.Trim('\"');

                callback(equippedItems);
            });
        });
    }

    /// <summary>
    /// Update or Create new equipped item by the user in the database.
    /// </summary>
    /// <param name="studentUsername">The username of the student as the JSON key.</param>
    /// <param name="equippedItems">The item that is newly equipped.</param>
    public static void PutEquippedItem(string studentUsername, EquippedItems equippedItems)
    {
        RestClient.Put($"{databaseURL}equipment/{studentUsername}.json", "\"" + equippedItems.weapon.name + "\"");
    }

    /// <summary>
    /// Delete item in inventory based on the item's key in the inventory in the databasse.
    /// </summary>
    /// <param name="key">Key to the JSON that is created by "student's username" + "Item name" which results in a unique key.</param>
    public static void DeleteInventory(string key)
    {
        RestClient.Delete($"{databaseURL}inventory/{key}.json");
    }

    /// <summary>
    /// Read all dictionary of items the user has in his/her inventory from the database.
    /// </summary>
    /// <param name="studentUsername">The username of the student value to filter.</param>
    /// <param name="callback">The callback method to be executed once complete fetching of data from database due to coroutine.</param>
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

    /// <summary>
    /// Create/Update each item to inventory on Database one by one in the database.
    /// </summary>
    /// <param name="itemsDict">The dictionary of items the user owns to be updated in database.</param>
    public static void PutInventory(Dictionary<string, Item> itemsDict)
    {
        foreach (KeyValuePair<string, Item> item in itemsDict)
        {
            // check if should delete those 0 quantity items from database or able to update them on database
            if (item.Value.quantity != 0)
            {
                RestClient.Put($"{databaseURL}inventory/{item.Key}/name.json", "\"" + item.Value.name + "\"");
                RestClient.Put($"{databaseURL}inventory/{item.Key}/quantity.json", item.Value.quantity.ToString());
                RestClient.Put($"{databaseURL}inventory/{item.Key}/studentUsername.json", "\"" + item.Value.studentUsername + "\"");
            }
            else
                DeleteInventory(item.Key);
        }
    }
}