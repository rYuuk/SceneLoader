using NUnit.Framework;
using UnityEngine;

namespace SceneLoader.Test
{
    public class SceneLoaderTester
    {
        [Test]
        public void UnitTest_LoadSceneWithCorrectName()
        {
            string sceneName = "SceneB";
            Assert.NotNull(SceneLoader.LoadScene(sceneName));
        }

        [Test]
        public void UnitTest_LoadSceneWithIncorrectName()
        {
            string sceneName = "SceneC";
            Assert.NotNull(SceneLoader.LoadScene(sceneName));
        }
    }
}
