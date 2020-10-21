using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MediatR;
using OpenWebApiContract.Classes;

namespace OpenWebApiContract.Communication.LinkContactSkill
{
    [DataContract(Name = "LinkContactSkill", Namespace = Constants.Namespace)]
    public class CreateLinkContactSkillRequestV1 : IRequest<LinkContactSkillV1>
    {
        #region Identifier

        /// <summary>
        /// Id of the Contact
        /// </summary>
        [DataMember(Name = "idContact", IsRequired = true)]
        [Required]
        public int IdContact { get; set; }


        /// <summary>
        /// Id of the Skill
        /// </summary>
        [DataMember(Name = "idSkill", IsRequired = true)]
        [Required]
        public int IdSkill { get; set; }
        #endregion
    }
}
