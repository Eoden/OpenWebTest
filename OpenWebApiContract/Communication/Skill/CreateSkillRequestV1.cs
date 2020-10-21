using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MediatR;
using OpenWebApiContract.Classes;

namespace OpenWebApiContract.Communication.Skill
{
    [DataContract(Name = "Skill", Namespace = Constants.Namespace)]
    public class CreateSkillRequestV1 : IRequest<SkillV1>
    {
        #region Information
        /// <summary>
        /// Name of the Skill
        /// </summary>
        [DataMember(Name = "name", IsRequired = true)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Level of Skill from 0 to 10
        /// </summary>
        [Range(0, 10)]
        [DataMember(Name = "level", IsRequired = true)]
        [Required]
        public int Level { get; set; }
        #endregion
    }
}