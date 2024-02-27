using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using HallOfFame.Domain.Entities;
using HallOfFame.Domain.Exceptions;
using HallOfFame.Infrastructure.DTO;
using HallOfFame.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HallOfFame.Controllers
{
    [Route("api/v1/persons")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;
        private readonly IValidator<Person> _personValidator;
        private readonly IValidator<Skill> _skillValidator;

        public PersonController(IPersonService personService, IMapper mapper,
                                IValidator<Person> personValidator, IValidator<Skill> skillValidator)
        {
            _personService = personService;
            _mapper = mapper;
            _personValidator = personValidator;
            _skillValidator = skillValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var persons = await _personService.GetAll();

                var res = _mapper.Map<List<PersonResponse>>(persons);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var person = await _personService.GetById(id);

                var res = _mapper.Map<PersonResponse>(person);
                return Ok(res);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(PersonRequest request)
        {
            try
            {
                var newPerson = _mapper.Map<Person>(request);
                ValidationResult personResult = _personValidator.Validate(newPerson);

                if (!personResult.IsValid)
                    return BadRequest(personResult.Errors);

                var createdPerson = await _personService.Create(newPerson);

                var res = _mapper.Map<PersonResponse>(createdPerson);
                return Ok(res);
            }
            catch(UniqueException  ex)
            {
                return Conflict(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, PersonRequest request)
        {
            try
            {
                var newPerson = _mapper.Map<Person>(request);
                ValidationResult personResult = _personValidator.Validate(newPerson);

                if (!personResult.IsValid)
                    return BadRequest(personResult.Errors);

                await _personService.Update(id, request.Name, request.DispayName, request.Description, skills);

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _personService.DeleteById(id);

                return Ok();
            }
            catch(NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
