using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI; //To link function to unity UI
using Proyecto26; //RestClient lib
using System.Runtime.InteropServices;


public class Retrieve : MonoBehaviour
{
    public Text dataText;
    public Text stageText;
    string stage;
    //Question question = new Question();
    List<Question> question = new List<Question>();
    //Stage question = new Stage();
    // Start is called before the first frame update
    void Start()
    {
        
        //dataText.text = question.Questions;

    }

    
    //Get stage level from text within button
     private string GetStageValue()
     {
         stage = stageText.text;
         return stage;
     }

     //Get Data
     public void GetFromDatabase()
     {
         GetStageValue();
        for (int i = 1; i<=2; i++)
        {
            string quest = "Q" +i;
            RestClient.Get<Question>("https://my-project-1475569765373.firebaseio.com/QuestionData/"+ stage + quest + ".json" ).Then(response =>
            {

                question.Add(response);
            });
        }
        Debug.Log("Question 1" + question[0].Questions);
        Debug.Log("Answer 1 " + question[0].Answer);
        Debug.Log("Question 2" + question[1].Questions);
        UpdateStage();
        
        /*RestClient.Get<Question>("https://my-project-1475569765373.firebaseio.com/QuestionData/" + stage +".json").Then(response =>
        {

            question.Add(response);
            UpdateStage();
        });*/
    }

     private void UpdateStage()
     {
        dataText.text = question[1].Questions;
         //dataText.text = question.QuestionSelect.Answer.ToString();
     }
    // Update is called once per frame
    void Update()
    {
        
    }
}
