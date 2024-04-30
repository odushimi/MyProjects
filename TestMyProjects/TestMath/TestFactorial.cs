using static MyProjects.Math;

namespace TestMyProjects;

public class TestFactorial
{
    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    [InlineData(3, 6)]
    [InlineData(4, 24)]
    [InlineData(5, 120)]
    [InlineData(6, 720)]
    [InlineData(7, 5040)]
    [InlineData(8, 40320)]
    //[InlineData(18, 6402373705728000)]
    public void ShouldCalculateFactorial(int number, int expected)
    {
        var actual = Factorial(number);

        Assert.True(actual == expected,
        $"Expected Factorial({number})={expected}");
    }


    [Theory]
    [InlineData(-1)]
    [InlineData(-2)]
    [InlineData(-3)]
    [InlineData(-99)]
    public void ShouldThrowException(int number)
    {
        Func<object?> testCode = () => Factorial(number);
        var exception = Assert.Throws<Exception>(testCode);

        Assert.Equal("Number cannot be less than 0.", exception.Message);
    }
}