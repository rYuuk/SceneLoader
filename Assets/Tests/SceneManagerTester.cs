using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class SceneManagerTester
    {
        [Test]
        public void UnitTest_LoadSceneWithCorrectId()
        {
            int sceneIndex = 1;
            GenericSceneManager.LoadScene(sceneIndex, false, () => 
            {
                Assert.That(sceneIndex, Is.EqualTo(GenericSceneManager.currentSceneIndex));
            });
        }

        [Test]
        public void UnitTest_LoadSceneWithCorrectName()
        {
            string sceneName = "SceneB";

            GenericSceneManager.LoadScene(sceneName, false, () =>
            {
                Assert.That(sceneName, Is.EqualTo(GenericSceneManager.currentSceneName));
            });
        }

        [Test]
        public void UnitTest_LoadSceneWithIncorrectId()
        {
            int sceneIndex = 6;
            GenericSceneManager.LoadScene(sceneIndex);
        }

        [Test]
        public void UnitTest_LoadSceneWithIncorrectName()
        {
            string sceneName = "SceneC";

            GenericSceneManager.LoadScene(sceneName);
        }
    }
}
