using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using System;
using Proyecto26; //RestClient API
using FullSerializer;


/// <summary>
/// This class is used to select available courses assigned to students by the teachers. 
/// Students will be able to see different number of stages depending on the number of stages created by the teacher according to the course name.
/// This class will retrieve special stages data from the database.
/// When student click on the stages, it will pass question, answer, monster, courseName, and levelNo date to the Battle System script.
/// </summary>
public class CourseSelectionController : MonoBehaviour
{
    //for battle system retrieval
    public List<String> question;
    public List<int> answer;
    public Dictionary<string, int> monster; //Call using monster["attack" or "experience" or "gold" or "health"]
    public string courseName;
    public int levelNo;

    private Dictionary<string, CourseData> course = new Dictionary<string, CourseData>();
    public Dictionary<string, SpecialLevelQuestion> splevel = new Dictionary<string, SpecialLevelQuestion>();
    private Dictionary<string, MonsterData> mons = new Dictionary<string, MonsterData>();
    private List<String> myKeys;
    private List<String> key;
    private List<String> crsName;

    //Database link
    string database = "https://engeenur-17baa.firebaseio.com/";

    public static fsSerializer serializer = new fsSerializer();

    public MainMenuControllerScript mainMenuControllerScript;
    public UserData userData;

    public Dropdown dropdown;
    private DynamicButtons dbtn;

    public GameObject DefaultCanvas;
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

    
    
    //Dropdown courses
    public void Drop_IndexChanged(int index)
    {

        dbtn = FindObjectOfType(typeof(DynamicButtons)) as DynamicButtons;

        switch (index)
        {
            case 0:
                dbtn.SetBtnInvisible(StgCount(crsName[0]));
                courseName = crsName[0];
                get1.onClick.AddListener(delegate { GetStageSelected(crsName[0], 1); });
                get2.onClick.AddListener(delegate { GetStageSelected(crsName[0], 2); });
                get3.onClick.AddListener(delegate { GetStageSelected(crsName[0], 3); });
                get4.onClick.AddListener(delegate { GetStageSelected(crsName[0], 4); });
                get5.onClick.AddListener(delegate { GetStageSelected(crsName[0], 5); });
                get6.onClick.AddListener(delegate { GetStageSelected(crsName[0], 6); });
                get7.onClick.AddListener(delegate { GetStageSelected(crsName[0], 7); });
                get8.onClick.AddListener(delegate { GetStageSelected(crsName[0], 8); });
                get9.onClick.AddListener(delegate { GetStageSelected(crsName[0], 9); });
                get10.onClick.AddListener(delegate { GetStageSelected(crsName[0], 10); });
                break;
            case 1:
                dbtn.SetBtnInvisible(StgCount(crsName[1]));
                courseName = crsName[1];
                get1.onClick.AddListener(delegate { GetStageSelected(crsName[index], 1); });
                get2.onClick.AddListener(delegate { GetStageSelected(crsName[index], 2); });
                get3.onClick.AddListener(delegate { GetStageSelected(crsName[index], 3); });
                get4.onClick.AddListener(delegate { GetStageSelected(crsName[index], 4); });
                get5.onClick.AddListener(delegate { GetStageSelected(crsName[index], 5); });
                get6.onClick.AddListener(delegate { GetStageSelected(crsName[index], 6); });
                get7.onClick.AddListener(delegate { GetStageSelected(crsName[index], 7); });
                get8.onClick.AddListener(delegate { GetStageSelected(crsName[index], 8); });
                get9.onClick.AddListener(delegate { GetStageSelected(crsName[index], 9); });
                get10.onClick.AddListener(delegate { GetStageSelected(crsName[index], 10); });
                break;
            case 2:
                dbtn.SetBtnInvisible(StgCount(crsName[2]));
                courseName = crsName[2];
                get1.onClick.AddListener(delegate { GetStageSelected(crsName[index], 1); });
                get2.onClick.AddListener(delegate { GetStageSelected(crsName[index], 2); });
                get3.onClick.AddListener(delegate { GetStageSelected(crsName[index], 3); });
                get4.onClick.AddListener(delegate { GetStageSelected(crsName[index], 4); });
                get5.onClick.AddListener(delegate { GetStageSelected(crsName[index], 5); });
                get6.onClick.AddListener(delegate { GetStageSelected(crsName[index], 6); });
                get7.onClick.AddListener(delegate { GetStageSelected(crsName[index], 7); });
                get8.onClick.AddListener(delegate { GetStageSelected(crsName[index], 8); });
                get9.onClick.AddListener(delegate { GetStageSelected(crsName[index], 9); });
                get10.onClick.AddListener(delegate { GetStageSelected(crsName[index], 10); });
                break;

            case 3:
                dbtn.SetBtnInvisible(StgCount(crsName[3]));
                courseName = crsName[3];
                get1.onClick.AddListener(delegate { GetStageSelected(crsName[index], 1); });
                get2.onClick.AddListener(delegate { GetStageSelected(crsName[index], 2); });
                get3.onClick.AddListener(delegate { GetStageSelected(crsName[index], 3); });
                get4.onClick.AddListener(delegate { GetStageSelected(crsName[index], 4); });
                get5.onClick.AddListener(delegate { GetStageSelected(crsName[index], 5); });
                get6.onClick.AddListener(delegate { GetStageSelected(crsName[index], 6); });
                get7.onClick.AddListener(delegate { GetStageSelected(crsName[index], 7); });
                get8.onClick.AddListener(delegate { GetStageSelected(crsName[index], 8); });
                get9.onClick.AddListener(delegate { GetStageSelected(crsName[index], 9); });
                get10.onClick.AddListener(delegate { GetStageSelected(crsName[index], 10); });
                break;
            case 4:
                dbtn.SetBtnInvisible(StgCount(crsName[4]));
                courseName = crsName[4];
                get1.onClick.AddListener(delegate { GetStageSelected(crsName[index], 1); });
                get2.onClick.AddListener(delegate { GetStageSelected(crsName[index], 2); });
                get3.onClick.AddListener(delegate { GetStageSelected(crsName[index], 3); });
                get4.onClick.AddListener(delegate { GetStageSelected(crsName[index], 4); });
                get5.onClick.AddListener(delegate { GetStageSelected(crsName[index], 5); });
                get6.onClick.AddListener(delegate { GetStageSelected(crsName[index], 6); });
                get7.onClick.AddListener(delegate { GetStageSelected(crsName[index], 7); });
                get8.onClick.AddListener(delegate { GetStageSelected(crsName[index], 8); });
                get9.onClick.AddListener(delegate { GetStageSelected(crsName[index], 9); });
                get10.onClick.AddListener(delegate { GetStageSelected(crsName[index], 10); });
                break;
            case 5:
                dbtn.SetBtnInvisible(StgCount(crsName[5]));
                courseName = crsName[5];
                get1.onClick.AddListener(delegate { GetStageSelected(crsName[index], 1); });
                get2.onClick.AddListener(delegate { GetStageSelected(crsName[index], 2); });
                get3.onClick.AddListener(delegate { GetStageSelected(crsName[index], 3); });
                get4.onClick.AddListener(delegate { GetStageSelected(crsName[index], 4); });
                get5.onClick.AddListener(delegate { GetStageSelected(crsName[index], 5); });
                get6.onClick.AddListener(delegate { GetStageSelected(crsName[index], 6); });
                get7.onClick.AddListener(delegate { GetStageSelected(crsName[index], 7); });
                get8.onClick.AddListener(delegate { GetStageSelected(crsName[index], 8); });
                get9.onClick.AddListener(delegate { GetStageSelected(crsName[index], 9); });
                get10.onClick.AddListener(delegate { GetStageSelected(crsName[index], 10); });
                break;
            case 6:
                dbtn.SetBtnInvisible(StgCount(crsName[6]));
                courseName = crsName[6];
                get1.onClick.AddListener(delegate { GetStageSelected(crsName[index], 1); });
                get2.onClick.AddListener(delegate { GetStageSelected(crsName[index], 2); });
                get3.onClick.AddListener(delegate { GetStageSelected(crsName[index], 3); });
                get4.onClick.AddListener(delegate { GetStageSelected(crsName[index], 4); });
                get5.onClick.AddListener(delegate { GetStageSelected(crsName[index], 5); });
                get6.onClick.AddListener(delegate { GetStageSelected(crsName[index], 6); });
                get7.onClick.AddListener(delegate { GetStageSelected(crsName[index], 7); });
                get8.onClick.AddListener(delegate { GetStageSelected(crsName[index], 8); });
                get9.onClick.AddListener(delegate { GetStageSelected(crsName[index], 9); });
                get10.onClick.AddListener(delegate { GetStageSelected(crsName[index], 10); });
                break;
            case 7:
                dbtn.SetBtnInvisible(StgCount(crsName[7]));
                courseName = crsName[7];
                get1.onClick.AddListener(delegate { GetStageSelected(crsName[index], 1); });
                get2.onClick.AddListener(delegate { GetStageSelected(crsName[index], 2); });
                get3.onClick.AddListener(delegate { GetStageSelected(crsName[index], 3); });
                get4.onClick.AddListener(delegate { GetStageSelected(crsName[index], 4); });
                get5.onClick.AddListener(delegate { GetStageSelected(crsName[index], 5); });
                get6.onClick.AddListener(delegate { GetStageSelected(crsName[index], 6); });
                get7.onClick.AddListener(delegate { GetStageSelected(crsName[index], 7); });
                get8.onClick.AddListener(delegate { GetStageSelected(crsName[index], 8); });
                get9.onClick.AddListener(delegate { GetStageSelected(crsName[index], 9); });
                get10.onClick.AddListener(delegate { GetStageSelected(crsName[index], 10); });
                break;
            case 8:
                dbtn.SetBtnInvisible(StgCount(crsName[8]));
                courseName = crsName[8];
                get1.onClick.AddListener(delegate { GetStageSelected(crsName[index], 1); });
                get2.onClick.AddListener(delegate { GetStageSelected(crsName[index], 2); });
                get3.onClick.AddListener(delegate { GetStageSelected(crsName[index], 3); });
                get4.onClick.AddListener(delegate { GetStageSelected(crsName[index], 4); });
                get5.onClick.AddListener(delegate { GetStageSelected(crsName[index], 5); });
                get6.onClick.AddListener(delegate { GetStageSelected(crsName[index], 6); });
                get7.onClick.AddListener(delegate { GetStageSelected(crsName[index], 7); });
                get8.onClick.AddListener(delegate { GetStageSelected(crsName[index], 8); });
                get9.onClick.AddListener(delegate { GetStageSelected(crsName[index], 9); });
                get10.onClick.AddListener(delegate { GetStageSelected(crsName[index], 10); });
                break;

        }
            
    }

    //Retrieve monster data from firebase
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
        monster.Add("health", mons["S" + stage.ToString()].health);
        monster.Add("coin", mons["S" + stage.ToString()].coin);
        monster.Add("experience", mons["S" + stage.ToString()].experience);

    }

    //Use this to initialise display when special stage button pressed
    private void SpecialStg()
    {
        dbtn = FindObjectOfType(typeof(DynamicButtons)) as DynamicButtons;
        PopulateList();
        DefaultCanvas.SetActive(true);
        dbtn.SetBtnInvisible(StgCount(crsName[0]));
        courseName = crsName[0];
    }

    //Update variables for passing to Battle System when stage selected
    private void GetStageSelected(string course, int stage)
    {
        levelNo = stage;
        MonsData(stage);
        switch (stage)
        {
            case 1:
                question.Clear();
                answer.Clear();
                for (int i = 0; i < key.Count; i++)
                {
                    if (splevel[key[i]].courseName == course & splevel[key[i]].level == 1)
                    {
                        for (int k = 0; k < splevel[key[i]].qns.Count; k++)
                        {
                            question.Add(splevel[key[i]].qns[k]);
                            answer.Add(splevel[key[i]].ans[k]);
                        }
                        break;
                    }
                }
                break;

            case 2:
                question.Clear();
                answer.Clear();
                for (int i = 0; i < key.Count; i++)
                {
                    if (splevel[key[i]].courseName == course & splevel[key[i]].level == 2)
                    {
                        for (int k = 0; k < splevel[key[i]].qns.Count; k++)
                        {
                            question.Add(splevel[key[i]].qns[k]);
                            answer.Add(splevel[key[i]].ans[k]);
                        }
                        Debug.Log("Stage 2 " + question[0]);
                        break;
                    }
                }
                break;

            case 3:
                question.Clear();
                answer.Clear();
                for (int i = 0; i < key.Count; i++)
                {
                    if (splevel[key[i]].courseName == course & splevel[key[i]].level == 3)
                    {
                        for (int k = 0; k < splevel[key[i]].qns.Count; k++)
                        {
                            question.Add(splevel[key[i]].qns[k]);
                            answer.Add(splevel[key[i]].ans[k]);
                        }
                        Debug.Log("Stage 2 " + question[0]);
                        break;
                    }
                }
                break;
            case 4:
                question.Clear();
                answer.Clear();
                for (int i = 0; i < key.Count; i++)
                {
                    if (splevel[key[i]].courseName == course & splevel[key[i]].level == 4)
                    {
                        for (int k = 0; k < splevel[key[i]].qns.Count; k++)
                        {
                            question.Add(splevel[key[i]].qns[k]);
                            answer.Add(splevel[key[i]].ans[k]);
                        }
                        Debug.Log("Stage 2 " + question[0]);
                        break;
                    }
                }
                break;

            case 5:
                question.Clear();
                answer.Clear();
                for (int i = 0; i < key.Count; i++)
                {
                    if (splevel[key[i]].courseName == course & splevel[key[i]].level == 5)
                    {
                        for (int k = 0; k < splevel[key[i]].qns.Count; k++)
                        {
                            question.Add(splevel[key[i]].qns[k]);
                            answer.Add(splevel[key[i]].ans[k]);
                        }
                        Debug.Log("Stage 2 " + question[0]);
                        break;
                    }
                }
                break;

            case 6:
                question.Clear();
                answer.Clear();
                for (int i = 0; i < key.Count; i++)
                {
                    if (splevel[key[i]].courseName == course & splevel[key[i]].level == 6)
                    {
                        for (int k = 0; k < splevel[key[i]].qns.Count; k++)
                        {
                            question.Add(splevel[key[i]].qns[k]);
                            answer.Add(splevel[key[i]].ans[k]);
                        }
                        Debug.Log("Stage 2 " + question[0]);
                        break;
                    }
                }
                break;

            case 7:
                question.Clear();
                answer.Clear();
                for (int i = 0; i < key.Count; i++)
                {
                    if (splevel[key[i]].courseName == course & splevel[key[i]].level == 7)
                    {
                        for (int k = 0; k < splevel[key[i]].qns.Count; k++)
                        {
                            question.Add(splevel[key[i]].qns[k]);
                            answer.Add(splevel[key[i]].ans[k]);
                        }
                        Debug.Log("Stage 2 " + question[0]);
                        break;
                    }
                }
                break;

            case 8:
                question.Clear();
                answer.Clear();
                for (int i = 0; i < key.Count; i++)
                {
                    if (splevel[key[i]].courseName == course & splevel[key[i]].level == 8)
                    {
                        for (int k = 0; k < splevel[key[i]].qns.Count; k++)
                        {
                            question.Add(splevel[key[i]].qns[k]);
                            answer.Add(splevel[key[i]].ans[k]);
                        }
                        Debug.Log("Stage 2 " + question[0]);
                        break;
                    }
                }
                break;

            case 9:
                question.Clear();
                answer.Clear();
                for (int i = 0; i < key.Count; i++)
                {
                    if (splevel[key[i]].courseName == course & splevel[key[i]].level == 9)
                    {
                        for (int k = 0; k < splevel[key[i]].qns.Count; k++)
                        {
                            question.Add(splevel[key[i]].qns[k]);
                            answer.Add(splevel[key[i]].ans[k]);
                        }
                        Debug.Log("Stage 2 " + question[0]);
                        break;
                    }
                }
                break;

            case 10:
                question.Clear();
                answer.Clear();
                for (int i = 0; i < key.Count; i++)
                {
                    if (splevel[key[i]].courseName == course & splevel[key[i]].level == 10)
                    {
                        for (int k = 0; k < splevel[key[i]].qns.Count; k++)
                        {
                            question.Add(splevel[key[i]].qns[k]);
                            answer.Add(splevel[key[i]].ans[k]);
                        }
                        Debug.Log("Stage 2 " + question[0]);
                        break;
                    }
                }
                break;
        }
        
        
        
    }

    //Populate names in dropdown with Course name
    //myKeys contain strings of Course names
    void PopulateList()
    {

        dropdown.options.Clear();
        List<String> items = new List<String>();
        //check for student registered courses
        for (int i = 0; i < myKeys.Count; i++)
        {
            for(int k=0; k< course[myKeys[i]].students.Count; k++)
            {
                if (course[myKeys[i]].students[k].Equals(userData.userName)) //change Tom to name from database
                {
                    items.Add(myKeys[i]);
                    break;
                }
                    
            }
            
        }

        foreach (var item in items)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = item });
        }

     
    }

    //course is a dictionary with {courseName: students<list> , teacher name}
    //store course names into myKeys list as strings
    private IEnumerator GetStudentCourse()
    {
        RestClient.Get(database + "course.json").Then(response =>
        {


            fsData studentCourse = fsJsonParser.Parse(response.Text);
            serializer.TryDeserialize(studentCourse, ref course);
            myKeys = new List<String>(course.Keys);
            //Maybe try putting populateList method here to retrieve value as global for use
            crsName = new List<String>();
            for (int i = 0; i < myKeys.Count; i++)
            {
                for (int k = 0; k < course[myKeys[i]].students.Count; k++)
                {
                    if (course[myKeys[i]].students[k].Equals(userData.userName)) //change Tom to name from database
                    {
                        crsName.Add(myKeys[i]);
                    }

                }

            }


            courseName = crsName[0];
        });
        yield return null;
    }

    //splevel is a dictionary with {stageKey: ans, courseName, level, qns}
    //key is a list of strings that stores stageKey
    private IEnumerator GetSpecial()
    {


        RestClient.Get(database + "courseLevelQns.json").Then(response =>
        {


            fsData studentCourse = fsJsonParser.Parse(response.Text);
            serializer.TryDeserialize(studentCourse, ref splevel);
            key = new List<String>(splevel.Keys);


        });

        yield return null;
    }


    //Count the number of stages with the course name k
    private int StgCount(string k)
    {
        int level = 0;
        foreach (var i in key)
        {
            if (splevel[i].courseName == k)
                level++;
        }
        return level;
    }

    void Awake()
    {
    }

    void Start()
    {
        userData = mainMenuControllerScript.getUserData();
        StartCoroutine(GetStudentCourse());
        StartCoroutine(GetSpecial());
        StartCoroutine(GetMonsterData());
        get1.onClick.AddListener(delegate { GetStageSelected(crsName[0], 1); });
        get2.onClick.AddListener(delegate { GetStageSelected(crsName[0], 2); });
        get3.onClick.AddListener(delegate { GetStageSelected(crsName[0], 3); });
        get4.onClick.AddListener(delegate { GetStageSelected(crsName[0], 4); });
        get5.onClick.AddListener(delegate { GetStageSelected(crsName[0], 5); });
        get6.onClick.AddListener(delegate { GetStageSelected(crsName[0], 6); });
        get7.onClick.AddListener(delegate { GetStageSelected(crsName[0], 7); });
        get8.onClick.AddListener(delegate { GetStageSelected(crsName[0], 8); });
        get9.onClick.AddListener(delegate { GetStageSelected(crsName[0], 9); });
        get10.onClick.AddListener(delegate { GetStageSelected(crsName[0], 10); });
    }
}
