using System.Collections.Generic;
using FullSerializer;
using Proyecto26;
using System.Linq;

/// <summary>
/// Author: Ang Hao Jie <br/>
/// Database handler for leadership board to conduct CRUD.
/// </summary>
public static class LeadershipDBHandler
{
    private static readonly string databaseURL = $"https://engeenur-17baa.firebaseio.com/";

    private static fsSerializer serializer = new fsSerializer();

    public delegate void GetLeadershipCallback(List<KeyValuePair<string, TheScore>> itemDict);

    // public static void GetLeadershipRanking(GetLeadershipCallback callback) 
    // {
    //     RestClient.Get($"{databaseURL}score.json").Then(res => 
    //     {
    //         // parsing JSON into Item object
    //         var responseJson = res.Text;
    //         var data = fsJsonParser.Parse(responseJson);
    //         object deserialized = null;
    //         serializer.TryDeserialize(data, typeof(Dictionary<string, TheScore>), ref deserialized);

    //         var scoreDict = deserialized as Dictionary<string, TheScore>;
            
    //         var scoreList = scoreDict.OrderByDescending(t => t.Value.levelNo).ThenByDescending(t => t.Value.score).ToList();

    //         callback(scoreList);
    //     });
    // }

    /// <summary>
    /// Read leadership board global ranking from the database.
    /// </summary>
    /// <param name="callback">The callback method to be called after obtaining the data from database due to coroutine.</param>
    public static void GetLeadershipRanking(GetLeadershipCallback callback) 
    {
        RestClient.Get($"{databaseURL}score.json").Then(res => 
        {
            // parsing JSON into Item object
            var responseJson = res.Text;
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, TheScore>), ref deserialized);

            var scoreDict = deserialized as Dictionary<string, TheScore>;
            
            var scoreList = scoreDict.OrderByDescending(t => t.Value.levelScore.Count).
                ThenByDescending(t => t.Value.levelScore[t.Value.levelScore.Count - 1]).ToList();

            callback(scoreList);
        });
    }
}