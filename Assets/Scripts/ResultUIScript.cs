using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// <br>Author: Tan Boon Ping</br> 
/// Storing of results of battle
/// </summary>
public class ResultUIScript : MonoBehaviour
{
    public Text score;
    public Text gold;
    public Text experience;

    /// <summary>
    /// set text ui
    /// </summary>
    public void setResults(int _score, int _gold, int _experience)
    {
        score.text += _score.ToString();
        gold.text += _gold.ToString();
        experience.text += _experience.ToString();
    }

    /// <summary>
    /// reset text ui
    /// </summary>
    public void reset()
    {
        score.text = "Score: ";
        gold.text = "Gold: ";
        experience.text = "Experience: ";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
