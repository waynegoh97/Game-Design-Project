using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26; //RestClient API
using FullSerializer; // External Library, drag the source folder in
using System;
using System.Globalization;
using System.Threading.Tasks;

/// <summary>
/// This class will retrieve normal stages data from the database.
/// Data will be stored and passed to the Battle System script.
/// Stage information will be passed according to the selected stage number.
/// </summary>
public class GetDatabaseQuestions : MonoBehaviour
{
    //for battle system retrieval
    public List<String> question;
    public List<int> answer;
    public Dictionary<string, int> monster;
    public int levelNo;


    public Dictionary<string, StageQuestion> ques = new Dictionary<string, StageQuestion>();
    private Dictionary<string, MonsterData> mons = new Dictionary<string, MonsterData>();
    private Dictionary<string, CourseData> course = new Dictionary<string, CourseData>();
    private string ts;
    private int stgAvail;
    private List<String> key;


    public Button get1;
    public Button get2;
    public Button get3;
    public Button get4;
    public Button get5;
    public Button get6;
    public Button get7;
    public Button get8;
    public Button get9;
    public Button get10;

    public static fsSerializer serializer = new fsSerializer();

    //Database link
    string database = "https://engeenur-17baa.firebaseio.com/";

    //count number of questions in a stage. Input stage: e.g. 1 for stage 1
    private int CountKey(int n)
    {
        int key = 0;
        int i = 1;
    
        while (ques.ContainsKey("N" + n + "Q" + i))
        {
            i++;
            key++;
        }
        return key;
    }



    //Get all Questions from database
    private IEnumerator GetStageQuestions()
    {

        RestClient.Get(database + "QuestionData.json").Then(response =>
        {
           

                fsData questionData = fsJsonParser.Parse(response.Text);
                serializer.TryDeserialize(questionData, ref ques);
                //callback(ques);
          

        });

        yield return null;
    }

    //For monster data, only uncomment when database is done
     private IEnumerator GetMonsterData()
     {
         RestClient.Get(database + "monster.json").Then(response =>
         {


             fsData monsterData = fsJsonParser.Parse(response.Text);
             serializer.TryDeserialize(monsterData, ref mons);
       
         });
         yield return null;
     }

    //Store monster data from into dictionary to pass data
    private void MonsData(int stage)
    {
        monster = new Dictionary<string, int>();
        monster.Add("attack", mons["S" + stage.ToString()].attack);
        monster.Add("health", mons["S"+ stage.ToString()].health);
        monster.Add("coin", mons["S" + stage.ToString()].coin);
        monster.Add("experience", mons["S" + stage.ToString()].experience);
        Debug.Log("Get data " + monster["attack"]);

    }


    //OnClickListener Events, update question and answer list according to stage selected
    private void GetStageSelected(string course, int stage)
    {
        levelNo = stage;
        switch (stage)
        {
            case 1:
                question.Clear();
                answer.Clear();
                for (int i = 1; i <= CountKey(stage); i++)
                {
                    question.Add(ques[course + "Q" + i.ToString()].Questions);
                    answer.Add(ques[course + "Q" + i.ToString()].Answer);
                }
                Debug.Log("Ques " + question[0]);
                MonsData(stage);
                break;

            case 2:
                question.Clear();
                answer.Clear();
                for (int i = 1; i <= CountKey(stage); i++)
                {
                    question.Add(ques[course + "Q" + i.ToString()].Questions);
                    answer.Add(ques[course + "Q" + i.ToString()].Answer);
                }
                Debug.Log("Ques " + question[0]);
                MonsData(stage);
                break;

            case 3:
                question.Clear();
                answer.Clear();
                for (int i = 1; i <= CountKey(stage); i++)
                {
                    question.Add(ques[course + "Q" + i.ToString()].Questions);
                    answer.Add(ques[course + "Q" + i.ToString()].Answer);
                }
                Debug.Log("Ques " + question[0]);
                MonsData(stage);
                break;
            case 4:
                question.Clear();
                answer.Clear();
                for (int i = 1; i <= CountKey(stage); i++)
                {
                    question.Add(ques[course + "Q" + i.ToString()].Questions);
                    answer.Add(ques[course + "Q" + i.ToString()].Answer);
                }
                Debug.Log("Ques " + question[0]);
                MonsData(stage);
                break;

            case 5:
                question.Clear();
                answer.Clear();
                for (int i = 1; i <= CountKey(stage); i++)
                {
                    question.Add(ques[course + "Q" + i.ToString()].Questions);
                    answer.Add(ques[course + "Q" + i.ToString()].Answer);
                }
                Debug.Log("Ques " + question[0]);
                MonsData(stage);
                break;

            case 6:
                question.Clear();
                answer.Clear();
                for (int i = 1; i <= CountKey(stage); i++)
                {
                    question.Add(ques[course + "Q" + i.ToString()].Questions);
                    answer.Add(ques[course + "Q" + i.ToString()].Answer);
                }
                Debug.Log("Ques " + question[0]);
                MonsData(stage);
                break;

            case 7:
                question.Clear();
                answer.Clear();
                for (int i = 1; i <= CountKey(stage); i++)
                {
                    question.Add(ques[course + "Q" + i.ToString()].Questions);
                    answer.Add(ques[course + "Q" + i.ToString()].Answer);
                }
                Debug.Log("Ques " + question[0]);
                MonsData(stage);
                break;

            case 8:
                question.Clear();
                answer.Clear();
                for (int i = 1; i <= CountKey(stage); i++)
                {
                    question.Add(ques[course + "Q" + i.ToString()].Questions);
                    answer.Add(ques[course + "Q" + i.ToString()].Answer);
                }
                Debug.Log("Ques " + question[0]);
                MonsData(stage);
                break;

            case 9:
                question.Clear();
                answer.Clear();
                for (int i = 1; i <= CountKey(stage); i++)
                {
                    question.Add(ques[course + "Q" + i.ToString()].Questions);
                    answer.Add(ques[course + "Q" + i.ToString()].Answer);
                }
                Debug.Log("Ques " + question[0]);
                MonsData(stage);
                break;

            case 10:
                question.Clear();
                answer.Clear();
                for (int i = 1; i <= CountKey(stage); i++)
                {
                    question.Add(ques[course + "Q" + i.ToString()].Questions);
                    answer.Add(ques[course + "Q" + i.ToString()].Answer);
                }
                Debug.Log("Ques " + question[0]);
                MonsData(stage);
                break;
        }
    }

        void Start()
    {
 
        get1.onClick.AddListener(delegate { GetStageSelected("N1", 1); });
        get2.onClick.AddListener(delegate { GetStageSelected("N2", 2); });
        get3.onClick.AddListener(delegate { GetStageSelected("N3", 3); });
        get4.onClick.AddListener(delegate { GetStageSelected("N4", 4); });
        get5.onClick.AddListener(delegate { GetStageSelected("N5", 5); });
        get6.onClick.AddListener(delegate { GetStageSelected("N6", 6); });
        get7.onClick.AddListener(delegate { GetStageSelected("N7", 7); });
        get8.onClick.AddListener(delegate { GetStageSelected("N8", 8); });
        get9.onClick.AddListener(delegate { GetStageSelected("N9", 9); });
        get10.onClick.AddListener(delegate { GetStageSelected("N10", 10); });
    }

    void Awake()
    {
        
        StartCoroutine(GetStageQuestions());
        StartCoroutine(GetMonsterData());
    }

}
