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

namespace Tests
{
    /// <summary>
    /// Author: Lee Chong Yu <br/>
    /// Test Script for Login & Account Creation
    /// </summary>
    public class PlayModeTestSuite : MonoBehaviour
    {
        // A Test behaves as an ordinary method
        [Test]
        public void PlayModeTestSuiteSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator PlayModeTestSuiteWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }

        [OneTimeSetUp]

        public void LoadScene()
        {
            SceneManager.LoadScene("MainSceneIntergated2");
            
        }

        /// <summary>
        /// TC-1 Correct Login
        /// </summary>
        [UnityTest]
        public IEnumerator TC1CorrectLogin()
        {
            GameObject loginCanvas = GameObject.Find("LoginCanvas");

            InputField emailInput = loginCanvas.transform.Find("EmailInputField").GetComponent<InputField>();
            InputField passwordInput = loginCanvas.transform.Find("PasswordInputField").GetComponent<InputField>();
            LoginControllerScript loginController = loginCanvas.transform.Find("LoginController").GetComponent<LoginControllerScript>();

            emailInput.text = "tan11@hotmail.com";
            passwordInput.text = "Qwerty123";

            loginController.login();
            yield return new WaitForSeconds(10);


            Debug.Log(loginController.userData.userName);
            Assert.IsTrue(loginController.studentChk);

        }

        /// <summary>
        /// TC-2 Incorrect Login
        /// </summary>
        [UnityTest]
        public IEnumerator TC2IncorrectLogin()
        {
            GameObject loginCanvas = GameObject.Find("LoginCanvas");
            InputField emailInput = loginCanvas.transform.Find("EmailInputField").GetComponent<InputField>();
            InputField passwordInput = loginCanvas.transform.Find("PasswordInputField").GetComponent<InputField>();
            LoginControllerScript loginController = loginCanvas.transform.Find("LoginController").GetComponent<LoginControllerScript>();

            emailInput.text = "chuanming99@hotmail.com";
            passwordInput.text = "12345678";

            loginController.login();
            yield return new WaitForSeconds(10);

            Debug.Log(loginController.userData.userName);
            Assert.IsFalse(loginController.studentChk);
            
        }

        /// <summary>
        /// TC-3 Correct Registration Procedure
        /// </summary>
        [UnityTest]
        public IEnumerator TC3CorrectRegistration()
        {

            GameObject createAccountCanvas = GameObject.Find("CreateAccountCanvas");
            InputField emailInput = createAccountCanvas.transform.Find("UsernameInputField").GetComponent<InputField>();
            InputField username = createAccountCanvas.transform.Find("NameInputField").GetComponent<InputField>();
            InputField passwordInput = createAccountCanvas.transform.Find("PasswordInputField").GetComponent<InputField>();
            InputField passwordConfirmInput = createAccountCanvas.transform.Find("PasswordConfirmInputField").GetComponent<InputField>();

            CreateAccountControllerScript createAccountController = createAccountCanvas.transform.Find("CreateAccountController").GetComponent<CreateAccountControllerScript>();


            emailInput.text = "tan11@hotmail.com";
            username.text = "taneleven";
            passwordInput.text = "Qwerty123";
            passwordConfirmInput.text = "Qwerty123";

            
            createAccountController.createAccount();
            yield return new WaitForSeconds(10);

            Debug.Log(createAccountController.check.ToString());
            Assert.IsTrue(createAccountController.check);

        }

        /// <summary>
        /// TC-4 Repeated Email Registration
        /// </summary>
        [UnityTest]
        public IEnumerator TC4RepeatedEmailRegistration()
        {

            GameObject createAccountCanvas = GameObject.Find("CreateAccountCanvas");
            InputField emailInput = createAccountCanvas.transform.Find("UsernameInputField").GetComponent<InputField>();
            InputField username = createAccountCanvas.transform.Find("NameInputField").GetComponent<InputField>();
            InputField passwordInput = createAccountCanvas.transform.Find("PasswordInputField").GetComponent<InputField>();
            InputField passwordConfirmInput = createAccountCanvas.transform.Find("PasswordConfirmInputField").GetComponent<InputField>();

            CreateAccountControllerScript createAccountController = createAccountCanvas.transform.Find("CreateAccountController").GetComponent<CreateAccountControllerScript>();


            emailInput.text = "TanSy123@gmail.com";
            username.text = "Tansooyong";
            passwordInput.text = "P@55w0rD";
            passwordConfirmInput.text = "P@55w0rD";


            createAccountController.createAccount();
            yield return new WaitForSeconds(10);

            Debug.Log(createAccountController.emailExistflag.ToString());
            Assert.IsTrue(createAccountController.emailExistflag);

        }

        /// <summary>
        /// TC-5 Password Mismatched Registration Procedure
        /// </summary>
        [UnityTest]
        public IEnumerator TC5PasswordMismatchedRegistration()
        {

            GameObject createAccountCanvas = GameObject.Find("CreateAccountCanvas");
            InputField emailInput = createAccountCanvas.transform.Find("UsernameInputField").GetComponent<InputField>();
            InputField username = createAccountCanvas.transform.Find("NameInputField").GetComponent<InputField>();
            InputField passwordInput = createAccountCanvas.transform.Find("PasswordInputField").GetComponent<InputField>();
            InputField passwordConfirmInput = createAccountCanvas.transform.Find("PasswordConfirmInputField").GetComponent<InputField>();

            CreateAccountControllerScript createAccountController = createAccountCanvas.transform.Find("CreateAccountController").GetComponent<CreateAccountControllerScript>();


            emailInput.text = "TanSy123@gmail.com";
            username.text = "Tansooyong";
            passwordInput.text = "P@55w0rD";
            passwordConfirmInput.text = "password";

            createAccountController.createAccount();
            yield return new WaitForSeconds(10);

            Debug.Log(createAccountController.pwMismatchflag.ToString());
            Assert.IsTrue(createAccountController.pwMismatchflag);

        }

        /// <summary>
        /// TC-6 No Email Input Registration Procedure
        /// </summary>
        [UnityTest]
        public IEnumerator TC6NoEmailInputRegistration()
        {

            GameObject createAccountCanvas = GameObject.Find("CreateAccountCanvas");
            InputField emailInput = createAccountCanvas.transform.Find("UsernameInputField").GetComponent<InputField>();
            InputField username = createAccountCanvas.transform.Find("NameInputField").GetComponent<InputField>();
            InputField passwordInput = createAccountCanvas.transform.Find("PasswordInputField").GetComponent<InputField>();
            InputField passwordConfirmInput = createAccountCanvas.transform.Find("PasswordConfirmInputField").GetComponent<InputField>();

            CreateAccountControllerScript createAccountController = createAccountCanvas.transform.Find("CreateAccountController").GetComponent<CreateAccountControllerScript>();


            emailInput.text = "";
            username.text = "Tansooyong";
            passwordInput.text = "P@55w0rD";
            passwordConfirmInput.text = "P@55w0rD";

            createAccountController.createAccount();
            yield return new WaitForSeconds(10);

            Debug.Log(createAccountController.emailEmptyflag.ToString());
            Assert.IsTrue(createAccountController.emailEmptyflag);

        }

        /// <summary>
        /// TC-7 Wrong Email Format Input Registration Procedure
        /// </summary>
        [UnityTest]
        public IEnumerator TC7WrongEmailFormatInputRegistration()
        {

            GameObject createAccountCanvas = GameObject.Find("CreateAccountCanvas");
            InputField emailInput = createAccountCanvas.transform.Find("UsernameInputField").GetComponent<InputField>();
            InputField username = createAccountCanvas.transform.Find("NameInputField").GetComponent<InputField>();
            InputField passwordInput = createAccountCanvas.transform.Find("PasswordInputField").GetComponent<InputField>();
            InputField passwordConfirmInput = createAccountCanvas.transform.Find("PasswordConfirmInputField").GetComponent<InputField>();

            CreateAccountControllerScript createAccountController = createAccountCanvas.transform.Find("CreateAccountController").GetComponent<CreateAccountControllerScript>();


            emailInput.text = "TanSy123@gmail";
            username.text = "Tansooyong";
            passwordInput.text = "P@55w0rD";
            passwordConfirmInput.text = "P@55w0rD";

            createAccountController.createAccount();
            yield return new WaitForSeconds(10);

            Debug.Log(createAccountController.emailFormatWrong.ToString());
            Assert.IsTrue(createAccountController.emailFormatWrong);

        }

        /// <summary>
        /// TC-8 No Name Input Input Registration Procedure
        /// </summary>
        [UnityTest]
        public IEnumerator TC8NoNameInputRegistration()
        {

            GameObject createAccountCanvas = GameObject.Find("CreateAccountCanvas");
            InputField emailInput = createAccountCanvas.transform.Find("UsernameInputField").GetComponent<InputField>();
            InputField username = createAccountCanvas.transform.Find("NameInputField").GetComponent<InputField>();
            InputField passwordInput = createAccountCanvas.transform.Find("PasswordInputField").GetComponent<InputField>();
            InputField passwordConfirmInput = createAccountCanvas.transform.Find("PasswordConfirmInputField").GetComponent<InputField>();

            CreateAccountControllerScript createAccountController = createAccountCanvas.transform.Find("CreateAccountController").GetComponent<CreateAccountControllerScript>();


            emailInput.text = "TanSy123@gmail.com";
            username.text = "";
            passwordInput.text = "P@55w0rD";
            passwordConfirmInput.text = "P@55w0rD";

            createAccountController.createAccount();
            yield return new WaitForSeconds(10);

            Debug.Log(createAccountController.emptyUsername.ToString());
            Assert.IsTrue(createAccountController.emptyUsername);

        }

        /// <summary>
        /// TC-9 No Password Input Registration Procedure
        /// </summary>
        [UnityTest]
        public IEnumerator TC9NoPasswordInputRegistration()
        {

            GameObject createAccountCanvas = GameObject.Find("CreateAccountCanvas");
            InputField emailInput = createAccountCanvas.transform.Find("UsernameInputField").GetComponent<InputField>();
            InputField username = createAccountCanvas.transform.Find("NameInputField").GetComponent<InputField>();
            InputField passwordInput = createAccountCanvas.transform.Find("PasswordInputField").GetComponent<InputField>();
            InputField passwordConfirmInput = createAccountCanvas.transform.Find("PasswordConfirmInputField").GetComponent<InputField>();

            CreateAccountControllerScript createAccountController = createAccountCanvas.transform.Find("CreateAccountController").GetComponent<CreateAccountControllerScript>();


            emailInput.text = "TanSy123@gmail.com";
            username.text = "Tansooyong";
            passwordInput.text = "";
            passwordConfirmInput.text = "P@55w0rD";

            createAccountController.createAccount();
            yield return new WaitForSeconds(10);

            Debug.Log(createAccountController.passwordEmpty.ToString());
            Assert.IsTrue(createAccountController.passwordEmpty);

        }

        /// <summary>
        /// TC-10 Repeated Name Registration Procedure
        /// </summary>
        [UnityTest]
        public IEnumerator TC10RepeatedNameRegistration()
        {

            GameObject createAccountCanvas = GameObject.Find("CreateAccountCanvas");
            InputField emailInput = createAccountCanvas.transform.Find("UsernameInputField").GetComponent<InputField>();
            InputField username = createAccountCanvas.transform.Find("NameInputField").GetComponent<InputField>();
            InputField passwordInput = createAccountCanvas.transform.Find("PasswordInputField").GetComponent<InputField>();
            InputField passwordConfirmInput = createAccountCanvas.transform.Find("PasswordConfirmInputField").GetComponent<InputField>();

            CreateAccountControllerScript createAccountController = createAccountCanvas.transform.Find("CreateAccountController").GetComponent<CreateAccountControllerScript>();


            emailInput.text = "TanSy123@gmail.com";
            username.text = "Tansooyong";
            passwordInput.text = "P@55w0rD";
            passwordConfirmInput.text = "P@55w0rD";


            createAccountController.createAccount();
            yield return new WaitForSeconds(10);

            Debug.Log(createAccountController.nameExistflag.ToString());
            Assert.IsTrue(createAccountController.nameExistflag);

        }

        /// <summary>
        /// TC-11 Wrong Password Format Registration Procedure
        /// </summary>
        [UnityTest]
        public IEnumerator TC11WrongPasswordFormatRegistration()
        {

            GameObject createAccountCanvas = GameObject.Find("CreateAccountCanvas");
            InputField emailInput = createAccountCanvas.transform.Find("UsernameInputField").GetComponent<InputField>();
            InputField username = createAccountCanvas.transform.Find("NameInputField").GetComponent<InputField>();
            InputField passwordInput = createAccountCanvas.transform.Find("PasswordInputField").GetComponent<InputField>();
            InputField passwordConfirmInput = createAccountCanvas.transform.Find("PasswordConfirmInputField").GetComponent<InputField>();

            CreateAccountControllerScript createAccountController = createAccountCanvas.transform.Find("CreateAccountController").GetComponent<CreateAccountControllerScript>();


            emailInput.text = "TanSy123@gmail.com";
            username.text = "Tansooyong";
            passwordInput.text = "1234567";
            passwordConfirmInput.text = "1234567";


            createAccountController.createAccount();
            yield return new WaitForSeconds(10);

            Debug.Log(createAccountController.wrongPasswordFormat.ToString());
            Assert.IsTrue(createAccountController.wrongPasswordFormat);

        }

        /// <summary>
        /// TC-12 No Confirm Password Input Registration Procedure
        /// </summary>
        [UnityTest]
        public IEnumerator TC12NoConfirmPasswordInputRegistration()
        {

            GameObject createAccountCanvas = GameObject.Find("CreateAccountCanvas");
            InputField emailInput = createAccountCanvas.transform.Find("UsernameInputField").GetComponent<InputField>();
            InputField username = createAccountCanvas.transform.Find("NameInputField").GetComponent<InputField>();
            InputField passwordInput = createAccountCanvas.transform.Find("PasswordInputField").GetComponent<InputField>();
            InputField passwordConfirmInput = createAccountCanvas.transform.Find("PasswordConfirmInputField").GetComponent<InputField>();

            CreateAccountControllerScript createAccountController = createAccountCanvas.transform.Find("CreateAccountController").GetComponent<CreateAccountControllerScript>();


            emailInput.text = "TanSy123@gmail.com";
            username.text = "Tansooyong";
            passwordInput.text = "P@55w0rD";
            passwordConfirmInput.text = "";


            createAccountController.createAccount();
            yield return new WaitForSeconds(10);

            Debug.Log(createAccountController.emptyCfmPassword.ToString());
            Assert.IsTrue(createAccountController.emptyCfmPassword);

        }



    }
}
