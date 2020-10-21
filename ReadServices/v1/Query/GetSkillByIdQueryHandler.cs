using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OpenWebApiContract.Classes;
using OpenWebApiContract.Communication.Skill;
using OpenWebData.Repository.v1;

namespace OpenWebServices.v1.Query
{
    public class GetSkillByIdQueryHandler : IRequestHandler<GetSkillRequestV1, SkillV1>
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;

        public GetSkillByIdQueryHandler(ISkillRepository skillRepository, IMapper mapper)
        {
            _skillRepository = skillRepository;
            _mapper = mapper;
        }


        public async Task<SkillV1> Handle(GetSkillRequestV1 request, CancellationToken cancellationToken)
        {
            return _mapper.Map<SkillV1>(await _skillRepository.GetSkillById(request.Id, cancellationToken));
        }
    }
}
