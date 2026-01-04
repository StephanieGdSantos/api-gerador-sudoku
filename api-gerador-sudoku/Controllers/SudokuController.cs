using APIGeradorSudoku.Decorators;
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
        private readonly ISudokuDecorator _sudokuDecorator;
        public SudokuController(ISudokuDecorator sudokuDecorator)
        {
            _sudokuDecorator = sudokuDecorator;
        }

        [HttpGet]
        public IActionResult GetSudokuJogavel([FromQuery] NivelEnum nivelDificuldade)
        {
            if (!Enum.IsDefined(typeof(NivelEnum), nivelDificuldade))
            {
                return BadRequest("Nível de dificuldade inválido. Valores permitidos: 1 (Fácil), 2 (Médio), 3 (Difícil).");
            }

            var sudoku = _sudokuDecorator.CriarGradeDeSudokuJogavel(nivelDificuldade);

            var sudokuDTO = SudokuMapper.ToDTO(sudoku);

            return Ok(sudokuDTO);
        }
    }
}
