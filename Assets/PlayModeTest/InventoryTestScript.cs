using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using System.Runtime.ExceptionServices;
using UnityEngine.SceneManagement;
using System;

namespace Tests
{
    /// <summary>
    /// Author: Ang Hao Jie <br/>
    /// Test cases for Inventory UI.
    /// </summary>
    public class InventoryTestScript
    {
        /// <summary>
        /// For test case TC14 Equip weapon.
        /// </summary>
        [Test]
        public void TC14EquipWeaponTest()
        {
            UserData userData = new UserData();
            userData.userName = "peter";
            GameObject inventoryGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/InventoryCanvas"));
            InventoryViewModel inventoryViewModel = inventoryGameObject.transform.Find("InventoryUI").Find("Inbag ScrollView").Find("Viewport").Find("In-bagContent").GetComponent<InventoryViewModel>();
            inventoryViewModel.setUserData(userData);

            // create only one item to be stored in the inventory
            Item inBagWeapon = new Item();
            inBagWeapon.name = "Iron Sword";
            inBagWeapon.studentUsername = "peter";
            inBagWeapon.quantity = 1;
            inBagWeapon.property = Item.WEAPON;

            // set one item which is "Iron Sword" to inventory items
            Dictionary<string, Item> inBagList = new Dictionary<string, Item>();
            inBagList.Add(inBagWeapon.studentUsername + inBagWeapon.name, inBagWeapon);
            inventoryViewModel.setInBagList(inBagList);

            // generate an item to be equipped which is "Bronze Dagger"
            Item equippedWeapon = new Item();
            equippedWeapon.name = "Bronze Dagger";
            equippedWeapon.studentUsername = "peter";
            equippedWeapon.quantity = 1;
            equippedWeapon.property = Item.WEAPON;

            EquippedItems equippedItems = new EquippedItems();
            equippedItems.weapon = equippedWeapon;
            inventoryViewModel.setEquippedItems(equippedItems);

            // stimulate click on the first item as there are only one item in the inventory and check if it is successfully equipped
            inventoryViewModel.onInBagItemClick(inventoryViewModel.getInstantiatedUI()[0]);
            // check if iron sword is now added as equipped item
            Assert.AreEqual("Iron Sword", inventoryViewModel.equippedWeapon.GetComponent<Text>().text); ;
            // check if iron sword is removed from the inventory
            Assert.AreNotEqual("Iron Sword", inventoryViewModel.getInstantiatedUI()[0].transform.GetChild(0).GetComponent<Text>().text);
        }

        /// <summary>
        /// For test case TC15 Equip potion.
        /// </summary>
        [Test]
        public void TC15EquipPotionTest()
        {
            UserData userData = new UserData();
            userData.userName = "peter";
            GameObject inventoryGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/InventoryCanvas"));
            InventoryViewModel inventoryViewModel = inventoryGameObject.transform.Find("InventoryUI").Find("Inbag ScrollView").Find("Viewport").Find("In-bagContent").GetComponent<InventoryViewModel>();
            inventoryViewModel.setUserData(userData);

            // create only one item to be stored in the inventory
            Item inBagWeapon = new Item();
            inBagWeapon.name = "Healing Potion";
            inBagWeapon.studentUsername = "peter";
            inBagWeapon.quantity = 1;
            inBagWeapon.property = Item.HEALINGPOTION;

            // set one item which is "Healing Potion" to inventory items
            Dictionary<string, Item> inBagList = new Dictionary<string, Item>();
            inBagList.Add(inBagWeapon.studentUsername + inBagWeapon.name, inBagWeapon);
            inventoryViewModel.setInBagList(inBagList);

            // generate an item to be equipped which is "Bronze Dagger"
            Item equippedWeapon = new Item();
            equippedWeapon.name = "Bronze Dagger";
            equippedWeapon.studentUsername = "peter";
            equippedWeapon.quantity = 1;
            equippedWeapon.property = Item.WEAPON;

            EquippedItems equippedItems = new EquippedItems();
            equippedItems.weapon = equippedWeapon;
            inventoryViewModel.setEquippedItems(equippedItems);

            // stimulate click on the first item as there are only one item in the inventory and check if it is fails to be equipped
            inventoryViewModel.onInBagItemClick(inventoryViewModel.getInstantiatedUI()[0]);
            // check if "Healing Potion" is not being equipped as a weapon
            Assert.AreNotEqual("Healing Potion", inventoryViewModel.equippedWeapon.GetComponent<Text>().text); ;
            // check if "Healing Potion" is still in the inventory
            Assert.AreEqual("Healing Potion", inventoryViewModel.getInstantiatedUI()[0].transform.GetChild(0).GetComponent<Text>().text);
        }
    }
}
