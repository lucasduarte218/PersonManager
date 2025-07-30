using PersonManager.Application.UseCases;
using PersonManager.Domain.Repositories;
using PersonManager.Tests.Builders;


namespace PersonManager.Tests.UnitTests.UseCases;

public class CreatePersonUseCaseTests
{
    private readonly Mock<IPersonRepository> _repositoryMock;
    private readonly CreatePersonUseCase _useCase;

    public CreatePersonUseCaseTests()
    {
        _repositoryMock = new Mock<IPersonRepository>();
        _useCase = new CreatePersonUseCase(_repositoryMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_WithValidData_ShouldReturnSuccess()
    {
        // Arrange
        var dto = CreatePersonDtoBuilder.Create().WithValidDefaults().Build();
        _repositoryMock.Setup(x => x.GetByCpfAsync(dto.CPF)).ReturnsAsync((PersonManager.Domain.Entities.Person?)null);

        // Act
        var result = await _useCase.ExecuteAsync(dto);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.Name.Should().Be(dto.Name);
        result.Value.CPF.Should().Be(dto.CPF);
        _repositoryMock.Verify(x => x.AddAsync(It.IsAny<PersonManager.Domain.Entities.Person>()), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_WithEmptyName_ShouldReturnFailure()
    {
        // Arrange
        var dto = CreatePersonDtoBuilder.Create().WithValidDefaults().WithName("").Build();

        // Act
        var result = await _useCase.ExecuteAsync(dto);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be("Nome é obrigatório.");
        _repositoryMock.Verify(x => x.AddAsync(It.IsAny<PersonManager.Domain.Entities.Person>()), Times.Never);
    }

    [Fact]
    public async Task ExecuteAsync_WithEmptyCpf_ShouldReturnFailure()
    {
        // Arrange
        var dto = CreatePersonDtoBuilder.Create().WithValidDefaults().WithCpf("").Build();

        // Act
        var result = await _useCase.ExecuteAsync(dto);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be("CPF é obrigatório.");
    }

    [Fact]
    public async Task ExecuteAsync_WithInvalidEmail_ShouldReturnFailure()
    {
        // Arrange
        var dto = CreatePersonDtoBuilder.Create().WithValidDefaults().WithEmail("invalid-email").Build();

        // Act
        var result = await _useCase.ExecuteAsync(dto);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be("E-mail inválido.");
    }

    [Fact]
    public async Task ExecuteAsync_WithExistingCpf_ShouldReturnFailure()
    {
        // Arrange
        var dto = CreatePersonDtoBuilder.Create().WithValidDefaults().Build();
        var existingPerson = PersonBuilder.Create().WithValidDefaults().WithCpf(dto.CPF).Build();
        _repositoryMock.Setup(x => x.GetByCpfAsync(dto.CPF)).ReturnsAsync(existingPerson);

        // Act
        var result = await _useCase.ExecuteAsync(dto);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be("CPF já cadastrado.");
    }

    [Theory]
    [InlineData("1234567890")] // 10 dígitos
    [InlineData("123456789012")] // 12 dígitos
    [InlineData("1234567890a")] // contém letra
    public async Task ExecuteAsync_WithInvalidCpf_ShouldReturnFailure(string invalidCpf)
    {
        // Arrange
        var dto = CreatePersonDtoBuilder.Create().WithValidDefaults().WithCpf(invalidCpf).Build();

        // Act
        var result = await _useCase.ExecuteAsync(dto);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be("CPF inválido.");
    }

    [Fact]
    public async Task ExecuteAsync_WithDefaultBirthDate_ShouldReturnFailure()
    {
        // Arrange
        var dto = CreatePersonDtoBuilder.Create().WithValidDefaults().WithBirthDate(default).Build();

        // Act
        var result = await _useCase.ExecuteAsync(dto);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be("Data de nascimento é obrigatória.");
    }
}