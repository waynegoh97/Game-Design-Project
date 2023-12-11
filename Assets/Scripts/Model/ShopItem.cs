using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Ten Si Min <br/>
/// ShopItem Model.
/// </summary>
[Serializable]
public class ShopItem
{
    public int cost;
    public string property;
    public int no;
    public string description;

    /// <summary>
    /// Constructor for ShopItem.
    /// </summary>
    /// <param name="cost">Cost of the item.</param>
    /// <param name="property">Category of the item.</param>
    /// <param name="no">Number of item.</param>
    /// <param name="description">Description of the item.</param>
    public ShopItem(int cost, string property, int no, string description) {
        this.cost = cost;
        this.property = property;
        this.no = no;
        this.description = description;
    }
}
