using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Data.Logging;
using MagicVilla_VillaAPI.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VillaAPIController : ControllerBase
    {
        private readonly ILogging _logger;

        public VillaAPIController(ILogging logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<VillaDto> GetVillas()
        {
            _logger.log("Getting all villas", "");
            return VillaStore.villaList;
        }

        [HttpGet("{id:int}")]
        public ActionResult<VillaDto> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var vila = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            if (vila == null)
            {
                return NotFound();
            }
            return Ok(vila);
        }
        [HttpPost]
        public ActionResult<VillaDto> CreateVilla([FromBody] VillaDto villaDTO)
        {
            if (villaDTO == null || villaDTO.Id > 0)
            {
                return BadRequest(villaDTO);
            }
            villaDTO.Id = VillaStore.villaList
                .OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
            VillaStore.villaList.Add(villaDTO);
            return Ok(villaDTO);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            VillaStore.villaList.Remove(villa);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDto villaDto)
        {
            if (villaDto.Id != id || villaDto == null)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            villa.Name = villaDto.Name;
            villa.Sqft = villaDto.Sqft;
            villa.Occupancy = villaDto.Occupancy;

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDto> patchDto)
        {
            if (id == 0 || patchDto == null)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);

            if (villa == null)
            {
                return NotFound();
            }
            patchDto.ApplyTo(villa, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}
