using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///<summary>
///Author: Lim Pei Yan <br/>
///Course Model 
///</summary>
[Serializable]
public class Course
{
    public string userName;
    public List<string>students;

    ///<summary>
    ///Initialize a new instance of Course
    ///</summary> 
    ///<param name = "userName">Username of login teacher</param>
    ///<param name = "studsList">A list of students enrolled in the Course</param>
    public Course(string userName, List<string>studsList)
    {
        this.userName = userName;
        students = new List<string>(studsList);
    }
}