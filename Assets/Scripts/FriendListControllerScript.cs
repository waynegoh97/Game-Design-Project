using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// <br>Author: Heng Fuwei, Esmond</br> 
/// 
/// </summary>
public class FriendListControllerScript : MonoBehaviour
{
    public string theName;
    public GameObject inputField;
    public GameObject textDisplay;
    HashSet<string> names = new HashSet<string>();
    public void StoreName()
    {
        theName = inputField.GetComponent<Text>().text;

        names.Add(theName);

        textDisplay.GetComponent<Text>().text = string.Join("\n", names);
    }
    public void DeleteName()
    {
        theName = inputField.GetComponent<Text>().text;
        names.Remove(theName);

        textDisplay.GetComponent<Text>().text = string.Join("\n", names);
    }

}
