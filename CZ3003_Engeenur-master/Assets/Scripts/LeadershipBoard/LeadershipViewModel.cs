using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

/// <summary>
/// Author: Ang Hao Jie <br/>
/// Controller for Leadershipboard UI
/// </summary>
public class LeadershipViewModel : MonoBehaviour
{
    private List<KeyValuePair<string, TheScore>> scoreList;
    private List<GameObject> instantiatedUI = new List<GameObject>();
    public MainMenuControllerScript mainMenuControllerScript;
    public GameObject scoreRowTemplate;
    private UserData userData;

    /// <summary>
    /// Executes whenever user navigates to the Inventory page.
    /// </summary>
    void OnEnable() 
    {
        Init();
    }

    /// <summary>
    /// Fetch required data from database and set the values on the UI.
    /// </summary>
    public void Init() 
    {
        this.userData = this.mainMenuControllerScript.getUserData();

        LeadershipDBHandler.GetLeadershipRanking(scoreList => {
            // set to local dictionary of scores
            this.scoreList = scoreList;
            // display list of score in leadership board on the UI
            this.populateLeadershipboardRows();
        });
    }

    /// <summary>
    /// Display rows of best ranking details and in ranking order on the UI.
    /// </summary>
    private void populateLeadershipboardRows() 
    {
        uint ranking = 1;

        // Clear Results from previously selected level
        foreach (GameObject item in instantiatedUI) 
        {
            Destroy(item);
        }
        instantiatedUI.Clear();

        // Instantiate new results
        foreach (KeyValuePair<string, TheScore> score in scoreList) 
        {
            GameObject resultRow = Instantiate<GameObject>(this.scoreRowTemplate, transform);
            resultRow.SetActive(true);  // template was set to hidden so all duplicated onces must be set to active
            resultRow.transform.GetChild(0).GetComponent<Text>().text = ranking++.ToString();
            resultRow.transform.GetChild(0).GetComponent<Text>().fontStyle = 0; // set to not bold
            resultRow.transform.GetChild(1).GetComponent<Text>().text = score.Key;
            resultRow.transform.GetChild(1).GetComponent<Text>().fontStyle = 0; // set to not bold
            resultRow.transform.GetChild(2).GetComponent<Text>().text = (score.Value.levelScore.Count - 1).ToString();
            resultRow.transform.GetChild(2).GetComponent<Text>().fontStyle = 0; // set to not bold
            resultRow.transform.GetChild(3).GetComponent<Text>().text = score.Value.levelScore[score.Value.levelScore.Count - 1].ToString();
            resultRow.transform.GetChild(3).GetComponent<Text>().fontStyle = 0; // set to not bold
            instantiatedUI.Add(resultRow);

            // to highlight user's row by changing the text color to red for that row
            if (userData.getName().Equals(score.Key))
            // if ("jerry".Equals(score.Key))    // debug
            {
                for (int i=0; i<4; i++)
                    resultRow.transform.GetChild(i).GetComponent<Text>().color = Color.red;
            }
        }
    }

    /// <summary>
    /// To get the rows of ranking details in the leadership board. <br/>
    /// For test suite.
    /// </summary>
    /// <returns>Instances of the ranking details in the leadership board.</returns>
    public List<GameObject> getInstantiatedUI()
    {
        return this.instantiatedUI;
    }
}
