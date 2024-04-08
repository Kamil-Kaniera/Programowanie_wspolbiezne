using Data;

namespace Tests.Data
{
    [TestClass]
    public class BallRepositoryTest
    {
        [TestMethod]
        public void AddBallTest()
        {
            Ball ball = new(0, 0);
            BallRepository repository = new BallRepository();

            repository.addBall(ball);
            Assert.IsTrue(repository.getBalls().Contains(ball));
        }

        [TestMethod]
        public void RemoveBallTest()
        {
            Ball ball_1 = new(0, 0);
            Ball ball_2 = new(1, 1);

            BallRepository repository = new BallRepository();

            repository.addBall(ball_1);
            repository.addBall(ball_2);

            repository.removeBall(ball_1);

            Assert.IsFalse(repository.getBalls().Contains(ball_1));
        }
    }
}