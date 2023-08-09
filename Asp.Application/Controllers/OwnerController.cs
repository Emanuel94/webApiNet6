using Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Entities.DataTransferObjects;
using Entities.Models;

namespace Asp.Application.Controllers
{
    [Route("api/owner")]
    [ApiController]
    public class OwnerController : Controller
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public OwnerController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAllOwners()
        {
            try
            {
                var owners = _repository.Owner.GetAllOwners();
                _logger.LogInfo($"Return all ownners from Database");
                var ownerResult = _mapper.Map<IEnumerable<OwnerDto>>(owners);
                return Ok(ownerResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something  is wrong inside GetAllOwners EndPoint: {ex.Message }");
                return StatusCode(500, "Internal Error");
            }
        }


        [HttpGet("{id}", Name = "OwnerById")]
        public IActionResult GetOwnerById(Guid id)
        {
            try
            {
                var owner = _repository.Owner.GetOwnerById(id);
                if (owner is null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned owner  with id: {id}");
                    var ownerResult = _mapper.Map<OwnerDto>(owner);
                    return Ok(ownerResult);
                }

            }
            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong inside GetOwnerById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}/account")]
        public IActionResult GetOwnerWithDetails(Guid id)
        {
            try
            {
                var owner = _repository.Owner.GetOwnerWithDetails(id);
                if (owner is null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned owner with details for id {id}");
                    var ownerResult  =  _mapper.Map<OwnerDto> (owner);
                    return Ok(ownerResult);

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside  GetOwnerWithDetails action: {ex.Message}");
                return StatusCode(500, "Internal Server");
            }

        }

        [HttpPost]
        public IActionResult CreateOwner([FromBody] OwnerForCreationDto owner)
        {
            try
            {
                if (owner is null)
                {
                    _logger.LogError("Owner Objet sent from Client");
                    return BadRequest("Owner objet is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object sent from client.");
                    return BadRequest("Invalid model objetc");
                }
                var ownerEntity = _mapper.Map<Owner>(owner);
                _repository.Owner.CreateOwner(ownerEntity);
                _repository.Save();

                var createdOwner = _mapper.Map<Owner>(owner);

                return CreatedAtRoute("OwnerById", new { id = createdOwner.Id }, createdOwner);


            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateOwner action: {ex.Message}");
                return StatusCode(500, "Internal serve error");
            }
        }

        [HttpPut]
        public IActionResult UpdateOwner(Guid id, [FromBody] OwnerForUpdate owner)
        {
            try
            {
                if (owner is null)
                {
                    _logger.LogError("Owner Object sent from client is null");
                    return BadRequest("Invalid Model Object");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object sent from client");
                    return BadRequest("Invalid model Object");
                }

                var ownerEntity = _repository.Owner.GetOwnerById(id);
                if(ownerEntity is null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in DB");
                    return NotFound();
                }
                _mapper.Map(owner, ownerEntity);

                _repository.Owner.UpdateOwner(ownerEntity);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        public IActionResult DeleteOwner(Guid id)
        {
            try
            {
                var owner = _repository.Owner.GetOwnerById(id);
                if (owner is null)
                {
                    _logger.LogError($"Owner with id {id}, hasn't been found in db.");
                    return NotFound();
                }

                if (_repository.Account.AccountsByOwner(id).Any())
                {
                    _logger.LogError($"Cannot delete owner with id: {id}. It has related accounts. Delete those accounts first");
                    return BadRequest("Cannot delete owner. It has related accounts. Delete those accounts first");
                }
                _repository.Owner.DeleteOwner(owner);
                _repository.Save();

                return NoContent();
            }

            catch (Exception ex )
            {
                _logger.LogError($"Something went wrong inside DeleteOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


    }
}
