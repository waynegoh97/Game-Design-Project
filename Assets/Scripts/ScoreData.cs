using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ScoreData
{
    public List<int> levelScore = new List<int>();

    public ScoreData(List<int> score)
    {
        levelScore = score;
    }
}
