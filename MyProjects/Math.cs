namespace MyProjects;

public static class Math
{
    public static int Fibonacci(int number)
    {
        if (number < 0) throw new Exception("Number cannot be less than 0.");
        if (number < 2) return number;
        return Fibonacci(number - 2) + Fibonacci (number - 1);
    }

    public static int Factorial(int number)
    {
        if (number < 0) throw new Exception("Number cannot be less than 0.");
        if (number < 2) return 1;
        return Factorial(number -1 ) * number;
    }
}
