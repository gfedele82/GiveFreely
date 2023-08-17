using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using GiveFreely.Contracts.Engine;
using GiveFreely.Models;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace GiveFreely.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private ICustomerEngine _customerService;
        private readonly IValidator<Models.Customer> _customerValidator;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ICustomerEngine customerService,
            IValidator<Models.Customer> customerValidator,
            ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _customerValidator = customerValidator;
            _logger = logger;
        }

        [HttpPost]
        [Route("/CreateCustomer")]
        public async Task<IActionResult> Create(Customer newCustomer)
        {
            var resultValidator = _customerValidator.Validate(newCustomer);
            
            if (!resultValidator.IsValid)
            {
                return BadRequest(string.Join(", ", resultValidator.Errors));
            }
            try
            {         
                var created = await _customerService.Add(newCustomer);
                
                if(created == null)
                {
                    return BadRequest("The customer can't be created");
                }
                else if (created.IdCustomer == 0)
                {
                    return BadRequest("The customer can't be created. Affiliate doesn't exit");
                }
                else
                {
                    return StatusCode(StatusCodes.Status201Created, created);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Create customer error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("/GetsCustomer")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var listCustomer = await _customerService.GetAll();
                return StatusCode(StatusCodes.Status200OK, listCustomer);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Gets Customer error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Route("/GetCustomer/{Id:int}")]
        public async Task<IActionResult> Get(int Id)
        {
            try
            {
                var customer = await _customerService.GetById(Id);
                if(customer == null)
                {
                    return BadRequest("The customer doesn't exit");
                }
                return StatusCode(StatusCodes.Status200OK, customer); ;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get Customer error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    }

}
