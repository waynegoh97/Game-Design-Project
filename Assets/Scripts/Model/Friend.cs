using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <br>Author: Heng Fuwei, Esmond</br> 
/// Friend Model.
/// </summary>
public class Friend
{
    public string userName;
    public List<string> students;

    /// <summary>
    /// Constructor for Friend.
    /// </summary>
    /// <param name="userName">Username of the user.</param>
    /// <param name="studsList">List of friends of the user.</param>
    public Friend(string userName, List<string> studsList)
    {
        this.userName = userName;
        students = new List<string>(studsList);
    }
}
