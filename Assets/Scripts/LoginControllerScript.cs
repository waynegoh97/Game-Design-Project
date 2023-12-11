using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

/// <summary>
/// Author: Lee Chong Yu <b></b>
/// This class is to control the login functionality for teacher and students in the login page
/// </summary>
public class LoginControllerScript : MonoBehaviour {

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

    public LoginDbHandler loginDbHandler;

    public MainMenuControllerScript mainMenuController;
    public UserData userData;


    public bool studentChk = false; //Set as 'false'. Now 'true' for testing purpose
    private string response;
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {

    }

    /// <summary>
    /// This function is use to allow the user to login when the user click on the Login button. Checks will be done by the server to verify the user before logging him/her in
    /// </summary>
    public async void login() {
        string email = emailInput.text.Trim().ToLower();
        string password = passwordInput.text;
        Debug.Log(email);
        Debug.Log(password);

        if ((email == "" || email == null) && (password == "" || password == null)) {
            Debug.Log("Email Password not filled");
            StartCoroutine(EmailPasswordNotFill());
        } else {
            if (email == "" || email == null) {
                Debug.Log("Email not filled");
                StartCoroutine(EmailNotFill());
            } else {
                if (password == "" || password == null) {
                    Debug.Log("Password not filled");
                    StartCoroutine(PassNotFill());
                } else {
                    Debug.Log(email);
                    Debug.Log(password);
                    Debug.Log("entering loginChk");
                    string loginChk = await PostRequest(email, password);
                    Debug.Log("exited loginChk. Value below.");
                    Debug.Log(loginChk);
                    if (loginChk != null) {
                        Debug.Log("loginChk is not null");
                        studentChk = await ChkStudent(loginChk);
                        Debug.Log("studenChk exited");
                        if (studentChk == true) {
                            Debug.Log("studentChk true");
                            Debug.Log(userData.userName);
                            mainMenuController.setUserData(userData);
                            mainMenuController.loadMainMenu();
                            UIController.loginButton();
                        } else {
                            Debug.Log("Wrong User Pass");
                            StartCoroutine(ShowWrongMessage());
                        }
                    } else {
                        Debug.Log("Wrong User Pass");
                        StartCoroutine(ShowWrongMessage());
                    }

                }
            }
        }
    }

    /*public void gotoCreateAccount()
    {
        UIController.gotoCreateAccountButton();
    }*/
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
        string result = await loginDbHandler.FetchUserSignInInfo(username, password);
        Debug.Log("Check Result: " + result);
        return result;

    }
    /// <summary>
    /// Get the student data during login.
    /// </summary>
    /// <param name="localID"></param>
    /// <returns></returns>
    public async Task<bool> ChkStudent(string localID) {
        bool chk = false;
        LoginDbHandler.GetStudent(localID, student => {
            chk = true;
            userData = new UserData();
            userData.coin = student.coin;
            userData.email = student.email;
            userData.experience = student.experience;
            userData.hp = student.hp;
            userData.level = student.level;
            userData.localId = student.localId;
            userData.maxExperience = student.maxExperience;
            userData.userName = student.userName;
            userData.verified = student.verified;
        });
        Stopwatch sw = Stopwatch.StartNew();
        var delay = Task.Delay(1000).ContinueWith(_ => {
            sw.Stop();
            return sw.ElapsedMilliseconds;
        });
        await delay;
        int sec = (int)delay.Result;
        Debug.Log("Time elapsed milliseconds: {0}" + sec + ", Chk is " + chk);
        return chk;
    }
}
