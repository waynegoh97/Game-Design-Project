using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author: Tan Soo Yong <br/>
/// UI Handler class for Viewing Special Level Results
/// </summary>
public class ViewResultUI : MonoBehaviour
{
    public ViewResultController ctrl;

    public GameObject resultTemplate;
    public Dropdown dropdown;
    private List<GameObject> instantiatedUI = new List<GameObject>();

    private Dictionary<string, Dictionary<string, int>> allResults;

    /// <summary>
    /// MonoBehaviour inherited method. Called when UI becomes enabled and active
    /// </summary>
    void OnEnable() {
        Init();
    }

    /// <summary>
    /// Initialization method when UI is active
    /// </summary>
    private void Init() {
        ctrl.GetResults(allResults => {
            this.allResults = allResults;
            PopulateCourseInDropdown();
        });
    }

    /// <summary>
    /// Populate courses in dropdown UI
    /// </summary>
    private void PopulateCourseInDropdown() {
        dropdown.ClearOptions();

        foreach (string course in allResults.Keys) {
            dropdown.options.Add(new Dropdown.OptionData(course));
        }
        dropdown.RefreshShownValue();

        // Trigger OnValueChange Event to PopulateLevelResults(0)
        OnLevelSelected(dropdown);

        dropdown.onValueChanged.AddListener(delegate {
            OnLevelSelected(dropdown);
        });
    }

    /// <summary>
    /// OnClick Handler for selecting dropdown option
    /// </summary>
    /// <param name="dropdown">Dropdown GameObject</param>
    private void OnLevelSelected(Dropdown dropdown) {
        int index = dropdown.value;
        string course = dropdown.options[index].text;
        PopulateScores(course);
    }

    /// <summary>
    /// Dynamically populate results in UI for the course
    /// </summary>
    /// <param name="course">Course name</param>
    private void PopulateScores(string course) {
        foreach (GameObject item in instantiatedUI) {
            Destroy(item);
        }
        instantiatedUI.Clear();

        Dictionary<string, int> scores = allResults[course];

        // Instantiate new results
        foreach (string user in scores.Keys) {
            GameObject resultRow = Instantiate<GameObject>(resultTemplate, transform);
            resultRow.transform.GetChild(0).GetComponent<Text>().text = user;
            resultRow.transform.GetChild(1).GetComponent<Text>().text = scores[user].ToString();
            resultRow.SetActive(true);
            instantiatedUI.Add(resultRow);
        }
    }

    /// <summary>
    /// Getter method for list of dynamically created results UI
    /// </summary>
    /// <returns>List of dynamically created results UI</returns>
    public List<GameObject> getInstantiatedUI() {
        return this.instantiatedUI;
    }
}
