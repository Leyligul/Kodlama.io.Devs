using Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;

using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Models;
using Application.Features.ProgrammingLanguages.Queries.GetListProgrammingLanguage;
using Application.Features.ProgrammingLanguageTechnologies.Commands.CreateTechnology;
using Application.Features.ProgrammingLanguageTechnologies.Commands.DeleteTechnology;
using Application.Features.ProgrammingLanguageTechnologies.Commands.UpdateTechnology;
using Application.Features.ProgrammingLanguageTechnologies.Dtos;
using Application.Features.ProgrammingLanguageTechnologies.Models;

using Application.Features.ProgrammingLanguageTechnologies.Query.GetListTechnology;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnologiesController : BaseController
    {
        [HttpGet("get")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListTechnologyQuery getListTchnologyQuery = new() { PageRequest = pageRequest };
            TechnologyListModel result = await Mediator.Send(getListTchnologyQuery);
            return Ok(result);
        }


        [HttpPost("create")]
        public async Task<IActionResult> Add([FromBody] CreateTechnologyCommand request)
        {
          
            CreatedTechnologyDto result = await Mediator.Send(request);
            return Created("Created successfully.", result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateTechnologyCommand updateTechnologyCommand)
        {
           
            UpdatedTechnologyDto result = await Mediator.Send(updateTechnologyCommand);
            return Ok(result);
        }


        [HttpDelete("delete/{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int Id)
        {
            DeleteTechnologyCommand deleteTechnologyCommand = new() { Id = Id };
            DeletedTechnologyDto result = await Mediator.Send(deleteTechnologyCommand);

            return Ok(result);
        }

    }
}
