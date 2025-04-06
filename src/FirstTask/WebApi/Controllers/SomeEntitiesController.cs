using Application.Interfaces;
using Application.Models;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WebApi.Common.Interfaces;
using WebApi.Dtos;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SomeEntitiesController(ISomeEntityService service,
                                        IMapper<SomeEntityDto, SomeEntity> mapper) : ControllerBase
    {
        private readonly IMapper<SomeEntityDto, SomeEntity> _mapper = mapper;
        private readonly ISomeEntityService _service = service;
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] CodeFilter filterModel, CancellationToken ct)
        {
            Log.Information("Start Get method");
            var result = await _service.GetEntities(filterModel, ct);
            Log.Information("Method Finished with code 200");
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddEntities([FromBody] SomeEntityDto[] someEntitiesDto, CancellationToken ct)
        {
            Log.Information("Start Save method");

            var someEntities = _mapper.MapWith(someEntitiesDto);

            await _service.SaveEntities(someEntities, ct);

            Log.Information("Method Finished with code 200");

            return Ok();
        }
    }
}
