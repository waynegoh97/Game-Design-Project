using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StageInfo
{
    public int StageAvailable;
    
    public StageInfo(int StageAvailable)
    {
        this.StageAvailable = StageAvailable;
    }
}
