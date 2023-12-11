using System.Collections.Generic;
using Proyecto26;
using FullSerializer;
using System;

/// <summary>
/// Author: Tan Soo Yong <br/>
/// Database Handler class for SpecialScore
/// </summary>
public static class SpecialScoreDBHandler {
    private static readonly string databaseURL = $"https://engeenur-17baa.firebaseio.com/";
    public static fsSerializer serializer = new fsSerializer();

    /// <summary>
    /// Get all results from SpecialScore
    /// </summary>
    /// <param name="callback">Callback function to retrieve SpecialScore results from database</param>
    public static void GetResults(Action<Dictionary<string, Dictionary<string, int>>> callback) {

        RestClient.Get($"{databaseURL}specialScore.json").Then(resCourse => {
            var jsonRes = resCourse.Text;

            var data = fsJsonParser.Parse(jsonRes);
            object deserialized = null;
            // Deserialize to Value: object as I only require Key: course
            serializer.TryDeserialize(data, typeof(Dictionary<string, object>), ref deserialized);

            Dictionary<string, object> courseNameDict = deserialized as Dictionary<string, object>;

            Dictionary<string, Dictionary<string, int>> allResults = new Dictionary<string, Dictionary<string, int>>();

            int count = 0;
            foreach (var course in courseNameDict.Keys) {
                RestClient.Get($"{databaseURL}specialScore/{course}.json").Then(resScore => {
                    jsonRes = resScore.Text;

                    data = fsJsonParser.Parse(jsonRes);
                    deserialized = null;
                    serializer.TryDeserialize(data, typeof(Dictionary<string, int>), ref deserialized);

                    Dictionary<string, int> scoreDict = deserialized as Dictionary<string, int>;

                    allResults.Add(course, scoreDict);

                    if (++count == courseNameDict.Keys.Count) callback(allResults);
                });
            }
        });
    }



}
