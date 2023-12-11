using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControllerScript : MonoBehaviour {
    public GameObject loginCanvas;
    public GameObject loginTrCanvas;
    public GameObject createAccountCanvas;
    public GameObject townCanvas;
    public GameObject inventoryCanvas;
    public GameObject levelSelectCanvas;
    public GameObject shopCanvas;
    public GameObject shopItemCanvas;
    public GameObject battleCanvas;
    public GameObject shopScrollView;
    public GameObject inventoryScrollView;
    public GameObject questionUI;
    public GameObject battleMenu;
    public GameObject friendCanvas;
    public GameObject leadershipBoardCanvas;
    public GameObject teacherTownCanvas;
    public GameObject questionCanvas;
    public GameObject viewResultsCanvas;
    public GameObject courseCanvas;
    public GameObject specialLevelCanvas;
    public GameObject trQnCanvas;
    public GameObject enrollCanvas;
    public GameObject mainCanvas;
    public GameObject stageSelectionNormalCanvas;
    public GameObject stageSelectionCloseCanvas;
    public GameObject stageSelectSpecialCanvas;
    public GameObject stageSelectedCanvas;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    /// <summary>
    /// To switch form login page to main menu page
    /// </summary>
    public void loginButton() {
        loginCanvas.SetActive(false);
        townCanvas.SetActive(true);

    }
    /// <summary>
    /// To switch from teacher login page to main menu page
    /// </summary>
    public void teacherLoginButton() {
        loginTrCanvas.SetActive(false);
        teacherTownCanvas.SetActive(true);

    }
    /// <summary>
    /// To switch from login page to create account page
    /// </summary>
    public void gotoCreateAccountButton() {
        loginCanvas.SetActive(false);
        createAccountCanvas.SetActive(true);

    }
    /// <summary>
    /// To switch from create account page to main menu page
    /// </summary>
    public void createAccount() {
        createAccountCanvas.SetActive(false);
        townCanvas.SetActive(true);

    }
    /// <summary>
    /// To switch from main menu page to inventory page
    /// </summary>
    public void OpenInventoryButton() {
        townCanvas.SetActive(false);
        inventoryCanvas.SetActive(true);
    }
    /// <summary>
    /// To switch from inventory page to main menu page
    /// </summary>
    public void CloseInventoryButton() {
        inventoryCanvas.SetActive(false);
        townCanvas.SetActive(true);
    }
    /// <summary>
    /// To switch from main menu page to level selection page
    /// </summary>
    public void OpenLevelSelectButton() {
        townCanvas.SetActive(false);
        levelSelectCanvas.SetActive(true);
    }
    /// <summary>
    /// To switch from level selection page to main menu page
    /// </summary>
    public void CloseLevelSelectButton() {
        levelSelectCanvas.SetActive(false);
        townCanvas.SetActive(true);
    }
    /// <summary>
    /// To switch from main menu page to shop page
    /// </summary>
    public void OpenShopButton() {
        townCanvas.SetActive(false);
        shopItemCanvas.SetActive(true);
    }
    /// <summary>
    /// To switch from shop page to main menu page
    /// </summary>
    public void CloseShopButton() {
        shopItemCanvas.SetActive(false);
        townCanvas.SetActive(true);
    }
    /// <summary>
    /// To switch from main menu page to friend list page
    /// </summary>
    public void OpenFriendListButton() {
        townCanvas.SetActive(false);
        friendCanvas.SetActive(true);
    }
    /// <summary>
    /// To switch from friend list page to main menu page
    /// </summary>
    public void CloseFriendListButton() {
        friendCanvas.SetActive(false);
        townCanvas.SetActive(true);
    }
    /// <summary>
    /// To switch from teacher main menu page to friend list page
    /// </summary>
    public void OpenTeacherFriendListButton() {
        teacherTownCanvas.SetActive(false);
        friendCanvas.SetActive(true);
    }
    /// <summary>
    /// To switch from friend list page to teacher main menu page
    /// </summary>
    public void CloseTeacherFriendListButton() {
        friendCanvas.SetActive(false);
        teacherTownCanvas.SetActive(true);
    }
    /// <summary>
    /// To switch from main menu page to leadership board page
    /// </summary>
    public void OpenLeadershipBoardButton() {
        townCanvas.SetActive(false);
        leadershipBoardCanvas.SetActive(true);
    }
    /// <summary>
    /// To switch from main menu page to leadership board page
    /// </summary>
    public void OpenTeacherLeadershipBoardButton() {
        teacherTownCanvas.SetActive(false);
        leadershipBoardCanvas.SetActive(true);
    }
    /// <summary>
    /// To switch from leadership board page to teacher main menu page
    /// </summary>
    public void CloseTeacherLeadershipBoardButton() {
        leadershipBoardCanvas.SetActive(false);
        teacherTownCanvas.SetActive(true);
    }
    /// <summary>
    /// To switch from leadership board page to main menu page
    /// </summary>
    public void CloseLeadershipBoardButton() {
        leadershipBoardCanvas.SetActive(false);
        townCanvas.SetActive(true);

    }
    /// <summary>
    /// To switch from inventory page to shop page
    /// </summary>
    public void OpenShopScrollView() {
        inventoryScrollView.SetActive(false);
        shopScrollView.SetActive(true);
    }
    /// <summary>
    /// To switch from shop page to inventory page
    /// </summary>
    public void OpenInventoryScrollView() {
        inventoryScrollView.SetActive(true);
        shopScrollView.SetActive(false);
    }
    /// <summary>
    /// To switch from battle menu page to the question page
    /// </summary>
    public void OpenQuestionUI() {
        questionUI.SetActive(true);
        battleMenu.SetActive(false);
    }
    /// <summary>
    /// To switch to the battle system page
    /// </summary>
    public void OpenBattleCanvas() {
        battleCanvas.SetActive(true);
        townCanvas.SetActive(false);
        stageSelectedCanvas.SetActive(false);
    }
    /// <summary>
    /// to switch from teacher main menu page to question page
    /// </summary>
    public void OpenCreateLevelCanvas() {
        questionCanvas.SetActive(true);
        teacherTownCanvas.SetActive(false);
    }
    /// <summary>
    /// To switch from teacher main menu page to view results page
    /// </summary>
    public void OpenViewResultsCanvas() {
        viewResultsCanvas.SetActive(true);
        teacherTownCanvas.SetActive(false);
    }
    /// <summary>
    /// To switch from view results page to teacher main menu page
    /// </summary>
    public void CloseViewResultsCanvas() {
        viewResultsCanvas.SetActive(false);
        teacherTownCanvas.SetActive(true);
    }
    /// <summary>
    /// To switch from enrollment page to course page
    /// </summary>
    public void EnrollmentToCourseCanvas() {
        enrollCanvas.SetActive(false);
        courseCanvas.SetActive(true);
    }
    /// <summary>
    /// To switch from course page to enrollment page
    /// </summary>
    public void CourseToEnrollmentCanvas() {
        courseCanvas.SetActive(false);
        enrollCanvas.SetActive(true);

    }
    /// <summary>
    /// To switch from enrollment page to special level selection page
    /// </summary>
    public void EnrollmentToSpecialLevelCanvas() {
        enrollCanvas.SetActive(false);
        specialLevelCanvas.SetActive(true);
    }
    /// <summary>
    /// To switch from special level selection page to enrollment page
    /// </summary>
    public void SpecialLevelToEnrollmentCanvas() {
        specialLevelCanvas.SetActive(false);
        enrollCanvas.SetActive(true);
    }
    /// <summary>
    /// To switch from special level page to teacher questions page
    /// </summary>
    public void OpenTrQnCanvas() {
        specialLevelCanvas.SetActive(false);
        trQnCanvas.SetActive(true);
    }
    /// <summary>
    /// To switch from teacher questions page to special level page
    /// </summary>
    public void CloseTrQnCanvas() {
        trQnCanvas.SetActive(false);
        specialLevelCanvas.SetActive(true);
    }
    /// <summary>
    /// To switch from teacher questions page to the course page
    /// </summary>
    public void TrQnToCourseCanvas() {
        trQnCanvas.SetActive(false);
        courseCanvas.SetActive(true);
    }
    /// <summary>
    /// To switch from the battle system page to the main menu page
    /// </summary>
    public void CloseBattleCanvas() {
        battleCanvas.SetActive(false);
        townCanvas.SetActive(true);
    }
    /// <summary>
    /// To switch from main page to login page
    /// </summary>
    public void ProceedToLoginCanvas() {
        mainCanvas.SetActive(false);
        loginCanvas.SetActive(true);
    }
    /// <summary>
    /// To switch from main page to create account page
    /// </summary>
    public void ProceedToCreateAccCanvas() {
        mainCanvas.SetActive(false);
        createAccountCanvas.SetActive(true);
    }
    /// <summary>
    /// To switch from login page to main page
    /// </summary>
    public void LoginToMainCanvas() {
        loginCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }
    /// <summary>
    /// To switch from teacher login page to main page
    /// </summary>
    public void LoginTrToMainCanvas() {
        loginTrCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }
    /// <summary>
    /// To switch from create account page to main menu page
    /// </summary>
    public void CreateAccToMainCanvas() {
        createAccountCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }
    /// <summary>
    /// To switch from login page to teacher login page
    /// </summary>
    public void SelectIAmATeacher() {
        loginTrCanvas.SetActive(true);
        loginCanvas.SetActive(false);

    }
    /// <summary>
    /// To switch from teacher login page to login page
    /// </summary>
    public void SelectIAmAStudent() {
        loginCanvas.SetActive(true);
        loginTrCanvas.SetActive(false);

    }
    /// <summary>
    /// To switch to special stages page
    /// </summary>
    public void StageSpecialCanvas() {
        stageSelectionNormalCanvas.SetActive(false);
        stageSelectSpecialCanvas.SetActive(true);
        stageSelectionCloseCanvas.SetActive(true);
    }
    /// <summary>
    /// To switch to normal stages page
    /// </summary>
    public void StageNormalCanvas() {
        stageSelectSpecialCanvas.SetActive(false);
        stageSelectionNormalCanvas.SetActive(true);
        stageSelectionCloseCanvas.SetActive(true);
    }
    /// <summary>
    /// To close stages page
    /// </summary>
    public void StageSelectionCloseCanvas() {
        stageSelectedCanvas.SetActive(false);
    }
    /// <summary>
    /// To switch to battle system page
    /// </summary>
    public void StageSelectedCanvas() {
        stageSelectedCanvas.SetActive(false);
        townCanvas.SetActive(false);
        battleCanvas.SetActive(true);
    }
    /// <summary>
    /// To switch the stage selection page
    /// </summary>
    public void StageSelectionCanvasOpen() {
        stageSelectedCanvas.SetActive(true);
    }
    /// <summary>
    /// To switch from teacher main menu page to courses page
    /// </summary>
    public void TrTownToCourse() {

        teacherTownCanvas.SetActive(false);
        courseCanvas.SetActive(true);
    }
    /// <summary>
    /// To switch from courses page to teacher main menu page
    /// </summary>
    public void CourseToTrTown() {
        courseCanvas.SetActive(false);
        teacherTownCanvas.SetActive(true);
    }
}
