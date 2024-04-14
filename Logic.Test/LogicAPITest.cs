using Data;
using Logic;
using System.Collections.Generic;

namespace Logic.Test
{
    [TestClass]
    public class LogicAPITest
    {
        [TestMethod]
        public void moveBallTest()
        {
            Ball ball = new(0, 0);

            ILogicAPI api = (LogicAPI)ILogicAPI.createAPI();

            api.MoveBall(ball, 5, 5);

            Assert.AreEqual(ball.X, 5);
            Assert.AreEqual(ball.Y, 5);
        }

        [TestMethod]
        public void createBallsTest()
        {
            ILogicAPI api = (LogicAPI)ILogicAPI.createAPI();

            api.CreateBalls(3);

            Assert.AreEqual(api.GetBalls().Count, 3);
        }

        [TestMethod]
        public void checkWallCollisionTest()
        {
            LogicAPI api = (LogicAPI)ILogicAPI.createAPI();  

            Assert.IsFalse(api.CheckWallCollision(451, 1));
            Assert.IsFalse(api.CheckWallCollision(50, -1));
            Assert.IsTrue(api.CheckWallCollision(50, 100));
        }

        [TestMethod]
        public void movementTest()
        {
            LogicAPI api = (LogicAPI)ILogicAPI.createAPI();

            api.CreateBalls(2);

            List<List<int>> startingBalls = api.GetAllBallsPosition();

            api.StartMovement();

            // Simulate 10 rounds of movement
            Thread.Sleep(1000);
            List<List<int>> endingBalls = api.GetAllBallsPosition();

            Assert.IsTrue(startingBalls[0] != endingBalls[0] || startingBalls[1] != endingBalls[1]);
        }
    }
}
