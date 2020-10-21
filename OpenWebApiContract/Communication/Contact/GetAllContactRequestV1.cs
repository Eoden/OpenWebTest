using System.Collections.Generic;
using MediatR;
using OpenWebApiContract.Classes;

namespace OpenWebApiContract.Communication.Contact
{
    public class GetAllContactRequestV1 : IRequest<List<ContactV1>>
    {
        public GetAllContactRequestV1()
        {
        }
    }
}
