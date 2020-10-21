using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace OpenWebApiContract.Classes
{
    [DataContract(Name = "LinkContactSkill", Namespace = Constants.Namespace)]
    public class LinkContactSkillV1
    {
        #region Identifier
        /// <summary>
        /// Id of the Link
        /// </summary>
        [DataMember(Name = "id", IsRequired = false)]
        public int Id { get; set; }

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
