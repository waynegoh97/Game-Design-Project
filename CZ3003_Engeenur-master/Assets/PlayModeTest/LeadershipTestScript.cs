using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using System.Runtime.ExceptionServices;
using UnityEngine.SceneManagement;
using System;
using Proyecto26;

namespace Tests
{
    /// <summary>
    /// Author: Ang Hao Jie <br/>
    /// Test cases for Leadership Board UI
    /// </summary>
    public class LeadershipTestScript
    {
        string databaseURL = $"https://engeenur-17baa.firebaseio.com/";

        /// <summary>
        /// Test case for TC16 Update of leadership board details (User).
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator TC16UpdateLeadershipboardDetails_User()
        {
            bool foundUser = false;
            int userOriginalLevel = 1;

            // set dummy data for user account
            UserData userData = new UserData();
            userData.userName = "peter";
            GameObject mainMenuGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/MainMenuController"));
            MainMenuControllerScript mainMenuControllerScript = mainMenuGameObject.GetComponent<MainMenuControllerScript>();
            mainMenuControllerScript.setUserData(userData);

            // initialized leadership board canvas and get the view model script attached to it
            GameObject leadershipGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/LeadershipBoardCanvas"));
            LeadershipViewModel leadershipViewModel = leadershipGameObject.transform.Find("Backboard").Find("RankingScrollView").
                Find("Scroll Rect").Find("RankingContent").GetComponent<LeadershipViewModel>();
            leadershipViewModel.mainMenuControllerScript = mainMenuControllerScript;

            // upload dummy data for peter
            RestClient.Put($"{databaseURL}score/{userData.userName}/levelScore/1.json", 5.ToString());

            yield return new WaitForSeconds(2);

            // stimulate enter leadershipboard page
            leadershipGameObject.SetActive(true);

            yield return new WaitForSeconds(2);
            
            // iterate thru the UI table via each row to see if can find the user's username means his result can be found in the leadershipboard
            foreach (GameObject item in leadershipViewModel.getInstantiatedUI())
            {
                if (item.transform.GetChild(1).GetComponent<Text>().text.Equals(userData.userName))
                {
                    foundUser = true;
                    userOriginalLevel = Int32.Parse(item.transform.GetChild(2).GetComponent<Text>().text);
                    // since found user, no point looping further
                    break;
                }
            }

            // check if found user in leadershipboard
            Assert.IsTrue(foundUser);


            // stimulate leaving leadershipboard page
            leadershipGameObject.SetActive(false);

            // upload dummy data for peter as if he completed new level
            // using userOriginalLevel to increment incase already have "peter" in database and his original score might be even higher than value of 1
            RestClient.Put($"{databaseURL}score/{userData.userName}/levelScore/{userOriginalLevel + 1}.json", 5.ToString());

            yield return new WaitForSeconds(2);

            // stimulate enter leadershipboard page
            leadershipGameObject.SetActive(true);

            yield return new WaitForSeconds(2);
            
            // reset boolean variable
            foundUser = false;
            // iterate thru the UI table via each row to see if can find the user's username means his result can be found in the leadershipboard
            foreach (GameObject item in leadershipViewModel.getInstantiatedUI())
            {
                // check if found user
                if (item.transform.GetChild(1).GetComponent<Text>().text.Equals(userData.userName))
                {
                    Assert.AreNotEqual(userOriginalLevel, Int32.Parse(item.transform.GetChild(2).GetComponent<Text>().text));
                    foundUser = true;
                }
            }

            // check if found user in leadershipboard
            Assert.IsTrue(foundUser);
        }

        /// <summary>
        /// Test case for UC17 Update of leadership board details (Other user).
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator TC17UpdateLeadershipboardDetails_OtherUser()
        {
            bool foundOtherUser = false;
            int otherUserOriginalLevel = 1;
            string jane = "jane";

            // set dummy data for user account
            UserData userData = new UserData();
            userData.userName = "peter";
            GameObject mainMenuGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/MainMenuController"));
            MainMenuControllerScript mainMenuControllerScript = mainMenuGameObject.GetComponent<MainMenuControllerScript>();
            mainMenuControllerScript.setUserData(userData);

            // initialized leadership board canvas and get the view model script attached to it
            GameObject leadershipGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/LeadershipBoardCanvas"));
            LeadershipViewModel leadershipViewModel = leadershipGameObject.transform.Find("Backboard").Find("RankingScrollView").
                Find("Scroll Rect").Find("RankingContent").GetComponent<LeadershipViewModel>();
            leadershipViewModel.mainMenuControllerScript = mainMenuControllerScript;

            // upload dummy data for jane
            RestClient.Put($"{databaseURL}score/{jane}/levelScore/1.json", 5.ToString());

            yield return new WaitForSeconds(2);

            // stimulate enter leadershipboard page
            leadershipGameObject.SetActive(true);

            yield return new WaitForSeconds(2);
            
            // iterate thru the UI table via each row to see if can find the user's username means his result can be found in the leadershipboard
            foreach (GameObject item in leadershipViewModel.getInstantiatedUI())
            {
                if (item.transform.GetChild(1).GetComponent<Text>().text.Equals(jane))
                {
                    foundOtherUser = true;
                    otherUserOriginalLevel = Int32.Parse(item.transform.GetChild(2).GetComponent<Text>().text);
                    // since found user, no point looping further
                    break;
                }
            }

            // check if found other user (jane) in leadershipboard
            Assert.IsTrue(foundOtherUser);


            // stimulate leaving leadershipboard page
            leadershipGameObject.SetActive(false);

            // upload dummy data for other user (jane) as if she completed new level
            // using userOriginalLevel to increment incase already have other user (jane) in database and her original score might be even higher than value of 1
            RestClient.Put($"{databaseURL}score/{jane}/levelScore/{otherUserOriginalLevel + 1}.json", 5.ToString());

            yield return new WaitForSeconds(2);

            // stimulate enter leadershipboard page
            leadershipGameObject.SetActive(true);

            yield return new WaitForSeconds(2);
            
            // reset boolean variable
            foundOtherUser = false;
            // iterate thru the UI table via each row to see if can find the other user (jane)'s username means her result can be found in the leadershipboard
            foreach (GameObject item in leadershipViewModel.getInstantiatedUI())
            {
                // check if found other user (jane)
                if (item.transform.GetChild(1).GetComponent<Text>().text.Equals(jane))
                {
                    Assert.AreNotEqual(otherUserOriginalLevel, Int32.Parse(item.transform.GetChild(2).GetComponent<Text>().text));
                    foundOtherUser = true;
                }
            }

            // check if found other user (jane) in leadershipboard
            Assert.IsTrue(foundOtherUser);
        }
    }
}
