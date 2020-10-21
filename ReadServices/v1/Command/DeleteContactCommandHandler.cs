using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OpenWebApiContract.Classes;
using OpenWebApiContract.Communication.Contact;
using OpenWebData.Repository.v1;
namespace OpenWebServices.v1.Command
{
    public class DeleteContactCommandHandler : IRequestHandler<DeleteContactRequestV1,ContactV1>
    {
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public DeleteContactCommandHandler(IContactRepository contactRepository,IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }
        async Task<ContactV1> IRequestHandler<DeleteContactRequestV1, ContactV1>.Handle(DeleteContactRequestV1 request, CancellationToken cancellationToken)
        {
            var contactDeleted = await _contactRepository.DeleteById(request.Id, cancellationToken);
            return _mapper.Map<ContactV1>(contactDeleted);
        }
    }
}
