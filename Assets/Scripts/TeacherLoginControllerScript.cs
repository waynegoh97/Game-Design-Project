using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Text;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
/// <summary>
/// Author: Lee Chong Yu <br></br>
/// A class for handling the teacher login procedure in the Teacher Login page
/// </summary>
public class TeacherLoginControllerScript : MonoBehaviour {

    [SerializeField]
    private InputField emailInput = null;

    [SerializeField]
    private InputField passwordInput = null;

    [SerializeField]
    private GameObject WrongPopUp = null;

    [SerializeField]
    private GameObject EmailPassPopUp = null;

    [SerializeField]
    private GameObject EmailPopUp = null;

    [SerializeField]
    private GameObject PassPopUp = null;

    public UIControllerScript UIController;
    public TeacherLoginHandler teacherLoginHandler;
    public MainMenuControllerScript mainMenuController;
    private TeacherData teacherData;

    // Start is called before the first frame update
    void Start() {
        //teacherLoginHandler.FetchTeacherSignUpInfo("MsHuang@gmail.com", "MsHuang", "Huang123");
    }

    //Update is called once per frame
    void Update() {

    }
    /// <summary>
    /// Function for the teacher to login when he/she clicks on the Login button
    /// </summary>
    public async void login() {
        string email = emailInput.text.Trim().ToLower();
        string password = passwordInput.text;
        if ((email == "" || email == null) && (password == "" || password == null)) {
            StartCoroutine(EmailPasswordNotFill());
        } else {
            if (email == "" || email == null) {
                StartCoroutine(EmailNotFill());
            } else {
                if (password == "" || password == null) {
                    StartCoroutine(PassNotFill());
                } else {
                    string loginChk = await PostRequest(email, password);
                    Debug.Log("login chk" + loginChk);
                    if (loginChk != null) {
                        Debug.Log("4");
                        bool teacherChk = await ChkTeacher(loginChk);
                        if (teacherChk == true) {
                            Debug.Log("teacher: " + teacherData.userName);
                            mainMenuController.setTeacherData(teacherData);
                            mainMenuController.loadTeacherMainMenu();
                            UIController.teacherLoginButton();
                        } else {
                            StartCoroutine(ShowWrongMessage());
                        }
                    } else {
                        Debug.Log("3");
                        StartCoroutine(ShowWrongMessage());
                    }

                }
            }
        }
    }
    /// <summary>
    /// Display password and email not filled message
    /// </summary>
    /// <returns></returns>
    IEnumerator EmailPasswordNotFill() {
        EmailPassPopUp.SetActive(true);
        yield return new WaitForSeconds(2);
        EmailPassPopUp.SetActive(false);

    }
    /// <summary>
    /// Display email not filled message
    /// </summary>
    /// <returns></returns>
    IEnumerator EmailNotFill() {
        EmailPopUp.SetActive(true);
        yield return new WaitForSeconds(2);
        EmailPopUp.SetActive(false);

    }
    /// <summary>
    /// Dispalay password not filled message
    /// </summary>
    /// <returns></returns>
    IEnumerator PassNotFill() {
        PassPopUp.SetActive(true);
        yield return new WaitForSeconds(2);
        PassPopUp.SetActive(false);

    }
    /// <summary>
    /// Show login failed message
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowWrongMessage() {
        WrongPopUp.SetActive(true);
        yield return new WaitForSeconds(2);
        WrongPopUp.SetActive(false);

    }
    /// <summary>
    /// Submit a post request to the server to username and password validation
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public async Task<string> PostRequest(string username, string password) {
        string result = await teacherLoginHandler.FetchTeacherSignInInfo(username, password);
        return result;
        Debug.Log("Check Result: " + result);
    }
    /// <summary>
    /// Get the student data during login.
    /// </summary>
    /// <param name="localID"></param>
    /// <returns></returns>
    public async Task<bool> ChkTeacher(string localID) {
        bool chk = false;
        TeacherLoginHandler.GetTeacher(localID, teacher => {
            chk = true;
            Debug.Log("inn");
            teacherData = new TeacherData();
            teacherData.email = teacher.email;
            teacherData.userName = teacher.userName;
            teacherData.verified = teacher.verified;
            teacherData.localId = teacher.localId;
            Debug.Log("inn2");
        });
        Stopwatch sw = Stopwatch.StartNew();
        var delay = Task.Delay(2000).ContinueWith(_ => {
            sw.Stop();
            return sw.ElapsedMilliseconds;
        });
        await delay;
        int sec = (int)delay.Result;
        Debug.Log("Time elapsed milliseconds: {0}" + sec + ", Chk is " + chk);
        return chk;
    }


}
