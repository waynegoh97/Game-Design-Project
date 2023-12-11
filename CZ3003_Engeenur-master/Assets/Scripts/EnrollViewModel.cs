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
///Enroll and unenroll student(s) in a course
///</summary>
public class EnrollViewModel : MonoBehaviour
{
    public GameObject itemParent, item, formCreate, messageBox, delMsgBox, loader;
    public string courseName;
    public string userName;
    public InputField studInput;
    private bool chk = false;
    private bool chkStudValid = false;
    Course currentCourse;
    public TrSpecialLvlViewModel trSpecialLvlViewModel;
    public TrCourseViewModel trCourseViewModel;
    public UIControllerScript UIController;

    public bool created = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    ///<summary>
    ///Create rows showing the list of enrolled students
    ///</summary> 
    public async Task Read()
    {
        int count = itemParent.transform.childCount;
        if (count != null)
        {
            for (int i = 0; i < count; i++)
            {
                Debug.Log("itemparent");
                Destroy(itemParent.transform.GetChild(i).gameObject);
            }

        }
        DatabaseQAHandler.GetCourse(courseName, course =>
        {
            currentCourse = new Course(userName, course.students);
            int number = 1;
            for (int i = 0; i < (course.students).Count; i++) // Loop through List
            {
                Debug.Log("STUDENT[I]" + course.students[i]);
                GameObject tmp_item = Instantiate(item, itemParent.transform);
                tmp_item.name = i.ToString();
                Debug.Log("here item name: " + tmp_item.name);
                tmp_item.transform.GetChild(0).GetComponent<Text>().text = number.ToString();
                tmp_item.transform.GetChild(1).GetComponent<Text>().text = course.students[i];
                number++;
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
    ///Create Student Method
    ///</summary> 
    public async void CreateStudEnroll()
    {
        created = false;
        string studName = studInput.text;
        bool handler = Check(studName);
        if (handler == true)
        {
            await InvokeStudCheck(studName);
        }
        chk = false;
        chkStudValid = false;
        formCreate.transform.GetChild(1).GetComponent<InputField>().text = "";
    }

    ///<summary>
    ///Basic Form Checking 
    ///</summary> 
    public bool Check(string studName)
    {
        string str = Regex.Replace(studName, @"\s", "");
        if (studName == null || str == "")
        {
            messageBox.SetActive(true);
            messageBox.transform.GetChild(1).GetComponent<Text>().text = "Please enter student username.";
            return false;
        }
        return true;
    }

    ///<summary>
    ///Student Creation Checks
    ///</summary> 
    ///<param name = "studName">Student's username</param>
    public async Task InvokeStudCheck(string studName)
    {
        Task<bool> task = CheckStudEnroll(studName);
        bool studEnroll = await task;
        Debug.Log("Stud Enroll: " + studEnroll);
        if (studEnroll == true)
        {
            messageBox.SetActive(true);
            messageBox.transform.GetChild(1).GetComponent<Text>().text = studName + " already enrolled.";
        }
        else
        {
            Task<bool> taskValid = CheckStudValid(studName);
            bool studValidCheck = await taskValid;
            Debug.Log("studValid"+ studValidCheck);
            if (studValidCheck == false) // Student Username Invalid
            {
                messageBox.SetActive(true);
                messageBox.transform.GetChild(1).GetComponent<Text>().text = studName + " does not exist";
            }
            else
            {
                loader.SetActive(true);
                created = true;
                Debug.Log("1");
                await PostingStud(studName);
                await Read();
                loader.SetActive(false);
                messageBox.SetActive(true);
                messageBox.transform.GetChild(1).GetComponent<Text>().text = studName + " enroll successfully.";
            }
        }
    }

    ///<summary>
    ///Check whether student is enrolled
    ///</summary> 
    ///<param name = "studName">Student's username</param>
    public async Task<bool> CheckStudEnroll(string studName)
    {
        DatabaseQAHandler.GetCourse(courseName, course =>
        {
            for (int i = 0; i < (course.students).Count; i++) // Loop through List
            {
                Debug.Log("STUDENT[I]" + course.students[i]);
                Debug.Log("studname:" + studName);
                if (studName == course.students[i])
                {
                    Debug.Log("SAMEE");
                    chk = true;
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
        Debug.Log("check stud enroll elapsed milliseconds: {0}" + sec + ", Chk is " + chk);
        return chk;
    }

    ///<summary>
    ///Check whether the student username is valid
    ///</summary> 
    ///<param name = "studName">Student's username</param>
    public async Task<bool> CheckStudValid(string studName)
    {
        DatabaseQAHandler.GetUsers(userDatas =>
        {
            foreach (var userData in userDatas)
            {
                Debug.Log($"{userData.Value.userName}");
                if (userData.Value.userName == studName)
                {
                    Debug.Log("USERNAME VALIG");
                    chkStudValid = true; // Student username is valid
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
        Debug.Log("check stud exist elapsed milliseconds: {0}" + sec + ", Chk Valid is " + chkStudValid);
        return chkStudValid;
    }

    ///<summary>
    ///Creating stud in Database
    ///</summary> 
    ///<param name = "studName">Student's username</param>
    public async Task PostingStud(string studName)
    {
        (currentCourse.students).Add(studName);
        DatabaseQAHandler.PutCourse(courseName, currentCourse, () => { });
        Stopwatch sw = Stopwatch.StartNew();
        var delay = Task.Delay(1000).ContinueWith(_ =>
        {
            sw.Stop();
            return sw.ElapsedMilliseconds;
        });
        await delay;
        int sec = (int)delay.Result;
        Debug.Log("Enroll Student Elapsed milliseconds: {0}" + sec);
    }

    ///<summary>
    ///Check the name of the course to be deleted
    ///</summary> 
    ///<param name = "item">A GameObject</param>
    public int delName;
    public void CheckDelName(GameObject item)
    {
        delName = int.Parse(item.name, System.Globalization.NumberStyles.Integer);
        Debug.Log("CHECK DELETE name: " + delName);
        delMsgBox.transform.GetChild(1).GetComponent<Text>().text = "Are you sure you want to unenroll this student?";

    }

    ///<summary>
    ///Deleting stud in Database
    ///</summary> 
    public async void DeleteStud()
    {
        loader.SetActive(true);
        (currentCourse.students).RemoveAt(delName);
        DatabaseQAHandler.PutCourse(courseName, currentCourse, () => { });
        Stopwatch sw = Stopwatch.StartNew();
        var delay = Task.Delay(1000).ContinueWith(_ =>
        {
            sw.Stop();
            return sw.ElapsedMilliseconds;
        });
        await delay;
        int sec = (int)delay.Result;
        Debug.Log("Delete Stud Elapsed milliseconds: {0}" + sec);
        await Read();
        loader.SetActive(false);
        messageBox.SetActive(true);
        messageBox.transform.GetChild(1).GetComponent<Text>().text = "Unenroll successfully.";
    }

    ///<summary>
    /// Invoke the Read() Method
    ///</summary> 
    public async void WakeUp()
    {
        Debug.Log("Course Name In enrollment: " + courseName);
        Debug.Log("Username In enrollment: " + userName);
        loader.SetActive(true);
        await Read();
        loader.SetActive(false);
    }

    ///<summary>
    ///Returns to Course Canvas
    ///</summary> 
    public void CloseEnroll()
    {
        trCourseViewModel.WakeUp();
        UIController.EnrollmentToCourseCanvas();
    }

    ///<summary>
    ///Proceed to Special Level Canvas when clicked "Next"
    ///</summary> 
    public void Next()
    {
        trSpecialLvlViewModel.courseName = courseName;
        trSpecialLvlViewModel.userName = userName;
        trSpecialLvlViewModel.WakeUp();
        UIController.EnrollmentToSpecialLevelCanvas();
    }

    // Update is called once per frame
    void Update()
    {

    }
}