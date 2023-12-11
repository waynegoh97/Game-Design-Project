using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using UnityEngine.Serialization;
using UnityEngine.UI;
using FullSerializer;
using LitJson;
using System.Threading.Tasks;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

/// <summary>
/// Author: Huang Chao Shan <br></br>
/// A class for handling the signup and login through Firebase authentication for the teacher
/// </summary>
public class TeacherLoginHandler : MonoBehaviour {
    private string AuthKey = "AIzaSyAM0B744pa-v9FjU69DmlfQGMqiZAHJpUo";
    public static fsSerializer serializer = new fsSerializer();
    public string userName;
    private string idToken;
    public static string localId;
    private string getLocalId;
    public string verified;
    TeacherData teacherData = new TeacherData();
    private string databaseURL = "https://engeenur-17baa.firebaseio.com/teachers/";
    private LoginControllerScript loginControllerScript;
    private bool signInResult;
    private string localID;


    /* public async Task<bool> FetchTeacherSignInInfo(string email, string password)
     {
        signInResult = true;
        await SignInTeacher(email, password);
        if (signInResult == false){
            return false;
        }else{
            return true;
        }
     }*/
    /// <summary>
    /// Function to obtain localID after authentication
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public async Task<string> FetchTeacherSignInInfo(string email, string password) {
        localID = null;
        await SignInTeacher(email, password);
        return localID;
    }
    /// <summary>
    /// Function to obtain teacher data from the Firebase Authentication
    /// </summary>
    private void RetrieveFromDB() {
        RestClient.Get(databaseURL + "/" + getLocalId + ".json?auth=" + idToken).Then(response => {
            var responseJson = response.Text;
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, TeacherData>), ref deserialized);
            var teacherData = deserialized as Dictionary<string, TeacherData>;
            Debug.Log(teacherData);
            //loginControllerScript.
        });
    }
    /// <summary>
    /// Function to authenticate the teacher credential in the database
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    private async Task SignInTeacher(string email, string password) {
        string teacherData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
        RestClient.Post<SignResponse>("https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=" + AuthKey, teacherData).Then(
            response => {
                idToken = response.idToken;
                localId = response.localId;
                GetTeachername();
                localID = response.localId; //get the localId
            }).Catch(error => {
                localID = null;
                //signInResult = false;
                Debug.Log(error);
            });
        Stopwatch sw = Stopwatch.StartNew();
        var delay = Task.Delay(1000).ContinueWith(_ =>
        {
            sw.Stop();
            return sw.ElapsedMilliseconds;
        });
        await delay;
        int sec = (int)delay.Result;
        Debug.Log("Time elapsed milliseconds: {0}" + sec);
    }
    /// <summary>
    /// Function to obtain the teacher name from the database
    /// </summary>
    private void GetTeachername() {
        RestClient.Get<UserData>(databaseURL + "/" + localId + ".json?auth=" + idToken).Then(response => {
            userName = response.userName;
            GetTeacherLocalId();
            Debug.Log("here2");
        });
    }
    /// <summary>
    /// Function to obtain the teacher local id from the database
    /// </summary>
    private void GetTeacherLocalId() {
        RestClient.Get(databaseURL + "/" + ".json?auth=" + idToken).Then(response =>
        {
            var username = userName;

            fsData teacherData = fsJsonParser.Parse(response.Text);
            Dictionary<string, TeacherData> teachers = null;
            serializer.TryDeserialize(teacherData, ref teachers);

            foreach (var teacher in teachers.Values) {
                if (teacher.userName == username) {
                    getLocalId = teacher.localId;
                    RetrieveFromDB();
                    break;
                }
            }
        }).Catch(error => {
            Debug.Log(error);
        });
    }
    /// <summary>
    /// Function to retrieve the Teacher information from the database
    /// </summary>
    /// <param name="teacher"></param>
    //Retreive Teacher
    public delegate void GetTeacherDataCallback(TeacherData teacher);
    public static void GetTeacher(string localId, GetTeacherDataCallback callback) {
        RestClient.Get<TeacherData>("https://engeenur-17baa.firebaseio.com/teachers/" + localId + ".json").Then(teacher => { callback(teacher); });
    }

    //Temp
    /// <summary>
    /// Backend function to create an account for the teacher
    /// </summary>
    /// <param name="email"></param>
    /// <param name="username"></param>
    /// <param name="password"></param>
    public void FetchTeacherSignUpInfo(string email, string username, string password) {
        Debug.Log("ENTER FETCH1");
        SignUpTeacher(email, username, password);
    }
    /// <summary>
    /// Bsckend function to create an account for the teacher in the Firebase Authentication
    /// </summary>
    /// <param name="email"></param>
    /// <param name="username"></param>
    /// <param name="password"></param>
    private void SignUpTeacher(string email, string username, string password) {
        Debug.Log("signup");
        string teacherData = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
        Debug.Log("userData");
        RestClient.Post<SignResponse>("https://identitytoolkit.googleapis.com/v1/accounts:signUp?key=" + AuthKey, teacherData).Then(
            response => {
                idToken = response.idToken;
                localId = response.localId;
                userName = username;
                email = email;
                Debug.Log("bp");
                PostToDatabase(true, userName, email);

            }).Catch(error => {
                //verified = false;
                Debug.Log(error);

            });
    }
    /// <summary>
    /// Set the default information of a teacher upon account creation
    /// </summary>
    /// <param name="verified"></param>
    /// <param name="username"></param>
    /// <param name="email"></param>
    private void PostToDatabase(bool verified = false, string username = null, string email = null) {
        TeacherData teacherData = new TeacherData();
        Debug.Log("1");
        if (verified) {
            teacherData.email = email;
            teacherData.localId = localId;
            teacherData.userName = username;
            teacherData.verified = true;
        }
        Debug.Log("2");
        //verify();

        RestClient.Put(databaseURL + "/" + localId + ".json?auth=" + idToken, teacherData);

    }
}

