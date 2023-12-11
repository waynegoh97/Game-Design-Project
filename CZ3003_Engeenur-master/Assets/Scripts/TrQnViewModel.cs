using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
///<summary>
///Author: Lim Pei Yan <br/>
///Create, update and delete questions
///</summary>
public class TrQnViewModel : MonoBehaviour
{
    public GameObject itemParent, item, formCreate, formUpdate, messageBox, delMsgBox, loader;
    public string key;
    public string courseName;
    public string userName;
    public int level;
    public InputField qnInput;
    public InputField ansInput;
    public InputField qnUpdateInput;
    public InputField ansUpdateInput;
    private bool chk = false;
    CourseLvlQn courseLvlQnCreate;
    public TrSpecialLvlViewModel trSpecialLvlViewModel;
    public TrCourseViewModel trCourseViewModel;
    public UIControllerScript UIController;
    public int count=0;

    void Start()
    {
    }

    ///<summary>
    ///Create rows for the list of questions and answers created
    ///</summary> 
    public async Task Read()
    {
        count=0;
        for (int i = 0; i < itemParent.transform.childCount; i++)
        {
            Destroy(itemParent.transform.GetChild(i).gameObject);
        }
        DatabaseQAHandler.GetCourseLvlQn(key, courseLvlQn =>
        {
            courseLvlQnCreate = courseLvlQn;
            int number = 1;
            for (int i = 0; i < (courseLvlQn.qns).Count; i++) // Loop through List
            {
                GameObject tmp_item = Instantiate(item, itemParent.transform);
                tmp_item.name = i.ToString();
                Debug.Log("here item name: " + tmp_item.name);
                tmp_item.transform.GetChild(0).GetComponent<Text>().text = number.ToString();
                tmp_item.transform.GetChild(1).GetComponent<Text>().text = courseLvlQn.qns[i];
                tmp_item.transform.GetChild(2).GetComponent<Text>().text = (courseLvlQn.ans[i]).ToString();
                number++;
                count++;
            }
        });
        Stopwatch sw = Stopwatch.StartNew();
        var delay = Task.Delay(1000).ContinueWith(_ =>
                                   {
                                       sw.Stop();
                                       return sw.ElapsedMilliseconds;
                                   });
        await delay;
        int sec = (int)delay.Result;
        Debug.Log("Read elapsed milliseconds: {0}" + sec +"Count:"+count);
    }

    ///<summary>
    ///Create question check
    ///</summary> 
    public async void CreateQns()
    {
        string qn = qnInput.text;
        string ans = ansInput.text;
        bool handler = Check(qn, ans);
        if (handler == true)
        {
            await InvokeQnCheckExist(qn, ans);
        }
        chk = false;
        formCreate.transform.GetChild(1).GetComponent<InputField>().text = "";
        formCreate.transform.GetChild(2).GetComponent<InputField>().text = "";
    }


    ///<summary>
    ///Basic Form Check
    ///</summary> 
    ///<param name = "qn">Question Input</param>
    ///<param name = "ans">Answer Input</param>
    public bool Check(string qn, string ans)
    {
        string strQ = Regex.Replace(qn, @"\s", "");
        string strA = Regex.Replace(ans, @"\s", "");
        if (qn == null || strQ == "")
        {
            messageBox.SetActive(true);
            messageBox.transform.GetChild(1).GetComponent<Text>().text = "Please enter a question.";
            return false;
        }
        else if (ans == null || strA == "")
        {
            messageBox.SetActive(true);
            messageBox.transform.GetChild(1).GetComponent<Text>().text = "Please enter a question.";
            return false;
        }
        return true;
    }

    ///<summary>
    ///Question Creation Check
    ///</summary> 
    ///<param name = "qn">Question Input</param>
    ///<param name = "ans">Answer Input</param>
    public async Task InvokeQnCheckExist(string qn, string ans)
    {
        Task<bool> task = CheckQnExist(qn);
        bool qnExist = await task;
        Debug.Log("Qn Exist: " + qnExist);
        if (qnExist == true)
        {
            messageBox.SetActive(true);
            messageBox.transform.GetChild(1).GetComponent<Text>().text = qn + " already exist.";
        }
        else
        {
            loader.SetActive(true);
            await PostingQn(qn, ans);
            await Read();
            loader.SetActive(false);
            messageBox.SetActive(true);
            messageBox.transform.GetChild(1).GetComponent<Text>().text = qn + " created successfully.";

        }
    }

    ///<summary>
    ///Check for existing question
    ///</summary> 
    ///<param name = "qn">Question Input</param>
    public async Task<bool> CheckQnExist(string qn)
    {
        DatabaseQAHandler.GetCourseLvlQn(key, courseLvlQn =>
        {
            //courseLvlQnCreate = courseLvlQn;
            for (int i = 0; i < (courseLvlQn.qns).Count; i++) // Loop through List
            {
                if (qn == courseLvlQn.qns[i])
                {
                    chk = true;
                }
            }
        });
        Stopwatch sw = Stopwatch.StartNew();
        var delay = Task.Delay(500).ContinueWith(_ =>
                                   {
                                       sw.Stop();
                                       return sw.ElapsedMilliseconds;
                                   });
        await delay;
        int sec = (int)delay.Result;
        Debug.Log("check qn exist elapsed milliseconds: {0}" + sec + ", Chk is " + chk);
        return chk;
    }

    ///<summary>
    ///Creating question in Database
    ///</summary> 
    ///<param name = "qn">Question Input</param>
    ///<param name = "ans">Answer Input</param>
    public async Task PostingQn(string qn, string ans)
    {
        int intAns = int.Parse(ans,System.Globalization.NumberStyles.Integer);
        (courseLvlQnCreate.qns).Add(qn);
        (courseLvlQnCreate.ans).Add(intAns);
        DatabaseQAHandler.PutCourseLvlQn(key, courseLvlQnCreate, () => { });
        Stopwatch sw = Stopwatch.StartNew();
        var delay = Task.Delay(1000).ContinueWith(_ =>
                                   {
                                       sw.Stop();
                                       return sw.ElapsedMilliseconds;
                                   });
        await delay;
        int sec = (int)delay.Result;
        Debug.Log("Creating Qn Elapsed milliseconds: {0}" + sec);
    }

    ///<summary>
    ///Check the name of the course to be deleted
    ///</summary> 
    ///<param name = "item">GameObject</param>
    public int delName;
    public void CheckDelName(GameObject item)
    {
        delName = int.Parse(item.name, System.Globalization.NumberStyles.Integer);
        Debug.Log("CHECK DELETE name: " + delName);
        delMsgBox.transform.GetChild(1).GetComponent<Text>().text = "Are you sure you want to delete this question?";

    }

    ///<summary>
    ///Deleting question in Database
    ///</summary> 
    public async void DeleteQn()
    {
        loader.SetActive(true);
        (courseLvlQnCreate.qns).RemoveAt(delName);
        (courseLvlQnCreate.ans).RemoveAt(delName);
        DatabaseQAHandler.PutCourseLvlQn(key, courseLvlQnCreate, () => { });
        Stopwatch sw = Stopwatch.StartNew();
        var delay = Task.Delay(1000).ContinueWith(_ =>
                                   {
                                       sw.Stop();
                                       return sw.ElapsedMilliseconds;
                                   });
        await delay;
        int sec = (int)delay.Result;
        Debug.Log("Creating Qn Elapsed milliseconds: {0}" + sec);
        await Read();
        loader.SetActive(false);
        messageBox.SetActive(true);
        messageBox.transform.GetChild(1).GetComponent<Text>().text = "Deleted successfully.";
        //key = null;
    }

    ///<summary>
    ///Check the name of the course to be edited
    ///</summary> 
    public int editName;
    public void OpenUpdateForm(GameObject item)
    {
        editName = int.Parse(item.name, System.Globalization.NumberStyles.Integer);
        Debug.Log("CHECK EDIT name: " + editName);
        formUpdate.transform.GetChild(1).GetComponent<InputField>().text = courseLvlQnCreate.qns[editName];
        formUpdate.transform.GetChild(2).GetComponent<InputField>().text = (courseLvlQnCreate.ans[editName]).ToString();
    }
    
    ///<summary>
    ///Edit question basic check
    ///</summary> 
    public async void EditQn()
    {
        string qn = qnUpdateInput.text;
        string ans = ansUpdateInput.text;
        bool handler = Check(qn, ans);
        if (handler == true)
        {
            if (qn == courseLvlQnCreate.qns[editName] && ans == courseLvlQnCreate.ans[editName].ToString())
            {
                messageBox.SetActive(true);
                messageBox.transform.GetChild(1).GetComponent<Text>().text = "No changes in question and answer.";
            }
            else
            {
                await UpdateQn(qn, ans);
            }
        }
        formUpdate.transform.GetChild(1).GetComponent<InputField>().text = "";
        formUpdate.transform.GetChild(2).GetComponent<InputField>().text = "";
    }

    ///<summary>
    ///Update question in Database
    ///</summary> 
    ///<param name = "qn">Question Input</param>
    ///<param name = "ans">Answer Input</param>
    public async Task UpdateQn(string qn, string ans)
    {
        loader.SetActive(true);
        courseLvlQnCreate.qns[editName] = qn;
        courseLvlQnCreate.ans[editName] = int.Parse(ans,System.Globalization.NumberStyles.Integer);
        DatabaseQAHandler.PutCourseLvlQn(key, courseLvlQnCreate, () => { });
        Stopwatch sw = Stopwatch.StartNew();
        var delay = Task.Delay(1000).ContinueWith(_ =>
                                   {
                                       sw.Stop();
                                       return sw.ElapsedMilliseconds;
                                   });
        await delay;
        int sec = (int)delay.Result;
        Debug.Log("Updating Qn Elapsed milliseconds: {0}" + sec);
        await Read();
        loader.SetActive(false);
        messageBox.SetActive(true);
        messageBox.transform.GetChild(1).GetComponent<Text>().text = "Updated successfully.";
    }

    //Wake up 
    public async void WakeUp()
    {
        Debug.Log("Course Name In tr qn: " + courseName);
        Debug.Log("Username In tr qn: " + userName);
        Debug.Log("Key In tr qn: " + key);
        loader.SetActive(true);
        await Read();
        loader.SetActive(false);
    }

    ///<summary>
    ///When click Close
    ///</summary> 
    public void CloseTrQn()
    {
        trSpecialLvlViewModel.WakeUp();
        UIController.CloseTrQnCanvas();
    }

    ///<summary>
    ///When click Next
    ///</summary> 
    public void Next()
    {
        trCourseViewModel.WakeUp();
        UIController.TrQnToCourseCanvas();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
