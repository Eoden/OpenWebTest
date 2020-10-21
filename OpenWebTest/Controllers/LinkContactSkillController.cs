using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenWebApiContract.Classes;
using OpenWebApiContract.Communication.LinkContactSkill;
using RestServices.BasicAuth;

namespace RestServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinkContactSkillController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public LinkContactSkillController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }



        /// <summary>
        ///     Get all links
        /// </summary>
        /// <returns>Returns a list of all links in database</returns>
        /// <response code="200">Returned if there is at lease 1 link in database</response>
        /// <response code="400">Returned if the link could not be retrieved</response>
        /// <response code="401">You need to be log (you can use the login and password you want)</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet]
        [BasicAuth]
        public async Task<ActionResult<List<LinkContactSkillV1>>> GetAllLink()
        {
            try
            {
                return await _mediator.Send(new GetAllLinkContactSkillRequestV1());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        ///     Create a Link
        /// </summary>
        /// <returns>Create a link. (the id is autoIncrement)</returns>
        /// <response code="200">Returned if the link have been corectly created</response>
        /// <response code="400">Returned if the link could not be retrieved</response>
        /// <response code="401">You need to be log (you can use the login and password you want)</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        [BasicAuth]
        public async Task<ActionResult<LinkContactSkillV1>> PostAsync([FromBody] CreateLinkContactSkillRequestV1 linkContactSkill)
        {
            try
            {
                return await _mediator.Send(linkContactSkill);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        ///     Delete a link
        /// </summary>
        /// <returns>Delete one link depending on the id.</returns>
        /// <response code="200">Returned if the link have been corectly Deleted</response>
        /// <response code="400">Returned if the link could not be retrieved</response>
        /// <response code="401">You need to be log (you can use the login and password you want)</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var request = new DeleteLinkContactSkillRequestV1 { Id = id };
                await _mediator.Send(request);

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("basic-logout")]
        [BasicAuth]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult BasicAuthLogout()
        {
            HttpContext.Response.Headers["WWW-Authenticate"] = "Basic realm=\"My Realm\"";
            return new UnauthorizedResult();
        }
    }
}
