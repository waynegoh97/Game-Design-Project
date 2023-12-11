using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

/// <summary>
/// Author: Lee Chong Yu <br></br>
/// This class is used as a controller for handling account creation
/// </summary>
public class CreateAccountControllerScript : MonoBehaviour {

    [SerializeField]
    private InputField emailInput = null;

    [SerializeField]
    private InputField UserName = null;

    [SerializeField]
    private InputField Password = null;

    [SerializeField]
    private InputField PasswordConfirm = null;

    [SerializeField]
    private GameObject WrongEmailPopUp = null;

    [SerializeField]
    private GameObject WrongPasswordPopUp = null;

    [SerializeField]
    private GameObject InvalidPasswordPopUp = null;

    [SerializeField]
    private GameObject EmailNotFillPopUp = null;

    [SerializeField]
    private GameObject NameNotFillPopUp = null;

    [SerializeField]
    private GameObject PasswordNotFillPopUp = null;

    [SerializeField]
    private GameObject CfmPassNotFillPopUp = null;

    [SerializeField]
    private GameObject UsernameExistPopUp = null;

    [SerializeField]
    private GameObject EmailExistPopUp = null;

    public MainMenuControllerScript MainMenuController;

    public UIControllerScript UIController;
    public LoginDbHandler loginDbHandler;

    private bool verified = false; //Set as 'false'. Now 'true' for testing purpose
    private string response;
    private bool chkEmailResult = false;
    private bool chkUsernameResult = false;
    public string email;
    public bool check = true;
    public bool nameExistflag = false;
    public bool pwMismatchflag = false;
    public bool emailEmptyflag = false;
    public bool emailFormatWrong = false;
    public bool emptyUsername = false;
    public bool emailExistflag = false;
    public bool passwordEmpty = false;
    public bool wrongPasswordFormat = false;
    public bool emptyCfmPassword = false;

    /// <summary>
    /// Function for creating account upon clicking on the "Create" button in the Create Account page
    /// </summary>
    public async void createAccount() {
        email = emailInput.text.Trim().ToLower();
        Debug.Log(email);
        string username = UserName.text.Trim().ToLower();
        string password = Password.text;
        string confirmPassword = PasswordConfirm.text;

        //Checking Email
        if (email == null || email == "") //Check if Email Input is empty
        {
            check = false;
            emailEmptyflag = true;
            StartCoroutine(ShowEmailNotFillMessage());
        } else {
            bool isEmail = Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            if (!isEmail) //Checking if email format is correct
            {
                check = false;
                emailFormatWrong = true;
                Debug.Log("Not email");
                StartCoroutine(ShowEmailWrongMessage());
            } else {
                await CheckEmailAccExist(email);
                //StartCoroutine(CheckEmailAccExist(email)); //Check if this email already has an account

                if (chkEmailResult) {
                    check = false;
                    emailExistflag = true;
                    Debug.Log("Email acc exist");
                    StartCoroutine(ShowEmailExistMessage());
                }
            }
        }

        //Checking Username
        if (username == null || username == "") //Check if Username Input is empty
        {
            check = false;
            emptyUsername = true;
            Debug.Log("Empty name");
            StartCoroutine(ShowUsernameNotFillMessage());
        } else {
            await CheckUsernameExist(username); //Check if this username already has an account 
            Debug.Log(chkUsernameResult.ToString());
            if (chkUsernameResult) {
                check = false;
                nameExistflag = true;
                Debug.Log("Name exist");
                StartCoroutine(ShowUsernameExistMessage());
            }
        }

        //Checking Password
        if (password == null || password == "") //Check if Password Input is empty
        {
            check = false;
            passwordEmpty = true;
            StartCoroutine(ShowPasswordNotFillMessage());
        } else {
            // Checking if password format is correct(8-50 characters,One Uppercase letter, One lowercase letter and One Digit)
            bool passwordChk = Regex.IsMatch(password, @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$");
            if (!passwordChk) {
                check = false;
                wrongPasswordFormat = true;
                Debug.Log("Invalid password");
                StartCoroutine(ShowInvalidPasswordWrongMessage());
            }
        }

        //Checking Confirm Password
        if (confirmPassword == null || confirmPassword == "") //Check if Confirm Password Input is empty
        {
            check = false;
            emptyCfmPassword = true;
            Debug.Log("Password not filled");
            StartCoroutine(ShowCfmPassNotFillMessage());
        } else {
            if (password != confirmPassword) {
                check = false;
                pwMismatchflag = true;
                Debug.Log("Not same password");
                StartCoroutine(ShowPasswordWrongMessage());
            }
        }

        //Create acc if it pass all checks
        if (check == true) {
            Debug.Log("checks are ok");
            Debug.Log("email" + email);
            Debug.Log(username);
            Debug.Log(password);
            loginDbHandler.FetchUserSignUpInfo(email, username, password);
            UIController.CreateAccToMainCanvas();
        }
    }
    /// <summary>
    /// Function to check the existence of the email in the database
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task CheckEmailAccExist(string email) {
        chkEmailResult = false;
        LoginDbHandler.GetUsers(userDatas => {
            foreach (var userData in userDatas) {
                Debug.Log($"{userData.Key}");
                if (userData.Value.email == email) {
                    chkEmailResult = true;
                    break;
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
        Debug.Log("Email check elapsed milliseconds: {0}" + sec);
    }
    /// <summary>
    /// Function for checking the existence of the username in the database
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    public async Task CheckUsernameExist(string userName) {
        chkUsernameResult = false;
        LoginDbHandler.GetUsers(userDatas => {
            foreach (var userData in userDatas) {
                Debug.Log($"{userData.Key}");
                if (userData.Value.userName == userName) {
                    chkUsernameResult = true;
                    break;
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
        Debug.Log("Username check elapsed milliseconds: {0}" + sec);
    }
    /// <summary>
    /// Popup alert when email is incorrect
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowEmailWrongMessage() {
        WrongEmailPopUp.SetActive(true);
        yield return new WaitForSeconds(2);
        WrongEmailPopUp.SetActive(false);

    }
    /// <summary>
    /// Popup alert when the email has already been used before
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowEmailExistMessage() {
        EmailExistPopUp.SetActive(true);
        yield return new WaitForSeconds(2);
        EmailExistPopUp.SetActive(false);

    }
    /// <summary>
    /// Popup alert when username has already been used before
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowUsernameExistMessage() {
        UsernameExistPopUp.SetActive(true);
        yield return new WaitForSeconds(2);
        UsernameExistPopUp.SetActive(false);

    }
    /// <summary>
    /// Popup alert when password complexity is in not following requirements
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowInvalidPasswordWrongMessage() {
        InvalidPasswordPopUp.SetActive(true);
        yield return new WaitForSeconds(2);
        InvalidPasswordPopUp.SetActive(false);

    }
    /// <summary>
    /// Popup alert when password and confirm password is not the same
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowPasswordWrongMessage() {
        WrongPasswordPopUp.SetActive(true);
        yield return new WaitForSeconds(2);
        WrongPasswordPopUp.SetActive(false);

    }
    /// <summary>
    /// Popup alert when email field is empty
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowEmailNotFillMessage() {
        EmailNotFillPopUp.SetActive(true);
        yield return new WaitForSeconds(2);
        EmailNotFillPopUp.SetActive(false);
    }
    /// <summary>
    /// Popup alert when username field is empty
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowUsernameNotFillMessage() {
        NameNotFillPopUp.SetActive(true);
        yield return new WaitForSeconds(2);
        NameNotFillPopUp.SetActive(false);
    }
    /// <summary>
    /// Popup alert when password field is empty
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowPasswordNotFillMessage() {
        PasswordNotFillPopUp.SetActive(true);
        yield return new WaitForSeconds(2);
        PasswordNotFillPopUp.SetActive(false);
    }
    /// <summary>
    /// Popup alert when confirm password field is empty
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowCfmPassNotFillMessage() {
        CfmPassNotFillPopUp.SetActive(true);
        yield return new WaitForSeconds(2);
        CfmPassNotFillPopUp.SetActive(false);
    }


    // IEnumerator PostRequest(string url, string json)
    // {
    //     var uwr = new UnityWebRequest(url, "GET"); //Should be POST
    //     byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
    //     uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
    //     uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
    //     uwr.SetRequestHeader("Content-Type", "application/json");

    //     //Send the request then wait here until it returns
    //     yield return uwr.SendWebRequest();

    //     if (uwr.isNetworkError)
    //     {
    //         Debug.Log("Error While Sending: " + uwr.error);
    //     }
    //     else
    //     {
    //         Debug.Log("Received: " + uwr.downloadHandler.text);
    //         response = uwr.downloadHandler.text;

    //         UserData player = JsonUtility.FromJson<UserData>(response);

    //         verified = player.getVerified();
    //         if (verified == true)
    //         {

    //             UIController.createAccount();
    //             MainMenuController.setUserData(player);
    //             MainMenuController.loadMainMenu();

    //         }


    //     }

    // }
}
