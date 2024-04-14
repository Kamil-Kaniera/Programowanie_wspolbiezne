using Data;

namespace Data.Test
{
    [TestClass]
    public class DataAPITest
    {
        [TestMethod]
        public void AddBallTest()
        {
            Ball ball = new(0, 0);
            IDataAPI api = IDataAPI.cerateAPI();

            api.AddBall(ball);
            Assert.IsTrue(api.GetBalls().Contains(ball));
        }

        [TestMethod]
        public void RemoveAllBallsTest()
        {
            Ball ball_1 = new(0, 0);
            Ball ball_2 = new(1, 1);

            IDataAPI api = IDataAPI.cerateAPI();

            api.AddBall(ball_1);
            api.AddBall(ball_2);

            api.RemoveAllBalls();

            Assert.AreEqual(api.GetBalls().Count, 0);
        }
    }
}