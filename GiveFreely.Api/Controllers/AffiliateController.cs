using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using FluentValidation;
using GiveFreely.Contracts.Engine;
using Microsoft.Extensions.Logging;
using GiveFreely.Models;

namespace GiveFreely.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AffiliateController : ControllerBase
    {

        private IAffiliateEngine  _affiliateService;
        private readonly IValidator<Models.Affiliate> _affiliateValidator;
        private readonly ILogger<AffiliateController> _logger;

        public AffiliateController(IAffiliateEngine affiliateService,
            IValidator<Models.Affiliate> affiliateValidator,
            ILogger<AffiliateController> logger)
        {
            _affiliateService = affiliateService;
            _affiliateValidator = affiliateValidator;
            _logger = logger;
        }

        [HttpPost]
        [Route("/CreateAffiliate")]
        public async Task<IActionResult> Create(Affiliate newAffiliate)
        {
            var resultValidator = _affiliateValidator.Validate(newAffiliate);

            if (!resultValidator.IsValid)
            {
                return BadRequest(string.Join(", ", resultValidator.Errors));
            }
            try
            {
                var created = await _affiliateService.Add(newAffiliate);

                if (created == null)
                {
                    return BadRequest("The affiliate can't be created");
                }
                else
                {
                    return StatusCode(StatusCodes.Status201Created, created);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Create affiliate error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("/GetsAffiliate")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var listAffiliate = await _affiliateService.GetAll();
                return StatusCode(StatusCodes.Status200OK, listAffiliate);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Gets Affiliate error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("/GetAffiliate/{Id:int}")]
        public async Task<IActionResult> Get(int Id)
        {
            try
            {
                var affiliate = await _affiliateService.GetById(Id); 
                if (affiliate == null)
                {
                    return BadRequest("The affiliate doesn't exit");
                }
                return StatusCode(StatusCodes.Status200OK, affiliate); ;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get Affiliate error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
