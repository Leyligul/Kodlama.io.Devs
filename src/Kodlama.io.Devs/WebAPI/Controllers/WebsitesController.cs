using Application.Features.ProgrammingLanguageTechnologies.Commands.CreateTechnology;
using Application.Features.ProgrammingLanguageTechnologies.Commands.DeleteTechnology;
using Application.Features.ProgrammingLanguageTechnologies.Dtos;
using Application.Features.Websites.Commands.CreateWebsiteCommands;
using Application.Features.Websites.Commands.DeleteWebsiteCommands;
using Application.Features.Websites.Commands.UpdateWebsiteCommands;
using Application.Features.Websites.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebsitesController : BaseController
    {
        [HttpPost("create")]
        public async Task<IActionResult> Add([FromBody] CreateWebsiteCommand request)
        {

            CreatedWebsiteDto result = await Mediator.Send(request);
            return Created("Created successfully.", result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateWebsiteCommand request)
        {

            UpdatedWebsiteDto result = await Mediator.Send(request);
            return Created("Updated successfully.", result);
        }

        [HttpDelete("delete/{Id}")]
        public async Task<IActionResult> Delete([FromRoute]int Id)
        {
            DeleteWebsiteCommand deleteWebsiteCommand = new() { Id = Id };
            DeletedWebsiteDto result = await Mediator.Send(deleteWebsiteCommand);
            return Created("Deleted successfully.", result);
        }
    }
}
