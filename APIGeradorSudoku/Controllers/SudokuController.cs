using APIGeradorSudoku.Services;
using APIGeradorSudoku.Entities;
using APIGeradorSudoku.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIGeradorSudoku.Controllers
{
    [Route("api/sudoku")]
    [ApiController]
    public class SudokuController : ControllerBase
    {
        private readonly ISudokuService _sudokuService;
        public SudokuController(ISudokuService sudokuService)
        {
            _sudokuService = sudokuService;
        }

        [HttpGet]
        public IActionResult GetSudokuJogavel([FromQuery] NivelEnum nivelDificuldade)
        {
            if (!Enum.IsDefined(typeof(NivelEnum), nivelDificuldade))
            {
                return BadRequest("Nível de dificuldade inválido. Valores permitidos: 1 (Fácil), 2 (Médio), 3 (Difícil).");
            }

            var sudoku = _sudokuService.CriarGradeDeSudokuJogavel(nivelDificuldade);

            var sudokuDTO = SudokuMapper.ToDTO(sudoku);

            return Ok(sudokuDTO);
        }
    }
}
