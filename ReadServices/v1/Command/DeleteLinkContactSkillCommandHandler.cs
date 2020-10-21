using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OpenWebApiContract.Classes;
using OpenWebApiContract.Communication.LinkContactSkill;
using OpenWebData.Repository.v1;
namespace OpenWebServices.v1.Command
{
    public class DeleteLinkContactSkillCommandHandler : IRequestHandler<DeleteLinkContactSkillRequestV1, LinkContactSkillV1>
    {
        private readonly ILinkContactSkillRepository _linkRepository;
        private readonly IMapper _mapper;

        public DeleteLinkContactSkillCommandHandler(ILinkContactSkillRepository linkRepository, IMapper mapper)
        {
            _linkRepository = linkRepository;
            _mapper = mapper;
        }
        async Task<LinkContactSkillV1> IRequestHandler<DeleteLinkContactSkillRequestV1, LinkContactSkillV1>.Handle(DeleteLinkContactSkillRequestV1 request, CancellationToken cancellationToken)
        {
            var contactDeleted = await _linkRepository.DeleteById(request.Id, cancellationToken);
            return _mapper.Map<LinkContactSkillV1>(contactDeleted);
        }
    }
}
