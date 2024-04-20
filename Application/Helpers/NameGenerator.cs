

namespace Application.Helpers;

public class NameGenerator
{
    public static string GenerateUniqCode()
    {
        return Guid.NewGuid().ToString().Replace("-", "");
    }
}
