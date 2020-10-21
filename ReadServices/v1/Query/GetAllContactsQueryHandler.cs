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
    public class GetAllContactsQueryHandler : IRequestHandler<GetAllContactRequestV1, List<ContactV1>>
    {
        private readonly IContactRepository _contactRepository;
        private readonly ILinkContactSkillRepository _linkRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;

        public GetAllContactsQueryHandler(IContactRepository contactRepository, ILinkContactSkillRepository linkRepository, ISkillRepository skillRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _linkRepository = linkRepository;
            _skillRepository = skillRepository;
            _mapper = mapper;
        }

        public async Task<List<ContactV1>> Handle(GetAllContactRequestV1 request, CancellationToken cancellationToken)
        {
            //get the contact and all the skills linked to it
            var listContact = _mapper.Map<List<ContactV1>>(await _contactRepository.GetAll());
            foreach(var contact in listContact)
            {
                contact.Skills = new List<SkillV1>();
                var listLinkContactSkill = await _linkRepository.GetAllLinkByContactId(contact.Id, cancellationToken);
                foreach (var linkContactSkill in listLinkContactSkill)
                {
                    var skill = await _skillRepository.GetSkillById(linkContactSkill.IdSkill,cancellationToken);
                    contact.Skills.Add(_mapper.Map<SkillV1>(skill));
                }
            }
            return listContact;
        }
    }
}
