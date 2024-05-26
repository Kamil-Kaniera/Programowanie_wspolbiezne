using Data;
using Data.Abstract;
using Logic.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace LogicTest
{
    public class LogicLayerTests
    {
        private const int Rescale = 100;
        private const int TableX = 500;
        private const int TableY = 500;
        [TestClass]
        public class LogicApiTests
        {
            private FakeDataApi _fakeDataApi;
            private LogicApi _logicApi;

            [TestInitialize]
            public void Setup()
            {
                _fakeDataApi = new FakeDataApi();
                _logicApi = new LogicApi(_fakeDataApi);
            }

            [TestMethod]
            public void StartMovement_AddsCorrectNumberOfBalls()
            {
                int numberOfBalls = 5;
                _logicApi.StartMovement(numberOfBalls);

                Assert.AreEqual(numberOfBalls, _fakeDataApi.Balls.Count);
                Assert.AreEqual(numberOfBalls, _logicApi.LogicBalls.Count);
            }

            [TestMethod]
            public void StopMovement_ClearsAllBalls()
            {
                int numberOfBalls = 5;
                _logicApi.StartMovement(numberOfBalls);
                _logicApi.StopMovement();

                Assert.AreEqual(0, _fakeDataApi.Balls.Count);
                Assert.AreEqual(0, _logicApi.LogicBalls.Count);
            }

            [TestMethod]
            public void CheckTableDimensions_AreCorrect()
            {
                var table = _fakeDataApi.GetTable();
                Assert.AreEqual(TableX * Rescale, table.TableX);
                Assert.AreEqual(TableY * Rescale, table.TableY);
            }
        }
        
        internal class FakeTable(int x, int y) : ITable
        {
            public int TableX { get; } = x;
            public int TableY { get; } = y;
        }

        internal class FakeBall : IBall
        {
            public Position Position { get; set; }
            public VelocityVector Velocity { get; set; }
            public Guid BallId { get; }

            public FakeBall(Position position, VelocityVector velocity)
            {
                Position = position;
                Velocity = velocity;
            }

            public IDisposable Subscribe(IObserver<IBall> observer)
            {
                return null;
            }

            public void Dispose()
            {
            }
        }
        internal class FakeDataApi : IDataApi
        {
            public List<IBall> Balls { get; } = [];
            private readonly FakeTable _table = new(TableX * Rescale, TableY * Rescale);

            
            public IBall AddBall(Position p)
            {
                FakeBall ball = new FakeBall(p, new VelocityVector());
                Balls.Add(ball);
                return ball;
            }

            public void RemoveAllBalls()
            {
                foreach (var ball in Balls )
                {
                    ball.Dispose();
                }
                Balls.Clear();
            }

            public ITable GetTable()
            {
                return _table;
            }
        }

    }
}
