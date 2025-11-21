using Business;
using Microsoft.AspNetCore.Mvc;
using Model.DTO.Funcionario;

namespace FutureWork.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class FuncionariosController : ControllerBase
    {
        private readonly IFuncionarioService _funcionarioService;

        public FuncionariosController(IFuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAll()
        {
            var funcionarios = await _funcionarioService.GetAllAsync();
            if (funcionarios == null || !funcionarios.Any())
                return NoContent(); 

            return Ok(); 
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var funcionario = await _funcionarioService.GetByIdAsync(id);
            if (funcionario == null)
                return NotFound(); 

            return Ok(); 
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateFuncionarioDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(); 

            var created = await _funcionarioService.CreateAsync(dto);

            var locationUrl = Url.Action(nameof(GetById), new { id = created.Id });
            Response.Headers.Location = locationUrl ?? string.Empty;

            return StatusCode(StatusCodes.Status201Created); 
        }

      
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateFuncionarioDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(); 

            try
            {
                await _funcionarioService.UpdateAsync(id, dto);
                return NoContent(); 
            }
            catch (KeyNotFoundException)
            {
                return NotFound(); 
            }
        }

        
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _funcionarioService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent(); 
        }
    }


}
