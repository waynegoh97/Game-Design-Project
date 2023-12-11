using System.Collections.Generic;
using System;

/// <summary>
/// Author: Ang Hao Jie <br/>
/// InventoryItems Model.
/// </summary>
[Serializable]
public class InventoryItems
{
    public List<Item> items { get; set; }
}