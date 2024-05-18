using AutoFixture;
using AutoFixture.AutoNSubstitute;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Pool.Common.Model;
using Pool.Data.Implementation;
using Pool.Data.Implementation.Mappers;

namespace Pool.Tests.Data;

[TestClass]
public class DataApiTests
{
    private Fixture _fixture;

    [OneTimeSetUp]
    public void SetUp()
    {
        _fixture = new();
        _fixture.Customize(new AutoNSubstituteCustomization());
    }

    [Test]
    public void ShouldSaveBall()
    {
        var api = _fixture.Create<DataApi>();

        var ball = _fixture.Create<Ball>();

        api.AddBall(ball);

        api.Balls.Count().Should().Be(1);
    }

    [Test]
    public void ShouldReturnBallIfExists()
    {
        var ballMapper = _fixture.Create<BallMapper>();
        _fixture.Inject((IBallMapper)ballMapper);

        var api = _fixture.Create<DataApi>();
        var ball = _fixture.Create<Ball>();

        api.AddBall(ball);

        api.Balls.Count().Should().Be(1);
    }

    [Test]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    public void ShouldAddEveryBall(int ballCount)
    {
        var ballMapper = _fixture.Create<BallMapper>();
        _fixture.Inject((IBallMapper)ballMapper);

        var api = _fixture.Create<DataApi>();
        for (var i = 0; i < ballCount; i++)
        {
            var ball = _fixture.Create<Ball>();
            api.AddBall(ball);
        }

        api.Balls.Count().Should().Be(ballCount);
    }

    [Test]
    public void ShouldThrowForBallNotFound()
    {
        var ballMapper = _fixture.Create<BallMapper>();
        _fixture.Inject((IBallMapper)ballMapper);

        var api = _fixture.Create<DataApi>();
        var ball = _fixture.Create<Ball>();

        api.Balls.Count().Should().Be(0);
        var action = () => api.GetBall(ball.BallId);
        action.Should().Throw<InvalidOperationException>();
    }
}