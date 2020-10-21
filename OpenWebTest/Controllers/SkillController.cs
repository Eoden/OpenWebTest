using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenWebApiContract.Classes;
using OpenWebApiContract.Communication.Skill;
using RestServices.BasicAuth;

namespace RestServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SkillController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }



        /// <summary>
        ///     Get all skills
        /// </summary>
        /// <returns>Returns a list of all skills in database</returns>
        /// <response code="200">Returned if there is at lease 1 skill in database</response>
        /// <response code="400">Returned if the skill could not be retrieved</response>
        /// <response code="401">You need to be log (you can use the login and password you want)</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet]
        [BasicAuth]
        public async Task<ActionResult<List<SkillV1>>> GetAllSkills()
        {
            try
            {
                return await _mediator.Send(new GetAllSkillRequestV1());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        ///     Get a skill by id
        /// </summary>
        /// <returns>Returns a skill depending on the id</returns>
        /// <response code="200">Returned if the skill is found in database</response>
        /// <response code="400">Returned if the skill could not be retrieved</response>
        /// <response code="401">You need to be log (you can use the login and password you want)</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("{id}")]
        public async Task<ActionResult<SkillV1>> Get(int id)
        {
            try
            {
                return await _mediator.Send(new GetSkillRequestV1 { Id = id });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// <summary>
        ///     Create a skill
        /// </summary>
        /// <returns>Create a skill. (the id is autoIncrement)</returns>
        /// <response code="200">Returned if the skill have been corectly created</response>
        /// <response code="400">Returned if the skill could not be retrieved</response>
        /// <response code="401">You need to be log (you can use the login and password you want)</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        [BasicAuth]
        public async Task<ActionResult<SkillV1>> PostAsync([FromBody] CreateSkillRequestV1 skill)
        {
            try
            {
                return await _mediator.Send(skill);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        ///     Update a skill
        /// </summary>
        /// <returns>Update one skill depending on the id. Warning ! the id and the id inside the body have to be se same !</returns>
        /// <response code="200">Returned if the skill have been corectly updated</response>
        /// <response code="400">Returned if the skill could not be retrieved</response>
        /// <response code="401">You need to be log (you can use the login and password you want)</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("{id}")]
        [BasicAuth]
        public async Task<ActionResult<SkillV1>> Put(int id, [FromBody] UpdateSkillRequestV1 skill)
        {
            if (id != skill.Id)
            {
                return BadRequest("The id in parametter is different from the id inside the skill");
            }

            try
            {
                return await _mediator.Send(skill);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        ///     Delete a skill
        /// </summary>
        /// <returns>Delete one skill depending on the id.</returns>
        /// <response code="200">Returned if the skill have been corectly Deleted</response>
        /// <response code="400">Returned if the skill could not be retrieved</response>
        /// <response code="401">You need to be log (you can use the login and password you want)</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var request = new DeleteSkillRequestV1 { Id = id };
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
