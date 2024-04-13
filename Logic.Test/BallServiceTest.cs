using Data;
using Logic;
using System.Collections.Generic;

namespace Logic.Test
{
    [TestClass]
    public class BallServiceTest
    {
        [TestMethod]
        public void moveBallTest()
        {
            Ball ball = new(0, 0);

            BallService service = new(100, 200);

            service.MoveBall(ball, 5, 5);

            Assert.AreEqual(ball.PositionX, 5);
            Assert.AreEqual(ball.PositionY, 5);
        }

        [TestMethod]
        public void createBallsTest()
        {
            BallService service = new(100, 200);

            service.CreateBalls(3);

            Assert.AreEqual(service.GetBalls().Count, 3);
        }

        [TestMethod]
        public void checkWallCollisionTest()
        {
            BallService service = new(100, 200);

            Assert.IsFalse(service.CheckWallCollision(101, 1));
            Assert.IsFalse(service.CheckWallCollision(50, -1));
            Assert.IsTrue(service.CheckWallCollision(50, 100));
        }

        [TestMethod]
        public void movementTest()
        {
            BallService service = new(100, 200);

            service.CreateBalls(2);

            List<List<int>> startingBalls = service.GetAllBallsPosition();

            service.StartMovement();

            // Simulate 10 rounds of movement
            Thread.Sleep(1000);
            List<List<int>> endingBalls = service.GetAllBallsPosition();

            Assert.IsTrue(startingBalls[0] != endingBalls[0] || startingBalls[1] != endingBalls[1]);
        }
    }
}
