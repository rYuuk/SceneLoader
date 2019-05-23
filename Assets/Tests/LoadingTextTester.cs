using NUnit.Framework;

namespace SceneLoader.Test
{
    public class LoadingTextTester
    {
        [Test]
        public void LoadingTextFetchTester()
        {
            LoadingTextManager loadingTextManager = new LoadingTextManager();
            Assert.NotNull(loadingTextManager.loadingTexts);
        }
    }
}
