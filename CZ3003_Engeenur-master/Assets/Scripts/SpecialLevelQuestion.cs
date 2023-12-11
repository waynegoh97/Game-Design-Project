using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpecialLevelQuestion
{
    public List<int> ans = new List<int>();
    public List<string> qns = new List<string>();
    public int level;
    public string courseName;

    public SpecialLevelQuestion(List<int> answer, List<string> question, int lvl, string cName)
    {
        ans = answer;
        qns = question;
        level = lvl;
        courseName = cName;
    }
}
