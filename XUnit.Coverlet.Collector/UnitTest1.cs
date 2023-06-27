using Xunit;
using System;
using SquareEquationLib;

namespace UnitTest1;

public class UnitTest1
{
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
        var SquareEquation = new SquareEquation();
        var argExc= new ArgumentException();

        try
        {
            var result = SquareEquation.Solve(a,b,c);
        }
        catch (Exception ex)
        {
            Assert.Equal(ex.GetType(), argExc.GetType());
        }
    }

    [Theory]

    [InlineData(1, 2, 1)]
    [InlineData(4, 8, 4)]

    public void Test_for_one_root(double a, double b, double c)
    {
        var SquareEquation = new SquareEquation();
        double[] result = SquareEquation.Solve(a,b,c);

        bool rightSolution = true;
        double e = 1e-8;

        foreach(var i in result)
        {
            if(Math.Abs(a*Math.Pow(i,2)+b*i+c)>e)
            {
                rightSolution=false;
            }            
        }
        Assert.True(rightSolution);
    }

    [Theory]

    [InlineData(1, 72, 1)]
    [InlineData(4, 88, 4)]

    public void Test_for_two_roots(double a, double b, double c)
    {
        var SquareEquation = new SquareEquation();

        double[] result = SquareEquation.Solve(a,b,c);

        bool rightSolution = true;
        double e = 1e-8;

        foreach(var i in result)
        {
            if(Math.Abs(a*Math.Pow(i,2)+b*i+c)>e)
            {
                rightSolution=false;
            }            
        }
        Assert.True(rightSolution);

    }

    [Theory]

    [InlineData(10000, 1, 1)]
    [InlineData(1, 1, 24)]

    public void Test_for_no_roots(double a, double b, double c)
    {
        var SquareEquation = new SquareEquation();

        double[] result = SquareEquation.Solve(a,b,c);
        Assert.Equal(0, result.Length);
    }
}