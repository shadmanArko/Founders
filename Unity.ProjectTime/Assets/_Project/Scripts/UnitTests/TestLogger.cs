using System;
using Zenject;
using NUnit.Framework;

[TestFixture]
public class TestLogger : ZenjectUnitTestFixture
{
    [SetUp]
    public void CommonInstall()
    {
        Container.Bind<Logger>().AsSingle();
    }

    [Test]
    public void TestInitialValues()
    {
        var logger = Container.Resolve<Logger>();

        Assert.That(logger.Log == "");
    }

    [Test]
    public void TestFirstEntry()
    {
        var logger = Container.Resolve<Logger>();

        logger.Write("foo");
        Assert.That(logger.Log == "foo");
    }

    [Test]
    public void TestAppend()
    {
        var logger = Container.Resolve<Logger>();

        logger.Write("foo");
        logger.Write("bar");

        Assert.That(logger.Log == "foobar");
    }

    [Test]
    public void TestNullValue()
    {
        var logger = Container.Resolve<Logger>();

        Assert.Throws<ArgumentException>(() => logger.Write(null));
    }
}