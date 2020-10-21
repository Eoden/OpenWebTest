using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OpenWebApiContract.Classes;
using OpenWebApiContract.Communication.LinkContactSkill;
using OpenWebData.Data.LinkContactSkill;
using OpenWebData.Repository.v1;

namespace OpenWebServices.v1.Command
{
    public class CreateLinkContactSkillCommandHandler : IRequestHandler<CreateLinkContactSkillRequestV1, LinkContactSkillV1>
    {
        private readonly ILinkContactSkillRepository _linkRepository;
        private readonly IMapper _mapper;

        public CreateLinkContactSkillCommandHandler(ILinkContactSkillRepository linkRepository, IMapper mapper)
        {
            _linkRepository = linkRepository;
            _mapper = mapper;
        }
        public async Task<LinkContactSkillV1> Handle(CreateLinkContactSkillRequestV1 request, CancellationToken cancellationToken)
        {
            var linkAdded = await _linkRepository.Add(_mapper.Map<LinkContactSkill>(request));
            return _mapper.Map<LinkContactSkillV1>(linkAdded);
        }
    }
}
