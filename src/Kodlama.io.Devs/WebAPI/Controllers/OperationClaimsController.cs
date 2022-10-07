using Application.Features.OperationClaims.Commands;
using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Models;
using Application.Features.OperationClaims.Queries.GetListOperationClaimQuery;
using Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Models;
using Application.Features.ProgrammingLanguages.Queries.GetListProgrammingLanguage;
using Core.Application.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimsController : BaseController
    {


        [HttpPost("create")]
        [Authorize] 
        public async Task<IActionResult> Add([FromBody] CreateOperationClaimCommand createOperationClaimCommand)
        {
            CreatedOperationClaimDto result = await Mediator.Send(createOperationClaimCommand);
            return Created("Created successfully.", result);
        }

        [HttpPut("update/{Id}")]
        public async Task<IActionResult> Update([FromRoute] int Id, [FromBody] string name)
        {
            UpdateOperationClaimCommand updateOperationClaimCommand = new() { Id = Id , Name = name};
            UpdatedOperationClaimDto result = await Mediator.Send(updateOperationClaimCommand);
            return Created("Created successfully.", result);
        }

        [HttpDelete("delete/{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int Id)
        {
            DeleteOperationClaimCommand deleteOperationClaimCommand = new() { Id = Id };
            DeletedOperationClaimDto result = await Mediator.Send(deleteOperationClaimCommand);
            return Created("Created successfully.", result);
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery] PageRequest pageRequest)
        {
            GetListOperationClaimQuery getListOperationClaimQuery = new() { PageRequest = pageRequest };
            OperationClaimListModel result = await Mediator.Send(getListOperationClaimQuery);
            return Ok(result);
        }
    }
}
