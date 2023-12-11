using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Tan Soo Yong <br/>
/// Controller class for Viewing Special Level Results
/// </summary>
public class ViewResultController : MonoBehaviour {

    public UIControllerScript uiController;

    /// <summary>
    /// Get Results from database handler SpecialScoreDBHandler
    /// </summary>
    /// <param name="callback">Callback function to retrieve results from SpecialScoreDBHandler</param>
    public void GetResults(Action<Dictionary<string, Dictionary<string, int>>> callback) {
        SpecialScoreDBHandler.GetResults(allResults => {
            callback(allResults);
        });
    }

    /// <summary>
    /// Close View Results Canvas
    /// </summary>
    public void CloseViewResult() {
        uiController.CloseViewResultsCanvas();
    }
}
