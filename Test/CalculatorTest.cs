namespace Test
{
    [TestClass]
    public class CalculatorTest
    {
        [TestMethod]
        public void PositiveAddTest()
        {
            Assert.AreEqual(Project.Calculator.add(2, 3), 5);
        }

        [TestMethod]
        public void PositiveSubtractTest() 
        {
            Assert.AreEqual(Project.Calculator.subtract(4, 1), 3);
        }
    }
}