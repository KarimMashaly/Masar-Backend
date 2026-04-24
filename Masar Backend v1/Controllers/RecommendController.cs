using Masar_Backend_v1.Models;
using Masar_Backend_v1.Services;
using Microsoft.AspNetCore.Mvc;

namespace Masar_Backend_v1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendController : ControllerBase
    {
        private readonly IMasarService _masarService;
        
        public RecommendController(IMasarService masarService)
        {
            _masarService = masarService;
        }

        [HttpPost]
        public async Task<IActionResult> Recommend([FromBody] AnswersRequest request)
        {
            var result = await _masarService.GetRecommendationAsync(request.Answers);
            return Ok(result);
        }
    }
}
