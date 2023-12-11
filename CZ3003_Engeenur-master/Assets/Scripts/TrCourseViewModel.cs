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
///Create and delete courses
///</summary>
public class TrCourseViewModel : MonoBehaviour
{
    public GameObject itemParent, item, formCreate, messageBox, delMsgBox, loader, specialLevelCanvas;
    public InputField courseInput;
    public bool chk = false;
    public EnrollViewModel enrollViewModel;
    public UIControllerScript UIController;
    public string userName;
    public bool created = false;

    public string teacherName;
    // Start is called before the first frame update
    async void Start()
    {
        Debug.Log("USERNAME IS aa " + userName);
        //loader.SetActive(true);
        await Read();
        //loader.SetActive(false);
    }

    ///<summary>
    ///Create course buttons
    ///</summary> 
    public async Task Read()
    {
        Debug.Log("ITEMmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm");
        int count = itemParent.transform.childCount;
        Debug.Log("IN ITEMPARENT");
        if (count != 0)
        {
            for (int i = 0; i < count; i++)
            {
                Debug.Log("itemparent");
                Destroy(itemParent.transform.GetChild(i).gameObject);
            }

        }
        Debug.Log("ITEM 2");
        DatabaseQAHandler.GetCourses(courses =>
        {
            foreach (var course in courses)
            {
                Debug.Log($"{course.Key} {course.Value.userName}");
                if (course.Value.userName == userName)
                {
                    GameObject tmp_btn = Instantiate(item, itemParent.transform);
                    tmp_btn.name = course.Key;
                    Debug.Log("item name: " + tmp_btn.name);
                    tmp_btn.transform.GetChild(1).GetComponent<Text>().text = course.Key;
                }
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
        Debug.Log("Read elapsed milliseconds: {0}" + sec);
    }

    ///<summary>
    ///When Cancel is clicked 
    ///</summary> 
    public void Cancel()
    {
        formCreate.transform.GetChild(1).GetComponent<InputField>().text = "";
    }

    ///<summary>
    ///Course creation check
    ///</summary> 
    public async void CreateCourse()
    {
        created = false;
        string courseName = courseInput.text;
        bool handler = Check();
        if (handler == true)
        {
            Debug.Log("Line75: " + courseName);
            await InvokeCourseCheckExist(courseName);
        }
        formCreate.transform.GetChild(1).GetComponent<InputField>().text = "";
        chk = false;
    }

    ///<summary>
    ///Basic checking of form elements
    ///</summary> 
    public bool Check()
    {
        string str = Regex.Replace(courseInput.text, @"\s", "");
        if (courseInput.text == null || str == "")
        {
            messageBox.SetActive(true);
            messageBox.transform.GetChild(1).GetComponent<Text>().text = "Please enter a course name.";
            return false;
        }
        return true;
    }

    
    public bool locks = true;
    ///<summary>
    ///Invoke checkCourseExist
    ///</summary> 
    public async Task InvokeCourseCheckExist(string courseName)
    {
        Task<bool> task = CheckCourseExist(courseName);
        bool courseExist = await task;
        Debug.Log("Course Exist: " + courseExist);

        while (locks) {}
        if (courseExist == true)
        {
            created = false;
            Debug.Log("created " + created);
            Debug.Log("ABOVE MSG BOX1");
            messageBox.SetActive(true);
            Debug.Log("ABOVE MSG BOX2");
            messageBox.transform.GetChild(1).GetComponent<Text>().text = courseName + " already exist.";
        }
        else
        {
            created = true;
            List<string> students = new List<string>();
            var course = new Course(userName, students);
            Debug.Log("Before loader");
            loader.SetActive(true);
            Debug.Log("created " + created);
            await PostingCourse(course, courseName);
            await Read();
            loader.SetActive(false);
            Debug.Log("messageBox");
            messageBox.SetActive(true);
            Debug.Log("messageBox2");
            messageBox.transform.GetChild(1).GetComponent<Text>().text = courseName + " created successfully.";
        }
    }

    ///<summary>
    ///Creating course in database
    ///</summary> 
    public async Task PostingCourse(Course course, string courseName)
    {
        DatabaseQAHandler.PostCourse(course, courseName, () => { });
        Stopwatch sw = Stopwatch.StartNew();
        var delay = Task.Delay(1000).ContinueWith(_ =>
                                   {
                                       sw.Stop();
                                       return sw.ElapsedMilliseconds;
                                   });
        await delay;
        int sec = (int)delay.Result;
        Debug.Log("Creating Course Elapsed milliseconds: {0}" + sec);
    }

    ///<summary>
    ///Check if the course already exist
    ///</summary> 
    public async Task<bool> CheckCourseExist(string courseName)
    {
        chk = false;
        Debug.Log("line140:" + courseName + "!");
        DatabaseQAHandler.GetCourse(courseName, course =>
        {
            chk = true;
            Debug.Log("chCourseExist1" + chk);
        });
        Stopwatch sw = Stopwatch.StartNew();
        var delay = Task.Delay(2000).ContinueWith(_ =>
                                   {
                                       sw.Stop();
                                       return sw.ElapsedMilliseconds;
                                   });
        await delay;
        int sec = (int)delay.Result;
        Debug.Log("check course exist elapsed milliseconds: {0}" + sec);
        locks = false;
        return chk;
    }

    ///<summary>
    ///Check the name of the course to be deleted
    ///</summary> 
    public string courseKey;
    public void CheckItemDelName(GameObject item)
    {
        courseKey = item.name;
        Debug.Log("CHECK DELETE name: " + courseKey);
        delMsgBox.transform.GetChild(1).GetComponent<Text>().text = "Are you sure you want to delete " + courseKey + "?";

    }

    ///<summary>
    ///Delete course in database
    ///</summary> 
    public async void DeleteCourse()
    {
        loader.SetActive(true);
        Debug.Log("DELETE name: " + courseKey);
        DatabaseQAHandler.DeleteCourse(courseKey, () => { });
        DatabaseQAHandler.GetCourseLvlQns(courseLvlQns =>
        {
            foreach (var courseLvlQn in courseLvlQns)
            {
                //Debug.Log($"{course.Key} {course.Value.userName}");
                if (courseLvlQn.Value.courseName == courseKey) //Find course created by you
                {
                    DatabaseQAHandler.DeleteCourseLvlQn(courseLvlQn.Key, () => { });
                }
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
        Debug.Log("Delete elapsed milliseconds: {0}" + sec);
        await Read();
        loader.SetActive(false);
        messageBox.SetActive(true);
        messageBox.transform.GetChild(1).GetComponent<Text>().text = courseKey + " deleted successfully.";
        courseKey = null;
    }

    ///<summary>
    ///Click a course button
    ///</summary> 
    public void ClickCourse(GameObject item)
    {
        enrollViewModel.courseName = item.name;
        enrollViewModel.userName = userName;
        enrollViewModel.WakeUp();
        Debug.Log("CLICK COURSE ITEM NAME: " + item.name);
        UIController.CourseToEnrollmentCanvas();
    }

    ///<summary>
    ///Click close button
    ///</summary> 
    public void ClickClose()
    {
        UIController.CourseToTrTown();
    }

    //wake up 
    public async void WakeUp()
    {
        loader.SetActive(true);
        await Read();
        loader.SetActive(false);
    }

}