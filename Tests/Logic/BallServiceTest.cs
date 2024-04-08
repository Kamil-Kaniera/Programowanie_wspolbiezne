using Data;
using Logic;

namespace Tests.Logic
{
    [TestClass]
    public class BallServiceTest
    {
        [TestMethod]
        public void moveBallTest()
        {
            Ball ball = new(0, 0);
            Table table = new(100, 200);

            BallService service = new(table);

            service.moveBall(ball, 5 , 5);

            Assert.AreEqual(ball.positionX, 5);
            Assert.AreEqual(ball.positionY, 5);
        }

        [TestMethod]
        public void createBallsTest()
        {
            Table table = new(100, 200);
            BallService service = new(table);

            service.createBalls(3);

            Assert.AreEqual(service.getBalls().Count, 3);
        }
    }
}
