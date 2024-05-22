using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Implementation;
using Commons;
using Data.Abstract;


namespace LogicTest
{
    public class LogicLayerTests
    {
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
                Assert.AreEqual(Constants.TABLE_X * Constants.RESCALE, table.TableSize.X);
                Assert.AreEqual(Constants.TABLE_Y * Constants.RESCALE, table.TableSize.Y);
            }
        }
        
        internal class FakeTable(TableSize size) : ITable
        {
            public TableSize TableSize { get; } = size;
        }

        internal class FakeBall : IBall
        {
            public Position Position { get; set; }
            public VelocityVector Velocity { get; set; }
            public int Diameter { get; } = Constants.DIAMETER * Constants.RESCALE;

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
            private readonly FakeTable _table = new(new TableSize(Constants.TABLE_X * Constants.RESCALE, Constants.TABLE_Y * Constants.RESCALE));

            
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
