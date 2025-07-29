using System.Net.Mail;

namespace PersonManager.Application.Helpers;

public static class ValidationHelper
{
    public static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    public static bool IsValidCpf(string cpf)
    {
        return cpf != null && cpf.Length == 11 && cpf.All(char.IsDigit);
    }
}