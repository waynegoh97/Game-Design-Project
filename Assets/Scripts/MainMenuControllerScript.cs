using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Lee Chong Yu <br></br>
/// A class for handling the Main Menu page and for obtaining and modifying the user information on the client end
/// </summary>
public class MainMenuControllerScript : MonoBehaviour {
    [SerializeField]
    private UnityEngine.UI.Text Name = null;

    [SerializeField]
    private UnityEngine.UI.Text LevelValue = null;

    [SerializeField]
    private UnityEngine.UI.Text ExpValue = null;

    [SerializeField]
    private UnityEngine.UI.Text HealthValue = null;

    [SerializeField]
    private UnityEngine.UI.Text CoinValue = null;

    UserData data = new UserData();

    [SerializeField]
    private UnityEngine.UI.Text TeacherName = null;

    TeacherData teacherData = new TeacherData();
    public TrCourseViewModel trCourseViewModel;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    /// <summary>
    /// To obtain the student information
    /// </summary>
    /// <returns></returns>
    public UserData getUserData() {
        return data;
    }
    /// <summary>
    /// To obtain the teacher information
    /// </summary>
    /// <returns></returns>
    public TeacherData getTeacherData() {
        Debug.Log("1 MainController: " + teacherData.userName);
        return teacherData;
    }
    /// <summary>
    /// To modify the student information
    /// </summary>
    /// <param name="user"></param>
    public void setUserData(UserData user) {
        Debug.Log("SET USER DATA: " + user.userName);
        /* UserData userData = new UserData();
         userData.coin = user.coin;
         userData.email = user.email;
         userData.experience = user.experience;
         userData.hp = user.hp;
         userData.level = user.level;
         userData.localId = user.localId;
         userData.maxExperience = user.maxExperience;
         userData.userName = user.userName;
         userData.verified = user.verified;
         data = userData; */


        data = user; //Object reference not set to instance error 
    }
    /// <summary>
    /// To load into the Main Menu page
    /// </summary>
    public void loadMainMenu() {
        Name.text = data.userName;
        LevelValue.text = data.level.ToString();
        ExpValue.text = data.experience.ToString() + "/" + data.maxExperience.ToString();
        HealthValue.text = data.hp.ToString();
        CoinValue.text = data.coin.ToString();
        /*Name.text = data.getName();
        LevelValue.text = data.getLevel().ToString();
        ExpValue.text = data.getExperience().ToString() + "/" + data.getMaxExperience().ToString();
        HealthValue.text = data.getHp().ToString();
        CoinValue.text = data.getCoin().ToString();*/
    }
    /// <summary>
    /// To modify the teacher information
    /// </summary>
    /// <param name="teacher"></param>
    public void setTeacherData(TeacherData teacher) {
        Debug.Log("SET teacher DATA: " + teacher.userName);
        teacherData = teacher;
    }
    /// <summary>
    /// To load into the Teacher Main Menu page
    /// </summary>
    public void loadTeacherMainMenu() {
        TeacherName.text = teacherData.userName;//teacherData.getName();
        trCourseViewModel.userName = teacherData.userName;
    }
    /// <summary>
    /// To obtain teacher username
    /// </summary>
    /// <returns></returns>
    public string getTeacherUserNameData() {
        //Debug.Log("2 MainController: " + teacherUserName);
        return teacherData.userName;
    }
}
