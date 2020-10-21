using System.Net;
using AutoMapper;
using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OpenWebApiContract.Classes;
using FluentAssertions;
using OpenWebApiContract.Communication.Contact;
using OpenWebData.Data.Contact;
using RestServices.Controllers;
using Xunit;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using OpenWebUnitsTests.Fixture;
using RestServices.Mapping;
using Moq;
using OpenWebServices.v1.Command;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using OpenWebServices.v1.Query;
using System;

namespace OpenWebUnitsTests.Controller
{
    public class TestContactController 
    {

        private IMediator _mediator;
        private ContactV1 _contactCreateReturned;
        private ContactV1 _contactUpdateReturned;
        private CreateContactRequestV1 _createContactValid;
        private CreateContactRequestV1 _createContactWithInvalidMail;
        private UpdateContactRequestV1 _updateContact;
        private ContactController _contactController;
        protected TestServer Server { get; }
        protected HttpClient Client { get; }

        private readonly MapperConfiguration mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingInfos());
        });
        public TestContactController()
        {
            CreationOfTestCasesAndMock();
        }

        private void CreationOfTestCasesAndMock()
        {

            _mediator = A.Fake<IMediator>();
            _contactController = new ContactController(_mediator);

            _contactCreateReturned = new ContactV1
            {
                Firstname = "Arthur",
                Lastname = "Picaud",
                Fullname = "Arthur Picaud",
                Address = "18 rue Colbert Lille",
                Email = "95504@supinfo.com",
                MobilePhoneNumber = "+33 7 89 73 78 62"
            };
            _createContactValid = new CreateContactRequestV1
            {
                Firstname = "Arthur",
                Lastname = "Picaud",
                Address = "18 rue Colbert Lille",
                Email = "95504@supinfo.com",
                MobilePhoneNumber = "+33 7 89 73 78 62"
            };
            _createContactWithInvalidMail = new CreateContactRequestV1
            {
                Firstname = "Arthur",
                Lastname = "Picaud",
                Address = "18 rue Colbert Lille",
                Email = "badRegex",
                MobilePhoneNumber = "+33 7 89 73 78 62"
            };
            _updateContact = new UpdateContactRequestV1
            {
                Id = 7,
                Firstname = "James",
                Lastname = "Bond",
                Address = "MI6 London",
                Email = "007@MI6.uk",
                MobilePhoneNumber = "00 70 07 00 70"
            };
            _contactUpdateReturned = new ContactV1
            {
                Firstname = "James",
                Lastname = "Bond",
                Fullname = "James Bond",
                Address = "MI6 London",
                Email = "007@MI6.uk",
                MobilePhoneNumber = "00 70 07 00 70"
            };

        }


        [Fact]
        public async void ContactCreationNominal()
        {
            //Arrange
            A.CallTo(() => _mediator.Send(_createContactValid, default)).Returns(_contactCreateReturned);

            //Act
            var result = await _contactController.PostAsync(_createContactValid);

            //Assert
            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Should().NotBeNull();
            
            result.Value.Lastname.Should().Be(_contactCreateReturned.Lastname);
            result.Value.Firstname.Should().Be(_contactCreateReturned.Firstname);
            result.Value.Fullname.Should().Be(_contactCreateReturned.Fullname);
            result.Value.Should().BeOfType<ContactV1>();
        }

        [Theory]
        [InlineData("CreateContact: email is invalid")]
        public async void Create_WhenAnExceptionOccurs_ShouldReturnBadRequest(string exceptionMessage)
        {
            A.CallTo(() => _mediator.Send(_createContactWithInvalidMail, default)).Throws(new Exception(exceptionMessage));

            var result = await _contactController.PostAsync(_createContactWithInvalidMail);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            (result.Result as BadRequestObjectResult)?.Value.Should().Be(exceptionMessage);
        }

        [Fact]
        public async void ContactUpdateNominal()
        {
            //Arrange
            A.CallTo(() => _mediator.Send(_updateContact, default)).Returns(_contactUpdateReturned);

            //Act
            var result = await _contactController.Put(7,_updateContact);
            
            //Assert
            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Should().NotBeNull();

            result.Value.Lastname.Should().Be(_contactUpdateReturned.Lastname);
            result.Value.Firstname.Should().Be(_contactUpdateReturned.Firstname);
            result.Value.Fullname.Should().Be(_contactUpdateReturned.Fullname);
            result.Value.Should().BeOfType<ContactV1>();
        }

        [Fact]
        public async void Update_WhenIdIsDiffertentFromContact()
        {
            //Arrange
            A.CallTo(() => _mediator.Send(_updateContact, default)).Returns(_contactUpdateReturned);

            //Act
            var result = await _contactController.Put(1, _updateContact);

            //Assert
            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Should().NotBeNull();

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            (result.Result as BadRequestObjectResult)?.Value.Should().Be("The id in parametter is different from the id inside the contact");
        }
    }
}
