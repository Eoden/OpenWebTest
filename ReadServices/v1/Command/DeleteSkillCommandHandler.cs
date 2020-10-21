using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OpenWebApiContract.Classes;
using OpenWebApiContract.Communication.Skill;
using OpenWebData.Repository.v1;
namespace OpenWebServices.v1.Command
{
    public class DeleteSkillCommandHandler : IRequestHandler<DeleteSkillRequestV1, SkillV1>
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;

        public DeleteSkillCommandHandler(ISkillRepository skillRepository, IMapper mapper)
        {
            _skillRepository = skillRepository;
            _mapper = mapper;
        }
        async Task<SkillV1> IRequestHandler<DeleteSkillRequestV1, SkillV1>.Handle(DeleteSkillRequestV1 request, CancellationToken cancellationToken)
        {
            var skillDeleted = await _skillRepository.DeleteById(request.Id, cancellationToken);
            return _mapper.Map<SkillV1>(skillDeleted);
        }
    }
}
