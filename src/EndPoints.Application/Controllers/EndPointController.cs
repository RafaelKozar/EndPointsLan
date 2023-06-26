using EndPoints.Application.Services;
using EndPoints.Core;
using EndPoints.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EndPoints.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EndPointController : ControllerBase
    {
        private readonly IEndPointService _endPointService;

        public EndPointController(IEndPointService endPointService)
        {
            _endPointService = endPointService;
        }

        [HttpGet]
        public async Task<IEnumerable<EndPointGyrDto>> Get()
        {
            return await _endPointService.GetAllEndPoints(); 
        }

        [HttpPost]
        public async Task<IActionResult> Post(EndPointGyrDto endPointGyr)
        {
            try
            {
                await _endPointService.SaveEndPoint(endPointGyr);
                return Ok();
            }
            catch(DomainException ex)
            {
                return BadRequest(ex.Message);  
            }            
        }

        [HttpPost]
        [Route("Update")]   
        public async Task<IActionResult> Update(EndPointGyrDto endPointGyr)
        {
            try
            {
                await _endPointService.UpdateEndPoint(endPointGyr);
                return Ok();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetBySerialNumber")]
        public async Task<IActionResult> GetBySerialNumber(string serialNumber)
        {
            try
            {
                return Ok(await _endPointService.GetBySerialNumber(serialNumber));
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("Delete")]
        public async Task<IActionResult> Remove(string serialNumber)
        {
            try
            {
                await _endPointService.RemoveEndPoint(serialNumber);
                return Ok();    
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
