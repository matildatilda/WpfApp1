namespace TestProject1
{
    [TestClass]
    public sealed class ImageManagerViewModelTest
    {
        [TestMethod]
        public void TestMethod_GetImageInfoModelList()
        {
            var vm = new ImageManagerViewModel();
            vm.GetImageInfoModelList();
            Assert.IsTrue(true);
        }
    }
}
