using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OpenWebApiContract.Classes;
using OpenWebApiContract.Communication.Skill;
using OpenWebData.Repository.v1;

namespace OpenWebServices.v1.Query
{
    public class GetAllSkillQueryHandler : IRequestHandler<GetAllSkillRequestV1, List<SkillV1>>
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;

        public GetAllSkillQueryHandler(ISkillRepository skillRepository, IMapper mapper)
        {
            _skillRepository = skillRepository;
            _mapper = mapper;
        }

        public async Task<List<SkillV1>> Handle(GetAllSkillRequestV1 request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<SkillV1>>(await _skillRepository.GetAll());
        }
    }
}
