using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Proyecto26; //RestClient API
using FullSerializer; // External Library, drag the source folder in

/// <summary>
/// This class is used to set the available stages players can access. This can be seen from the score history of stages attempted by players.
/// </summary>
public class Disable : MonoBehaviour
{
    private Dictionary<string, ScoreData> stgComplete = new Dictionary<string, ScoreData>();
    public static fsSerializer serializer = new fsSerializer();
    public MainMenuControllerScript mainMenuControllerScript;
    public UserData userData;

    //Database link
    string database = "https://engeenur-17baa.firebaseio.com/";

    //Get levels from database to disable stage
    public void getStg(int level)
    {
        for (int i = 10; i > 0; i--)
        {
            setEnable(i);
        }

        for (int i = 10; i > level; i--)
        {
            setDisable(i);
        }
    }
    public void setEnable(int lvl)
    {
        Button myBtn = GameObject.Find("Stg" + lvl).GetComponent<Button>();
        myBtn.interactable = true;
    }


    //Set button to disable mode
    public void setDisable(int lvl)
    {
        Button myBtn = GameObject.Find("Stg"+lvl).GetComponent<Button>();
        myBtn.interactable = false;
    }

    //Retrieve stage completed from database
    private IEnumerator GetStageCompleted()
    {
        RestClient.Get(database + "score.json").Then(response =>
        {
            userData = mainMenuControllerScript.getUserData();
            fsData questionData = fsJsonParser.Parse(response.Text);
            serializer.TryDeserialize(questionData, ref stgComplete);
            getStg(stgComplete[userData.userName].levelScore.Count);
        });

        yield return null;
    }



    void OnEnable()
    {

        StartCoroutine(GetStageCompleted());

    }


  

   
}
