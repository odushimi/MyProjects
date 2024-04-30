using static MyProjects.Math;

namespace TestMyProjects;

public class TestFibonacci
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(2, 1)]
    [InlineData(3, 2)]
    [InlineData(4, 3)]
    [InlineData(5, 5)]
    [InlineData(6, 8)]
    [InlineData(7, 13)]
    [InlineData(8, 21)]
    [InlineData(18, 2584)]
    [InlineData(19, 4181)]
    public void ShouldCalculateFibonacci(int number, int expected)
    {
        var actual = Fibonacci(number);

        Assert.True(actual == expected,
        $"Expected Fibonacci({number})={expected}");
    }


    [Theory]
    [InlineData(-1)]
    [InlineData(-2)]
    [InlineData(-3)]
    [InlineData(-99)]
    public void ShouldThrowException(int number)
    {
        Func<object?> testCode = () => Fibonacci(number);
        var exception = Assert.Throws<Exception>(testCode);

        Assert.Equal("Number cannot be less than 0.", exception.Message);
    }
}
