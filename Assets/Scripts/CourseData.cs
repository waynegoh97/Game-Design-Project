using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CourseData
{
    public string userName;
    public List<string> students = new List<string>();

    public CourseData(string uname, List<string> stud)
    {
        userName = uname;
        students = stud;
    }
}
