using Data;

namespace DataTest
{
    using Data.Abstract;
    using Data.Implementation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DataLayerTests
    {

        [TestClass]
        public class DataApiTests
        {
            private const int Rescale = 100;
            private const int TableX = 500 * Rescale;
            private const int TableY = 500 * Rescale;

            private DataApi _dataApi;

            [TestInitialize]
            public void Setup()
            {
                _dataApi = new DataApi();
            }

            [TestCleanup]
            public void Teardown()
            {
                _dataApi.RemoveAllBalls();
            }

            [TestMethod]
            public void AddBall_AddsBallToList()
            { 
                var position = new Position(0, 0);
                IBall ball = _dataApi.AddBall(position);

                Assert.AreEqual(1, _dataApi.Balls.Count);
                Assert.AreEqual(ball, _dataApi.Balls[0]);
                _dataApi.RemoveAllBalls();
            }

            [TestMethod]
            public void RemoveAllBalls_ClearsBallList()
            {
                var position = new Position(0, 0);
                IBall ball = _dataApi.AddBall(position);

                _dataApi.RemoveAllBalls();

                Assert.AreEqual(0, _dataApi.Balls.Count);
            }

            [TestMethod]
            public void GetTable_ReturnsTable()
            {
                var table = _dataApi.GetTable();
                Assert.IsNotNull(table);
                Assert.AreEqual(TableX, table.TableX);
                Assert.AreEqual(TableY, table.TableY);
            }
        }

        [TestClass]
        public class BallTests
        {
            private Ball _ball;

            [TestCleanup]
            public void Teardown()
            {
                _ball?.Dispose();
            }

            [TestMethod]
            public void Ball_InitializesCorrectly()
            {
                var position = new Position(0, 0);
                var velocity = new VelocityVector(1, 1);
                _ball = new Ball(position, velocity);

                Assert.AreEqual(position, _ball.Position);
                Assert.AreEqual(velocity, _ball.Velocity);
            }
        }
    }
}
