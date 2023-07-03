using Xunit;
using System;
using SquareEquationLib;

namespace UnitTest1;

public class UnitTest1
{
    static double e = 1e-8;

    [Theory]
    [InlineData(0, 1, 1)]

    [InlineData(double.NaN, 1, 1)]
    [InlineData(1, double.NaN, 1)]
    [InlineData(1, 1, double.NaN)]

    [InlineData(double.NegativeInfinity, 1, 1)]
    [InlineData(1, double.NegativeInfinity, 1)]
    [InlineData(1, 1, double.NegativeInfinity)]

    [InlineData(double.PositiveInfinity, 1, 1)]
    [InlineData(1, double.PositiveInfinity, 1)]
    [InlineData(1, 1, double.PositiveInfinity)]
    public void Test_for_Exception(double a, double b, double c)
    {
        Assert.Throws<ArgumentException>(() => SquareEquation.Solve(a, b, c));
    }

    [Theory]

    [InlineData(1, 2, 1)]
    [InlineData(4, 8, 4)]

    public void Test_for_one_root(double a, double b, double c)
    {
        double x = SquareEquation.Solve(a, b, c)[0];
        Assert.Equal(0, Math.Abs(a*Math.Pow(x, 2) + b*x + c), e);
    }

    [Theory]

    [InlineData(1, 72, 1)]
    [InlineData(4, 88, 4)]

    public void Test_for_two_roots(double a, double b, double c)
    {
        var SquareEquation = new SquareEquation();

        double[] result = SquareEquation.Solve(a,b,c);

        foreach(var i in result)
        {
            Assert.Equal(0, Math.Abs(a*Math.Pow(i, 2) + b*i + c), e);       
        }
    }

    [Theory]

    [InlineData(10000, 1, 1)]
    [InlineData(1, 1, 24)]

    public void Test_for_no_roots(double a, double b, double c)
    {
        var SquareEquation = new SquareEquation();

        double[] result = SquareEquation.Solve(a,b,c);
        Assert.Empty(result);
    }
}