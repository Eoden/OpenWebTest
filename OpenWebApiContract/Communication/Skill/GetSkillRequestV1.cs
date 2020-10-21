using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MediatR;
using OpenWebApiContract.Classes;
namespace OpenWebApiContract.Communication.Skill
{
    public class GetSkillRequestV1 : IRequest<SkillV1>
    {

        #region Identifier
        /// <summary>
        /// Id of the Skill
        /// </summary>
        [Required]
        [DataMember(Name = "id", IsRequired = true)]
        public int Id { get; set; }
        #endregion
    }
}
