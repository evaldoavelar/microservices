using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.DTO;
using PlatformService.Models;
using PlatformService.SyncDataServices.http;

namespace PlatformService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo platformRepo;
        private readonly IMapper mapper;
        private readonly ICommandDataClient commandDataClient;

        public PlatformsController(IPlatformRepo platformRepo, IMapper mapper, ICommandDataClient commandDataClient)
        {
            this.commandDataClient = commandDataClient;
            this.platformRepo = platformRepo;
            this.mapper = mapper;
        }

        [HttpGet()]
        [Produces("application/json")]
        public ActionResult<IEnumerable<PlatformReadDTO>> GetPlatforms()
        {
            Console.WriteLine("---> Getting platforms");

            var platforms = this.platformRepo.GetAllPlatforms();

            if (platforms == null)
                return NotFound();

            return Ok(this.mapper.Map<IEnumerable<Platform>, IEnumerable<PlatformReadDTO>>(platforms));
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        [Produces("application/json")]
        public ActionResult<Platform> GetPlatformById(int id)
        {
            var platform = this.platformRepo.GetPlatformById(id);

            if (platform == null)
                return NotFound("No platform with this id");

            return Ok(this.mapper.Map<Platform, PlatformReadDTO>(platform));
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Platform>> CreatePlatform(PlatformCreatDTO creatDTO)
        {
            if (ModelState.IsValid == false)
                return BadRequest("platfotm is no valid!");

            var platform = this.mapper.Map<PlatformCreatDTO, Platform>(creatDTO);
            this.platformRepo.CreatePlatform(platform);
            this.platformRepo.SaveChanges();

            var dto = this.mapper.Map<Platform, PlatformReadDTO>(platform);

            try
            {
                await this.commandDataClient.SendPlatformCommand(dto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($" could not send synchronously {ex.Message} {ex.InnerException}");
            }

            return CreatedAtRoute(nameof(GetPlatformById), new { id = dto.Id }, dto);
        }

    }
}