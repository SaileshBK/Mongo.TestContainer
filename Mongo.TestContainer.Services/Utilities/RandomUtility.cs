namespace Mongo.TestContainer.Services.Utilities;

internal static class RandomUtility
{
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    public static string GenerateRandomString(int length)
    {
        return new string(Enumerable.Repeat(Chars, length)
            .Select(s => s[new Random().Next(s.Length)]).ToArray());
    }
}
