using Microsoft.AspNetCore.Mvc;
using PersonManager.API.Controllers.v2.Models;
using PersonManager.Application.DTOs;
using PersonManager.Application.Interfaces.UseCasesInterfaces;
using PersonManager.Domain.Entities;

namespace PersonManager.API.Controllers.v2;

[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class PersonController : ControllerBase
{
    private readonly ICreatePersonUseCase _createPersonUseCase;
    private readonly IUpdatePersonUseCase _updatePersonUseCase;
    private readonly IDeletePersonUseCase _deletePersonUseCase;
    private readonly IGetPersonByIdUseCase _getPersonByIdUseCase;
    private readonly IGetAllPersonsUseCase _getAllPersonsUseCase;

    public PersonController(
        ICreatePersonUseCase createPersonUseCase,
        IUpdatePersonUseCase updatePersonUseCase,
        IDeletePersonUseCase deletePersonUseCase,
        IGetPersonByIdUseCase getPersonByIdUseCase,
        IGetAllPersonsUseCase getAllPersonsUseCase)
    {
        _createPersonUseCase = createPersonUseCase;
        _updatePersonUseCase = updatePersonUseCase;
        _deletePersonUseCase = deletePersonUseCase;
        _getPersonByIdUseCase = getPersonByIdUseCase;
        _getAllPersonsUseCase = getAllPersonsUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePersonRequestModel model)
    {
        if (string.IsNullOrWhiteSpace(model.Address))
            return BadRequest("Endereço é obrigatório na versão 2.");

        var dto = new CreatePersonDto
        {
            Name = model.Name,
            Gender = model.Gender,
            Email = model.Email,
            BirthDate = model.BirthDate,
            PlaceOfBirth = model.PlaceOfBirth,
            Nationality = model.Nationality,
            CPF = model.CPF,
            Address = model.Address
        };

        var result = await _createPersonUseCase.ExecuteAsync(dto);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        var response = MapToResponseModel(result.Value!);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, [FromBody] UpdatePersonRequestModel model)
    {
        if (id != model.Id)
            return BadRequest("Id mismatch.");

        if (string.IsNullOrWhiteSpace(model.Address))
            return BadRequest("Endereço é obrigatório na versão 2.");

        var dto = new UpdatePersonDto
        {
            Id = model.Id,
            Name = model.Name,
            Gender = model.Gender,
            Email = model.Email,
            BirthDate = model.BirthDate,
            PlaceOfBirth = model.PlaceOfBirth,
            Nationality = model.Nationality,
            CPF = model.CPF,
            Address = model.Address
        };

        var result = await _updatePersonUseCase.ExecuteAsync(dto);
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        var response = MapToResponseModel(result.Value!);

        return Ok(response);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await _deletePersonUseCase.ExecuteAsync(id);
        if (!result.IsSuccess)
            return NotFound(result.Error);

        return NoContent();
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        Result<Person> result = await _getPersonByIdUseCase.ExecuteAsync(id);

        if (!result.IsSuccess)
            return NotFound(result.Error);

        PersonResponseModel response = MapToResponseModel(result.Value!);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _getAllPersonsUseCase.ExecuteAsync();
        if (!result.IsSuccess)
            return BadRequest(result.Error);

        var response = result.Value!.Select(MapToResponseModel);
        return Ok(response);
    }

    private static PersonResponseModel MapToResponseModel(Person person)
    {
        return new PersonResponseModel
        {
            Id = person.Id,
            Name = person.Name,
            Gender = person.Gender,
            Email = person.Email,
            BirthDate = person.BirthDate,
            PlaceOfBirth = person.PlaceOfBirth,
            Nationality = person.Nationality,
            CPF = person.CPF,
            Address = person.Address ?? string.Empty,
            CreatedAt = person.CreatedAt,
            UpdatedAt = person.UpdatedAt
        };
    }
}