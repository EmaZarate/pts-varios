using AutoMapper;
using Hoshin.Core.Application.UseCases.CRUDSector.AddSectorUseCase;
using Hoshin.Core.Application.UseCases.CRUDSector.GetAllSectorsUseCase;
using Hoshin.Core.Application.UseCases.CRUDSector.GetOneSectorUseCase;
using Hoshin.Core.Application.UseCases.CRUDSector.UpdateSectorUseCase;
using Hoshin.Core.Domain.Sector;
using Hoshin.WebApi.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SectorClaim = Hoshin.CrossCutting.Authorization.Claims.Core.Sector;

namespace Hoshin.WebApi.Controllers.Hoshin.Core.UseCases.CRUDSectors
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(WebApiExceptionFilterAttribute))]
    public class SectorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGetAllSectorsUseCase _getAllSectorsUseCase;
        private readonly IGetOneSectorUseCase _getOneSectorUseCase;
        private readonly IAddSectorUseCase _addSectorUseCase;
        private readonly IUpdateSectorUseCase _updateSectorUseCase;

        public SectorController(
            IMapper mapper,
            IGetAllSectorsUseCase getAllSectorsUseCase,
            IGetOneSectorUseCase getOneSectorUseCase,
            IAddSectorUseCase addSectorUseCase,
            IUpdateSectorUseCase updateSectorUseCase
            )
        {
            _mapper = mapper;
            _getAllSectorsUseCase = getAllSectorsUseCase;
            _getOneSectorUseCase = getOneSectorUseCase;
            _addSectorUseCase = addSectorUseCase;
            _updateSectorUseCase = updateSectorUseCase;
        }

        [HttpGet]
        [Authorize(Policy = SectorClaim.ViewSector)]
        [ServiceFilter(typeof(CacheEndpointFilter))]
        public async Task<IActionResult> Get()
        {           
            return new OkObjectResult(await _getAllSectorsUseCase.Execute());
        }

        [HttpGet("{id}")]
        [Authorize(Policy = SectorClaim.ViewSector)]
        public IActionResult GetOne(int id)
        {
            return new OkObjectResult(_getOneSectorUseCase.Execute(id));
        }

        [HttpPost]
        [Authorize(Policy = SectorClaim.AddSector)]
        public IActionResult Add([FromBody] SectorDTO sector)
        {
            return new OkObjectResult(_addSectorUseCase.Execute(_mapper.Map<SectorDTO, Sector>(sector)));
        }

        [HttpPut]
        [Authorize(Policy = SectorClaim.EditSector)]
        [Authorize(Policy = SectorClaim.ActivateSector)]
        [Authorize(Policy = SectorClaim.DeactivateSector)]
        public IActionResult Update([FromBody] SectorDTO sector)
        {
            return new OkObjectResult(_updateSectorUseCase.Execute(_mapper.Map<SectorDTO, Sector>(sector)));
        }
    }
}