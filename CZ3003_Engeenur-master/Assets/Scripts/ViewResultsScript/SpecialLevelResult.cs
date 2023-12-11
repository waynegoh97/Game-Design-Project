using System;
using System.Collections.Generic;

[Serializable]
public class SpecialLevelResult
{
    public string level;
    public List<UserResult> userResults;

    [Serializable]
    public class UserResult {
        public string username;
        public string score;
        public string time;
    } 

    public SpecialLevelResult(string level, List<UserResult> userResults) {
        this.level = level;
        this.userResults = userResults;
    }
}
