using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace BattleTests
{
    public class BattleTestsScript
    {
        private BattleModelViewScript battleModelViewScript;
        private GameObject battleCanvas;
        [SetUp]
        public void Setup()
        {
            battleCanvas = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/BattleCanvas"));
            battleModelViewScript = battleCanvas.GetComponent<BattleModelViewScript>();
            battleModelViewScript.monsterPrefab = (Resources.Load<GameObject>("Prefabs/Monster"));
            battleModelViewScript.playerPrefab = (Resources.Load<GameObject>("Prefabs/Player"));
            battleModelViewScript.init();
        }
        // A Test behaves as an ordinary method
        [Test]
        public void TC30TestCorrectAnswerMonsterTakeDamage()
        {
            // Use the Assert class to test conditions
            
            InputField input = battleCanvas.GetComponentInChildren<InputField>();
            battleModelViewScript.Answer = 5;
            battleModelViewScript.answerInput.text = "5";
            battleModelViewScript.MonsterHealth = 10000;
            int initialMonsterHealth = battleModelViewScript.MonsterHealth;
            battleModelViewScript.checkAnswer();
            Assert.Less(battleModelViewScript.MonsterHealth, initialMonsterHealth);
        }

        [Test]
        public void TC30TestCorrectAnswerQuestionChange()
        {
            // Use the Assert class to test conditions

            InputField input = battleCanvas.GetComponentInChildren<InputField>();
            battleModelViewScript.Answer = 5;
            battleModelViewScript.answerInput.text = "5";
            string initialQuestion = battleModelViewScript.QuestionString;
            battleModelViewScript.checkAnswer();
            Assert.AreNotEqual(battleModelViewScript.QuestionString, initialQuestion);
        }

        [Test]
        public void TC30TC29TestWrongAnswerPlayerTakeDamage()
        {
            // Use the Assert class to test conditions

            InputField input = battleCanvas.GetComponentInChildren<InputField>();
            battleModelViewScript.Answer = 5;
            battleModelViewScript.answerInput.text = "4";
            int initialPlayerHealth = battleModelViewScript.PlayerHealth;
            battleModelViewScript.checkAnswer();
            Assert.Less(battleModelViewScript.PlayerHealth, initialPlayerHealth);
        }

        [Test]
        public void TC30TestWrongAnswerQuestionChange()
        {
            // Use the Assert class to test conditions

            InputField input = battleCanvas.GetComponentInChildren<InputField>();
            battleModelViewScript.Answer = 5;
            battleModelViewScript.answerInput.text = "4";
            string initialQuestion = battleModelViewScript.QuestionString;
            battleModelViewScript.checkAnswer();
            Assert.AreNotEqual(battleModelViewScript.QuestionString, initialQuestion);
        }

        [Test]
        public void TC30TestOpenResultScreenWhenMonsterHealthIsZero()
        {
            // Use the Assert class to test conditions

            InputField input = battleCanvas.GetComponentInChildren<InputField>();
            battleModelViewScript.Answer = 5;
            battleModelViewScript.answerInput.text = "5";
            battleModelViewScript.MonsterHealth = 0;
            battleModelViewScript.checkAnswer();
            
            Assert.True(battleModelViewScript.resultUIScript.gameObject.activeSelf);
        }

        [Test]
        public void TC29TestOpenResultScreenWhenPlayerHealthIsZero()
        {
            // Use the Assert class to test conditions

            InputField input = battleCanvas.GetComponentInChildren<InputField>();
            battleModelViewScript.Answer = 5;
            battleModelViewScript.answerInput.text = "5";
            battleModelViewScript.PlayerHealth = 0;
            battleModelViewScript.checkAnswer();

            Assert.True(battleModelViewScript.resultUIScript.gameObject.activeSelf);
        }
        
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator NewTestScriptWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
