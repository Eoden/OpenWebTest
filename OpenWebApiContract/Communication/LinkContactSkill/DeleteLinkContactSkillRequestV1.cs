using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MediatR;
using OpenWebApiContract.Classes;
namespace OpenWebApiContract.Communication.LinkContactSkill
{
    public class DeleteLinkContactSkillRequestV1 : IRequest<LinkContactSkillV1>
    {
        #region Identifier
        /// <summary>
        /// Id of the Link
        /// </summary>
        [Required]
        [DataMember(Name = "id", IsRequired = true)]
        public int Id { get; set; }
        #endregion
    }
}
