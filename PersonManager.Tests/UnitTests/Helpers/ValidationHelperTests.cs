using PersonManager.Application.Helpers;

namespace PersonManager.Tests.UnitTests.Helpers;

public class ValidationHelperTests
{
    [Theory]
    [InlineData("test@email.com", true)]
    [InlineData("user.name@domain.co.uk", true)]
    [InlineData("user+tag@example.org", true)]
    [InlineData("invalid-email", false)]
    [InlineData("@domain.com", false)]
    [InlineData("user@", false)]
    [InlineData("", false)]
    [InlineData(null, false)]
    public void IsValidEmail_ShouldValidateEmailCorrectly(string email, bool expectedResult)
    {
        // Act
        var result = ValidationHelper.IsValidEmail(email);

        // Assert
        result.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData("12345678901", true)]  // 11 dígitos
    [InlineData("00000000000", true)]  // 11 zeros
    [InlineData("1234567890", false)] // 10 dígitos
    [InlineData("123456789012", false)] // 12 dígitos
    [InlineData("1234567890a", false)] // contém letra
    [InlineData("", false)]
    [InlineData(null, false)]
    public void IsValidCpf_ShouldValidateCpfCorrectly(string cpf, bool expectedResult)
    {
        // Act
        var result = ValidationHelper.IsValidCpf(cpf);

        // Assert
        result.Should().Be(expectedResult);
    }
}