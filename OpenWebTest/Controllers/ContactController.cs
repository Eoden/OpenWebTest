using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenWebApiContract.Classes;
using OpenWebApiContract.Communication.Contact;
using RestServices.BasicAuth;

namespace RestServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IMediator _mediator;


        public ContactController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
         
        /// <summary>
        ///     Get all contacts
        /// </summary>
        /// <returns>Returns a list of all contacts in database</returns>
        /// <response code="200">Returned if there is at lease 1 contact in database</response>
        /// <response code="400">Returned if the contact could not be retrieved</response>
        /// <response code="401">You need to be log (you can use the login and password you want)</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet]
        [BasicAuth]
        public async Task<ActionResult<List<ContactV1>>> GetAllContacts()
        {
            try
            {
                return await _mediator.Send(new GetAllContactRequestV1());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        ///     Get a contact by id
        /// </summary>
        /// <returns>Returns a contanct depending on the id</returns>
        /// <response code="200">Returned if the contact is found in database</response>
        /// <response code="400">Returned if the contact could not be retrieved</response>
        /// <response code="401">You need to be log (you can use the login and password you want)</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactV1>> Get(int id)
        {
            try
            {
                return await _mediator.Send(new GetContactRequestV1 { Id = id });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// <summary>
        ///     Create a contact
        /// </summary>
        /// <returns>Create a contact. (the id is autoIncrement)</returns>
        /// <response code="200">Returned if the contact have been corectly created</response>
        /// <response code="400">Returned if the contact could not be retrieved</response>
        /// <response code="401">You need to be log (you can use the login and password you want)</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        [BasicAuth]
        public async Task<ActionResult<ContactV1>> PostAsync([FromBody] CreateContactRequestV1 contact)
        {
            try
            {
                return await _mediator.Send(contact);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        ///     Update a contact
        /// </summary>
        /// <returns>Update one contact depending on the id. Waring ! the id and the id inside the body have to be se same !</returns>
        /// <response code="200">Returned if the contact have been corectly updated</response>
        /// <response code="400">Returned if the contact could not be retrieved</response>
        /// <response code="401">You need to be log (you can use the login and password you want)</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("{id}")]
        [BasicAuth]
        public async Task<ActionResult<ContactV1>> Put(int id, [FromBody] UpdateContactRequestV1 contact)
        {
            if (id != contact.Id)
            {
                return BadRequest("The id in parametter is different from the id inside the contact");
            }
            
            try
            {
                return await _mediator.Send(contact);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        ///     Delete a contact
        /// </summary>
        /// <returns>Delete one contact depending on the id.</returns>
        /// <response code="200">Returned if the contact have been corectly Deleted</response>
        /// <response code="400">Returned if the contact could not be retrieved</response>
        /// <response code="401">You need to be log (you can use the login and password you want)</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var request = new DeleteContactRequestV1 { Id = id };
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
