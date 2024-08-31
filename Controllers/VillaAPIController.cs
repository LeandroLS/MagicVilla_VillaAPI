using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Data.Logging;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VillaAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogging _logger;

        public VillaAPIController(ILogging logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        public ActionResult<VillaDto> GetVillas()
        {
            _logger.log("Getting all villas", "");
            return Ok(_db.Villas);
        }

        [HttpGet("{id:int}")]
        public ActionResult<VillaDto> GetVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var vila = _db.Villas.FirstOrDefault(v => v.Id == id);
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
            
            var villa = new Villa()
            {
                Name = villaDTO.Name,
                Amenity = villaDTO.Amenity,
                ImageUrl =  villaDTO.ImageUrl,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,
                Details = villaDTO.Details,
            };
            _db.Add(villa);
            _db.SaveChanges();
            return Ok(villa);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = _db.Villas.FirstOrDefault(v => v.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            _db.Villas.Remove(villa);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDto villaDto)
        {
            if (villaDto.Id != id || villaDto == null)
            {
                return BadRequest();
            }
            var model = new Villa()
            {
                Name = villaDto.Name,
                Amenity = villaDto.Amenity,
                ImageUrl =  villaDto.ImageUrl,
                Occupancy = villaDto.Occupancy,
                Rate = villaDto.Rate,
                Sqft = villaDto.Sqft,
                Id = villaDto.Id
            };

            _db.Villas.Update(model);
            _db.SaveChanges();
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
