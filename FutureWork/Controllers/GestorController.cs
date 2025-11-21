using Business;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.DTO.Gestor;

namespace FutureWork.Controllers
{


    [ApiController]
    [Route("api/v1/[controller]")]
    public class GestorController : ControllerBase
    {
        private readonly IGestorService _gestorService;

        public GestorController(IGestorService gestorService)
        {
            _gestorService = gestorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GestorDto>>> GetAll()
        {
            var gestores = await _gestorService.GetAllAsync();
            return Ok(gestores);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GestorDto>> GetById(int id)
        {
            var gestor = await _gestorService.GetByIdAsync(id);
            if (gestor == null) return NotFound(new { message = "Gestor não encontrado" });
            return Ok(gestor);
        }

        [HttpPost]
        public async Task<ActionResult<GestorDto>> Create([FromBody] CreateGestorDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _gestorService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<GestorDto>> Update(int id, [FromBody] UpdateGestorDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var updated = await _gestorService.UpdateAsync(id, dto);
                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Gestor não encontrado" });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _gestorService.DeleteAsync(id);
            if (!deleted) return NotFound(new { message = "Gestor não encontrado" });
            return NoContent();
        }
    }


}
