namespace TestProject1
{
    [TestClass]
    public sealed class ImageManagerViewModelTest
    {
        [TestMethod]
        public void TestMethod_GetImageInfoModelList()
        {
            var vm = new WpfApp1.ImageManagerViewModel();
            vm.GetImageInfoModelList();
            Assert.IsTrue(true);
        }
    }
}
