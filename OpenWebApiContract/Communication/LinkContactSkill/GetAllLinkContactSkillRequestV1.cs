using System.Collections.Generic;
using MediatR;
using OpenWebApiContract.Classes;

namespace OpenWebApiContract.Communication.LinkContactSkill
{
    public class GetAllLinkContactSkillRequestV1 : IRequest<List<LinkContactSkillV1>>
    {
        public GetAllLinkContactSkillRequestV1()
        {
        }
    }
}
