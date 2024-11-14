using Microsoft.AspNetCore.Identity;

public static class PasswordHelper
{
    private static PasswordHasher<object> _passwordHasher = new PasswordHasher<object>();

    // แฮชรหัสผ่าน
    public static string HashPassword(string password)
    {
        return _passwordHasher.HashPassword(null, password);
    }

    // ตรวจสอบความถูกต้องของรหัสผ่าน
    public static bool VerifyPassword(string hashedPassword, string providedPassword)
    {
        var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
        return result == PasswordVerificationResult.Success;
    }
}