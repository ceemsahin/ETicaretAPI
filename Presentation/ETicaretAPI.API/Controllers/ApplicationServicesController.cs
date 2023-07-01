using ETicaretAPI.Application.Abstractions.Services.Configuraitons;
using ETicaretAPI.Application.CustomAttributes;
using ETicaretAPI.Application.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Admin")]
    public class ApplicationServicesController : ControllerBase
    {

        IApplicationService _applicationService;

        public ApplicationServicesController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }
        [HttpGet]
        [AuthorizeDefinition(ActionType =ActionType.Reading,Definition ="Get Authorize Definition Endpoint",Menu ="Application Services")]
        public IActionResult GetAuthorizeDefinationEndpoints()
        {
           
            var datas = _applicationService.GetAuthorizeDefinitionEndpoints(typeof(Program));  

            
            
            return Ok(datas);
        }

    }
}
