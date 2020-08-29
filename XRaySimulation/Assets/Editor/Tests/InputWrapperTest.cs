using System;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

public class InputWrapperTest
{
    [Test]
    [TestCase(true)]
    [TestCase(false)]
    public void GetKeyReturnsExpectedValue_Test(bool expected)
    {
        IInputWrapper input = Substitute.For<IInputWrapper>();
        input.GetKey(KeyCode.None).Returns(expected);

        bool actual = input.GetKey(KeyCode.None);

        Assert.AreEqual(expected, actual);
    }

    [Test]
    [TestCase(true, KeyCode.A)]
    [TestCase(false, KeyCode.A)]
    [TestCase(true, KeyCode.Space)]
    [TestCase(false, KeyCode.Space)]
    public void GetKeyReturnsExpectedForKey_Test(bool expected, KeyCode code)
    {
        IInputWrapper input = Substitute.For<IInputWrapper>();
        input.GetKey(code).Returns(expected);

        bool actual = input.GetKey(code);

        Assert.AreEqual(expected, actual);
    }

    [Test]
    [TestCase(true)]
    [TestCase(false)]
    public void GetKeyDownReturnsExpectedValue_Test(bool expected)
    {
        IInputWrapper input = Substitute.For<IInputWrapper>();
        input.GetKey(KeyCode.None).Returns(expected);

        bool actual = input.GetKey(KeyCode.None);

        Assert.AreEqual(expected, actual);
    }

    [Test]
    [TestCase(true, KeyCode.A)]
    [TestCase(false, KeyCode.A)]
    [TestCase(true, KeyCode.Space)]
    [TestCase(false, KeyCode.Space)]
    public void GetKeyDownReturnsExpectedForKey_Test(bool expected, KeyCode code)
    {
        IInputWrapper input = Substitute.For<IInputWrapper>();
        input.GetKey(code).Returns(expected);

        bool actual = input.GetKey(code);

        Assert.AreEqual(expected, actual);
    }

    [Test]
    [TestCase(0f)]
    [TestCase(1f)]
    [TestCase(0.5f)]
    [TestCase(-1f)]
    [TestCase(-0.5f)]
    public void GetAxisReturnsExpectedValue_Test(float expected)
    {
        IInputWrapper input = Substitute.For<IInputWrapper>();
        input.GetAxis("Mouse X").Returns(expected);

        float actual = input.GetAxis("Mouse X");

        Assert.AreEqual(expected, actual);
    }

    [Test]
    [TestCase(0f, "Mouse X")]
    [TestCase(1f, "Mouse X")]
    [TestCase(0.5f, "Mouse Y")]
    [TestCase(-1f, "Mouse Y")]
    [TestCase(-0.5f, "Mouse X")]
    public void GetAxisReturnsExpectedValue_Test(float expected, string axis)
    {
        IInputWrapper input = Substitute.For<IInputWrapper>();
        input.GetAxis(axis).Returns(expected);

        float actual = input.GetAxis(axis);

        Assert.AreEqual(expected, actual);
    }

}