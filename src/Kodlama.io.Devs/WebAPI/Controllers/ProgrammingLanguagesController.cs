using Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Models;
using Application.Features.ProgrammingLanguages.Queries.GetByIdProgrammingLanguageQuery;
using Application.Features.ProgrammingLanguages.Queries.GetListProgrammingLanguage;
using Core.Application.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgrammingLanguagesController : BaseController
    {

        [HttpPost("create")]
        public async Task<IActionResult> Add([FromBody] CreateProgrammingLanguageCommand createProgrammingLanguageCommand)
        {
            CreatedProgrammingLanguageDto result = await Mediator.Send(createProgrammingLanguageCommand);
            return Created("Created successfully.", result);
        }

        [HttpPut("update/{Id}")]
        public async Task<IActionResult> Update([FromRoute] int Id, [FromBody] string name)
        {
            UpdateProgrammingLanguageCommand updateProgrammingLanguageCommand = new() { Id = Id , Name = name};
            UpdatedProgrammingLanguageDto result = await Mediator.Send(updateProgrammingLanguageCommand);
            return Ok(result);
        }

        //[HttpPut("update")]
        //public async Task<IActionResult> Update([FromBody] UpdateProgrammingLanguageCommand updateProgrammingLanguageCommand )
        //{
        //     UpdatedProgrammingLanguageDto result = await Mediator.Send(updateProgrammingLanguageCommand);
        //    return Ok(result);
        //}

        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery] PageRequest pageRequest)
        {
            GetListProgrammingLanguageQuery getListLanagugeQuery =  new() { PageRequest = pageRequest };
            ProgrammingLanguageListModel result = await Mediator.Send(getListLanagugeQuery);
            return Ok(result);
        }

        [HttpGet("get/{Id}")]
        public async Task<IActionResult> Get([FromRoute] GetByIdProgrammingLanguageQuery request)
        {
            ProgrammingLanguageGetByIdDto result = await Mediator.Send(request);
         
            return Ok(result);
        }

        [HttpDelete("delete/{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteProgrammingLanguageCommand request)
        {
            DeletedProgrammingLanguageDto result = await Mediator.Send(request);

            return Ok(result);
        }
    }
}
