namespace SuperStore.Model;
public static class AssertionConcern
{
    public static void AssertArgumentNotNull(object argument, string argumentName)
    {
        if (argument == null)
            throw new ArgumentNullException(argumentName);
    }

    public static void AssertArgumentRange(decimal argument, decimal minimum, decimal maximum, string argumentName)
    {
        if (argument < minimum || argument > maximum)
            throw new ArgumentOutOfRangeException(argumentName);
    }

    public static void AssertArgumentRange(int argument, int minimum, int maximum, string argumentName)
    {
        if (argument < minimum || argument > maximum)
            throw new ArgumentOutOfRangeException(argumentName);
    }

    public static void AssertArgumentNotNegative(decimal argument, string argumentName)
    {
        if (argument < 0)
            throw new ArgumentOutOfRangeException(argumentName);
    }

    public static void AssertArgumentNotNegative(int argument, string argumentName)
    {
        if (argument < 0)
            throw new ArgumentOutOfRangeException(argumentName);
    }

    public static void AssertArgumentNotNullOrEmpty(string argument, string argumentName)
    {
        if (string.IsNullOrEmpty(argument))
            throw new ArgumentException("Argument cannot be null or empty", argumentName);
    }
}
