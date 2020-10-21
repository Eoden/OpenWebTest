using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OpenWebApiContract.Classes;
using OpenWebApiContract.Communication.Contact;
using OpenWebData.Data.Contact;
using OpenWebData.Repository.v1;

namespace OpenWebServices.v1.Command
{
    public class UpdateContactCommandHandler : IRequestHandler <UpdateContactRequestV1, ContactV1>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public UpdateContactCommandHandler(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        public async Task<ContactV1> Handle(UpdateContactRequestV1 request, CancellationToken cancellationToken)
        {
            var contactUpdated = await _contactRepository.Update(_mapper.Map<Contact>(request));
            return _mapper.Map<ContactV1>(contactUpdated);
        }
    }
}
