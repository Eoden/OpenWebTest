using AutoMapper;
using OpenWebApiContract.Classes;
using OpenWebData.Data.Contact;
using OpenWebData.Data.Skill;
using OpenWebData.Data.LinkContactSkill;
using OpenWebApiContract.Communication.Contact;
using OpenWebApiContract.Communication.Skill;
using OpenWebApiContract.Communication.LinkContactSkill;

namespace RestServices.Mapping
{
    public class MappingInfos : Profile
    {
        public MappingInfos()
        {
            CreateMap<ContactV1, Contact>();
            CreateMap<Contact, ContactV1>().ForMember(d => d.Fullname, d => d.MapFrom(x => string.Format("{0} {1}", x.Lastname, x.Firstname))); ;
            CreateMap<CreateContactRequestV1, Contact>();
            CreateMap<UpdateContactRequestV1, Contact>();
            CreateMap<SkillV1, Skill>();
            CreateMap<Skill, SkillV1>();
            CreateMap<CreateSkillRequestV1, Skill>();
            CreateMap<UpdateSkillRequestV1, Skill>();
            CreateMap<LinkContactSkillV1, LinkContactSkill>();
            CreateMap<LinkContactSkill, LinkContactSkillV1>();
            CreateMap<CreateLinkContactSkillRequestV1, LinkContactSkill>();
        }
    }
}
