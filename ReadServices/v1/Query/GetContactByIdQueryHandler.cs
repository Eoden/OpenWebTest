using System.Collections.Generic;
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
        private readonly ISkillRepository _skillRepository;
        private readonly ILinkContactSkillRepository _linkRepository;
        private readonly IMapper _mapper;

        public GetContactByIdQueryHandler(IContactRepository contactRepository, ISkillRepository skillRepository, ILinkContactSkillRepository linkRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _linkRepository = linkRepository;
            _skillRepository = skillRepository;
            _mapper = mapper;
        }


        public async Task<ContactV1> Handle(GetContactRequestV1 request, CancellationToken cancellationToken)
        {
            var contact = _mapper.Map<ContactV1>(await _contactRepository.GetContactById(request.Id, cancellationToken));
            contact.Skills = new List<SkillV1>();

            var listLinkContactSkill = await _linkRepository.GetAllLinkByContactId(contact.Id, cancellationToken);
            foreach (var linkContactSkill in listLinkContactSkill)
            {
                var skill = await _skillRepository.GetSkillById(linkContactSkill.IdSkill, cancellationToken);
                contact.Skills.Add(_mapper.Map<SkillV1>(skill));
            }
            return contact;
        }
    }
}
