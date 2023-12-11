using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class StageQuestion
{
    public string Questions;
    public int Answer;

    public StageQuestion(int answer, string questions)
    {
        Questions = questions;
        Answer = answer;
    }
}
