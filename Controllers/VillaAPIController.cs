using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VillaAPIController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<VillaDto> GetVillas()
        {
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
        public ActionResult<VillaDto> CreateVilla([FromBody]VillaDto villaDTO)
        {
            if(villaDTO == null || villaDTO.Id > 0)
            {
                return BadRequest(villaDTO);
            }
            villaDTO.Id = VillaStore.villaList
                .OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
            VillaStore.villaList.Add(villaDTO);
            return Ok(villaDTO);
        }
    }
}
