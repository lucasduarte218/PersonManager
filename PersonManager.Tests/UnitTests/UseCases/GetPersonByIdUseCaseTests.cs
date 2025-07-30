using PersonManager.Application.UseCases;
using PersonManager.Domain.Repositories;
using PersonManager.Tests.Builders;

namespace PersonManager.Tests.UnitTests.UseCases;

public class GetPersonByIdUseCaseTests
{
    private readonly Mock<IPersonRepository> _repositoryMock;
    private readonly GetPersonByIdUseCase _useCase;

    public GetPersonByIdUseCaseTests()
    {
        _repositoryMock = new Mock<IPersonRepository>();
        _useCase = new GetPersonByIdUseCase(_repositoryMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_WithExistingId_ShouldReturnSuccess()
    {
        // Arrange
        var personId = 1L;
        var person = PersonBuilder.Create().WithValidDefaults().WithId(personId).Build();
        _repositoryMock.Setup(x => x.GetByIdAsync(personId)).ReturnsAsync(person);

        // Act
        var result = await _useCase.ExecuteAsync(personId);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.Id.Should().Be(personId);
        result.Value.Name.Should().Be(person.Name);
    }

    [Fact]
    public async Task ExecuteAsync_WithNonExistingId_ShouldReturnFailure()
    {
        // Arrange
        var personId = 999L;
        _repositoryMock.Setup(x => x.GetByIdAsync(personId)).ReturnsAsync((PersonManager.Domain.Entities.Person?)null);

        // Act
        var result = await _useCase.ExecuteAsync(personId);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be("Pessoa não encontrada.");
    }
}