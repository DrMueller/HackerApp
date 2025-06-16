using HackerApp.Client.Areas.RunningGame.Dtos;
using HackerApp.Client.Infrastructure.State.Dtos;
using HackerApp.Infrastructure.SemKer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HackerApp.Apis.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/game")]
    public class GameController(IGameAnalyzer gameAnalyzer) : ControllerBase
    {
        [HttpPost("analyze")]
        public async Task<IActionResult> AnalyzeAsync([FromBody] GameDto game)
        {
            var analysisText = await gameAnalyzer.AnalyzeAsync(game);

            var dto = new GameAnalysisResultDto
            {
                Text = analysisText
            };

            return Ok(dto);
        }
    }
}