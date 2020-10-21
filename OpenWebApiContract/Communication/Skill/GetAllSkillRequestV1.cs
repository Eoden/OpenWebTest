using System.Collections.Generic;
using MediatR;
using OpenWebApiContract.Classes;
namespace OpenWebApiContract.Communication.Skill
{
    public class GetAllSkillRequestV1 : IRequest<List<SkillV1>>
    {
    }
}
