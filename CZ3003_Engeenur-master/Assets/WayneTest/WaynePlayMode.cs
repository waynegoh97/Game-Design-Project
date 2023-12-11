using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class WaynePlayMode
    {
        [OneTimeSetUp]

        public void LoadScene()
        {
            SceneManager.LoadScene("MainSceneIntergated2");

        }

        [UnityTest]
        public IEnumerator WaynePlayMode_DictionaryFilledWithData()
        {
            bool question = false;
            bool monster = false;

            GameObject stageCanvas = GameObject.Find("StageSelectionCanvas");
            GetDatabaseQuestions stageScript = stageCanvas.GetComponent<GetDatabaseQuestions>();

            stageCanvas.SetActive(true);
            yield return new WaitForSeconds(2);
            //Check dictionary to get data from database
            Debug.Log(stageScript.ques.Count);
            if (stageScript.ques.Count > 0)
            {
                question = true;
            }

            Assert.IsTrue(question);

            /*Debug.Log(stageScript.mons.Count);
            if (stageScript.mons.Count > 0)
            {
                monster = true;
            }

            Assert.IsTrue(monster);*/

        }

        [UnityTest]
        public IEnumerator WaynePlayMode_DictionaryFilledWithSpecialLevelData()
        {
            bool question = false;

            GameObject stageCanvas = GameObject.Find("StageSelectionCanvas");
            CourseSelectionController stageScript = stageCanvas.transform.Find("Normal").Find("Sc1Btn").GetComponent<CourseSelectionController>();

            stageCanvas.SetActive(true);
            yield return new WaitForSeconds(2);

            //Check dictionary to get data from database
            Debug.Log(stageScript.splevel.Count);
            if (stageScript.splevel.Count > 0)
            {
                question = true;
            }

            Assert.IsTrue(question);


        }
    }
}
