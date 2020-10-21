using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OpenWebApiContract.Classes;
using OpenWebApiContract.Communication.Contact;
using OpenWebData.Repository.v1;

namespace OpenWebServices.v1.Query
{
    public class GetContactByIdQueryHandler : IRequestHandler<GetContactRequestV1, ContactV1>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public GetContactByIdQueryHandler(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }


        public async Task<ContactV1> Handle(GetContactRequestV1 request, CancellationToken cancellationToken)
        {
            //var contact = 
           // var listSkillsFromContactId
            return _mapper.Map<ContactV1>(await _contactRepository.GetContactById(request.Id, cancellationToken));
        }
    }
}
