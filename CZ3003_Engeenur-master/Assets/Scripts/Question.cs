using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Question
{
    public int Answer;
    public string Questions;

    public Question()
    {
    }

    public Question(int answer, string questions)
    {
        Questions = questions;
        Answer = answer;
    }


}
