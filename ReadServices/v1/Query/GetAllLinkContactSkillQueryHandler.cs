using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OpenWebApiContract.Classes;
using OpenWebApiContract.Communication.LinkContactSkill;
using OpenWebData.Repository.v1;


namespace OpenWebServices.v1.Query
{
    public class GetAllLinkContactSkillQueryHandler : IRequestHandler<GetAllLinkContactSkillRequestV1, List<LinkContactSkillV1>>
    {
        private readonly ILinkContactSkillRepository _linkRepository;
        private readonly IMapper _mapper;

        public GetAllLinkContactSkillQueryHandler(ILinkContactSkillRepository linkRepository, IMapper mapper)
        {
            _linkRepository = linkRepository;
            _mapper = mapper;
        }

        public async Task<List<LinkContactSkillV1>> Handle(GetAllLinkContactSkillRequestV1 request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<LinkContactSkillV1>>(await _linkRepository.GetAll());
        }
    }
}
