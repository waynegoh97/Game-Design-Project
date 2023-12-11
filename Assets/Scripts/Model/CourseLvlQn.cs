using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
///<summary>
///Author: Lim Pei Yan <br/>
///CourseLvlQn Model 
///</summary>
[Serializable]
public class CourseLvlQn 
{
    public string courseName;
    public int level;
    public List<string>qns;
    public List<int>ans;

    ///<summary>
    ///Initialize a new instance of CourseLvlQn
    ///</summary> 
    ///<param name = "courseName">Name of course</param>
    ///<param name = "level">Level created for the course</param>
    ///<param name = "qnsList">List containing all the questions for the level in the course</param>
    ///<param name = "ansList">List containing all the answers for the level in the course</param>
    public CourseLvlQn(string courseName, int level, List<string>qnsList, List<int>ansList){
        this.courseName = courseName;
        this.level = level;
        qns = new List<string>(qnsList);
        ans = new List<int>(ansList);
    }
}
