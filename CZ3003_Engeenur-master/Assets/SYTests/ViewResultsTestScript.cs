using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using Proyecto26;
using Random = System.Random;
using System.Linq;

namespace Tests
{
    /// <summary>
    /// Author: Tan Soo Yong <br/>
    /// Test Script for View Special Level Results
    /// </summary>
    public class ViewResultsTestScript {
        string databaseURL = $"https://engeenur-17baa.firebaseio.com/";

        // new data
        string course;
        string newUser;
        int newScore;

        [SetUp]
        public void Setup() {
            // generate new data
            this.course = "testCourse";
            this.newUser = RandomString(10);
            this.newScore = RandomNumber(0, 100);
        }

        /// <summary>
        /// TC-23 ViewLevelResults
        /// </summary>
        [UnityTest]
        public IEnumerator TC23ViewLevelResults() {
            GameObject canvas = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/ViewResultsCanvas"));
            // simulate enter viewResults
            canvas.SetActive(true);
            yield return new WaitForSeconds(3); // wait for reading database

            ViewResultUI uiScript = canvas.transform.Find("ShadedBackground").Find("ResultsWindow").Find("Scroll View (with Items)").Find("Scroll Rect").Find("Content").GetComponent<ViewResultUI>();
            //ViewResultUI uiScript = canvas.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetComponent<ViewResultUI>();

            Dropdown dropdown = uiScript.dropdown;
            selectDropdown(dropdown); // select course in dropdown to populateResults

            // traverse results to ensure new data does not exist before updating db
            List<GameObject> instantiatedUI = uiScript.getInstantiatedUI();
            foreach (var resultRow in instantiatedUI) {
                string displayedUser = resultRow.transform.GetChild(0).GetComponent<Text>().text;
                string displayedScore = resultRow.transform.GetChild(1).GetComponent<Text>().text;
                // new data not added yet
                Assert.AreNotEqual(newUser, displayedUser);
                Assert.AreNotEqual(newScore.ToString(), displayedScore);
            }

            // simulate exit viewResults
            canvas.SetActive(false);

            // Update database with course, newUser & newScore
            RestClient.Put($"{databaseURL}specialScore/{course}/{newUser}.json", newScore.ToString());

            // simulate enter viewResults
            canvas.SetActive(true);
            yield return new WaitForSeconds(3); // wait for reading database

            selectDropdown(dropdown); // select course in dropdown to populateResults

            // traverse results to check that new data is added
            instantiatedUI = uiScript.getInstantiatedUI();
            foreach (var resultRow in instantiatedUI) {
                string displayedUser = resultRow.transform.GetChild(0).GetComponent<Text>().text;
                string displayedScore = resultRow.transform.GetChild(1).GetComponent<Text>().text;
                if (displayedUser == newUser) {
                    // new data is added
                    Assert.AreEqual(newUser, displayedUser);
                    Assert.AreEqual(newScore.ToString(), displayedScore);
                    break;
                }
            }
        }

        public void selectDropdown(Dropdown dropdown) {
            // Traverse dropdown
            for (int i = 0; i < dropdown.options.Count; i++) {
                if (dropdown.options[i].text != course) {
                    continue;
                } else {
                    // select dropdown for course updated (at index i)
                    dropdown.value = i;
                }
            }
        }


        #region Helper methods
        public string RandomString(int length) {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public int RandomNumber(int min, int max) {
            Random random = new Random();
            return random.Next(min, max);
        }
        #endregion
    }

}
