using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using FluentValidation;
using GiveFreely.Contracts.Engine;
using Microsoft.Extensions.Logging;

namespace GiveFreely.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private IReportEngine _reportService;
        private readonly ILogger<AffiliateController> _logger;

        public ReportController(IReportEngine reportService,
            ILogger<AffiliateController> logger)
        {
            _reportService = reportService;
            _logger = logger;
        }

        [HttpGet]
        [Route("/GetReport/{Id:int}")]
        public async Task<IActionResult> Get(int Id)
        {
            try
            {
                var report = await _reportService.Generate(Id);
                if (report.Number == 0)
                {
                    return BadRequest("The affiliate doesn't exit");
                }
                else if (report == null) 
                {
                    return BadRequest("The report can't be created");
                }
                return StatusCode(StatusCodes.Status200OK, report); ;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get Customer error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
