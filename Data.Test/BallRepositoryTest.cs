using Data;

namespace Data.Test
{
    [TestClass]
    public class BallRepositoryTest
    {
        [TestMethod]
        public void AddBallTest()
        {
            Ball ball = new(0, 0);
            BallRepository repository = new BallRepository();

            repository.AddBall(ball);
            Assert.IsTrue(repository.GetBalls().Contains(ball));
        }

        [TestMethod]
        public void RemoveBallTest()
        {
            Ball ball_1 = new(0, 0);
            Ball ball_2 = new(1, 1);

            BallRepository repository = new BallRepository();

            repository.AddBall(ball_1);
            repository.AddBall(ball_2);

            repository.RemoveBall(ball_1);

            Assert.IsFalse(repository.GetBalls().Contains(ball_1));
        }
    }
}