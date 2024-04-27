using AutoFixture;
using AutoFixture.AutoNSubstitute;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NUnit.Framework;
using Pool.Common.Model;
using Pool.Data.Abstract;
using Pool.Data.Implementation;
using Pool.Logic.Abstract;
using Pool.Logic.Implementation;

namespace Pool.Tests.Logic;

[TestClass]
public class LogicApiTests
{
    private Fixture _fixture;

    [SetUp]
    public void SetUp()
    {
        _fixture = new();
        _fixture.Customize(new AutoNSubstituteCustomization());
    }

    [Test]
    [TestCase(5)]
    [TestCase(10)]
    [TestCase(15)]
    public void ShouldCreateGivenNumberOfBalls(int numberOfBalls)
    {
        var dataApi = _fixture.Freeze<IDataApi>();
        dataApi.GetTable().Returns(_fixture.Create<Table>());

        var logicApi = _fixture.Create<LogicApi>();

        logicApi.CreateBalls(numberOfBalls);

        dataApi.Received(numberOfBalls).AddBall(Arg.Any<Ball>());
    }

    // [Test]
    // public void 
}