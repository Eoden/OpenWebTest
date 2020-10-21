using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OpenWebApiContract.Classes;
using OpenWebApiContract.Communication.Skill;
using OpenWebData.Data.Skill;
using OpenWebData.Repository.v1;

namespace OpenWebServices.v1.Command
{
    public class CreateSkillCommandHandler : IRequestHandler<CreateSkillRequestV1, SkillV1>
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;

        public CreateSkillCommandHandler(ISkillRepository skillRepository, IMapper mapper)
        {
            _skillRepository = skillRepository;
            _mapper = mapper;
        }
        public async Task<SkillV1> Handle(CreateSkillRequestV1 request, CancellationToken cancellationToken)
        {
            var skillAdded = await _skillRepository.Add(_mapper.Map<Skill>(request));
            return _mapper.Map<SkillV1>(skillAdded);
        }
    }
}
