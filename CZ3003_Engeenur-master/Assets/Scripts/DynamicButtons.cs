using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26; //RestClient API
using FullSerializer;
using System;
using System.Globalization;

/// <summary>
/// This class is used to set the number of stages that students can select based on the number of stages created by teachers in the special stage.
/// </summary>
public class DynamicButtons : MonoBehaviour
{
    int count = 0;

    //Set button invisible for special stages
    public void SetBtnInvisible(int lvl)
    {

        for(int k = 10; k > 10-count; k--)
        {
            Button myBtn = GameObject.Find("Stg" + k).GetComponent<Button>();
            myBtn.interactable = true;
            myBtn.image.color = new Color(1f, 1f, 1f, 1f);
            myBtn.GetComponentInChildren<Text>().color = new Color(0.6132076f, 0.3401352f, 0.06652725f, 1f);
        }

        count = 0;

        for (int i = 10; i > lvl; i--)
        {
            Button myBtn = GameObject.Find("Stg" + i).GetComponent<Button>();
            myBtn.interactable = false;
            myBtn.image.color = new Color(0f, 0f, 0f, 0f);
            myBtn.GetComponentInChildren<Text>().color = new Color(0f, 0f, 0f, 0f);
            count++;
        }
        

    }




}
