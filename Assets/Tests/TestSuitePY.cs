using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using System.Runtime.ExceptionServices;
using UnityEngine.SceneManagement;
using System;

namespace Tests
{
    /// <summary>
    /// Author: Lim Pei Yan <br/>
    /// Unit Test
    /// </summary>
    public class TestSuite
    {

        // A Test behaves as an ordinary method
        [Test]
        public void TestSuiteSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator TestSuiteWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }

        public static GameObject courseCanvas = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/CourseCanvas"));
        // public TrCourseViewModel trCourseViewModel = courseCanvas.transform.Find("CourseViewModel").GetComponent<TrCourseViewModel>();

        ///<summary>
        ///Load the scene
        ///</summary> 
        [OneTimeSetUp]

        public void LoadScene()
        {
            SceneManager.LoadScene("MainSceneIntergated2");
        }

        ///<summary>
        ///Test whether the course can be sucessfully created when the course does not exist in the database
        ///</summary> 
        [UnityTest]
        public IEnumerator CreateCourseNotExist()
        {
            GameObject courseCanvas = GameObject.Find("CourseCanvas");
            TrCourseViewModel trCourseViewModel = courseCanvas.transform.Find("CourseViewModel").GetComponent<TrCourseViewModel>();
            trCourseViewModel.userName = "Mary";
            trCourseViewModel.courseInput.text = "MathTester1000";
            trCourseViewModel.CreateCourse();
            yield return new WaitForSeconds(30);
            Assert.IsTrue(trCourseViewModel.created);
        }

        ///<summary>
        ///Test whether the course cannot be created when the course exist in the database
        ///</summary> 
        [UnityTest]
        public IEnumerator CreateCourseAlrExist()
        {   //GameObject loader = courseCanvas.transform.Find("loader").GetComponent<GameObject>();
            GameObject courseCanvas = GameObject.Find("CourseCanvas");
            TrCourseViewModel trCourseViewModel = courseCanvas.transform.Find("CourseViewModel").GetComponent<TrCourseViewModel>();
            trCourseViewModel.userName = "Mary";
            trCourseViewModel.courseInput.text = "Math100";
            trCourseViewModel.CreateCourse();
            yield return new WaitForSeconds(30);
            Assert.IsFalse(trCourseViewModel.created);
        }

        ///<summary>
        ///Test whether the a student can be enroll into the course if the student is a valid user
        ///</summary> 
        [UnityTest]
        public IEnumerator EnrollValidStud()
        {
            GameObject enrollCanvas = GameObject.Find("EnrollmentCanvas");
            EnrollViewModel enrollViewModel = enrollCanvas.transform.Find("EnrollViewModel").GetComponent<EnrollViewModel>();
            enrollViewModel.userName = "Mary";
            enrollViewModel.courseName = "Math100";
            enrollViewModel.studInput.text = "tanbp";
            enrollViewModel.Read();
            enrollViewModel.CreateStudEnroll();
            yield return new WaitForSeconds(10);
            Assert.IsTrue(enrollViewModel.created);
        }

        ///<summary>
        ///Test whether the a student cannot be enroll into the course if the student is an invalid user
        ///</summary> 
        [UnityTest]
        public IEnumerator EnrollInvalidStud()
        {
            GameObject enrollCanvas = GameObject.Find("EnrollmentCanvas");
            EnrollViewModel enrollViewModel = enrollCanvas.transform.Find("EnrollViewModel").GetComponent<EnrollViewModel>();
            enrollViewModel.userName = "Mary";
            enrollViewModel.courseName = "Math100";
            enrollViewModel.studInput.text = "student999";
            enrollViewModel.Read();
            enrollViewModel.CreateStudEnroll();
            yield return new WaitForSeconds(10);
            Assert.IsFalse(enrollViewModel.created);
        }

        ///<summary>
        ///Test whether the a student can be enroll into the course if the student is already enrolled in the course
        ///</summary> 
        [UnityTest]
        public IEnumerator EnrollEnrolledStud()
        {
            GameObject enrollCanvas = GameObject.Find("EnrollmentCanvas");
            EnrollViewModel enrollViewModel = enrollCanvas.transform.Find("EnrollViewModel").GetComponent<EnrollViewModel>();
            enrollViewModel.userName = "Mary";
            enrollViewModel.courseName = "Math100";
            enrollViewModel.studInput.text = "";
            enrollViewModel.Read();
            enrollViewModel.CreateStudEnroll();
            yield return new WaitForSeconds(10);
            Assert.IsFalse(enrollViewModel.created);
        }

        ///<summary>
        ///Test whether a level can be created successfully
        ///</summary> 
        [UnityTest]
        public IEnumerator CreateLvl()
        {
            GameObject specialCanvas = GameObject.Find("SpecialLevelCanvas");
            TrSpecialLvlViewModel trSpecialLvlViewModel = specialCanvas.transform.Find("SpecialLevelViewModel").GetComponent<TrSpecialLvlViewModel>();
            trSpecialLvlViewModel.userName = "Mary";
            trSpecialLvlViewModel.courseName = "Math100";
            trSpecialLvlViewModel.lvlNoIF = 2;
            trSpecialLvlViewModel.Read();
            trSpecialLvlViewModel.CreateLvl();
            yield return new WaitForSeconds(10);
            Assert.IsTrue(trSpecialLvlViewModel.created);
        }

        ///<summary>
        ///Test whether the a level cannot be created if the level already exit in the course
        ///</summary> 
        [UnityTest]
        public IEnumerator CreateLvlAlrExist()
        {
            GameObject specialCanvas = GameObject.Find("SpecialLevelCanvas");
            TrSpecialLvlViewModel trSpecialLvlViewModel = specialCanvas.transform.Find("SpecialLevelViewModel").GetComponent<TrSpecialLvlViewModel>();
            trSpecialLvlViewModel.userName = "Mary";
            trSpecialLvlViewModel.courseName = "Math100";
            trSpecialLvlViewModel.lvlNoIF = 1;
            trSpecialLvlViewModel.Read();
            trSpecialLvlViewModel.CreateLvl();
            yield return new WaitForSeconds(10);
            Assert.IsFalse(trSpecialLvlViewModel.created);
        }

    }
}
