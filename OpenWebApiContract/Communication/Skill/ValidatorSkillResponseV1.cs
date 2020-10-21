using System.Runtime.Serialization;
using OpenWebApiContract.Classes;
namespace OpenWebApiContract.Communication.Skill
{
    public class ValidatorSkillResponseV1
    {
        #region Identifier
        /// <summary>
        /// Id of the Skill
        /// </summary>
        [DataMember(Name = "id", IsRequired = false)]
        public int Id { get; set; }
        #endregion

        #region body
        /// <summary>
        /// Skill body
        /// </summary>
        [DataMember(Name = "skill", IsRequired = false)]
        public SkillV1 Skill { get; set; }
        #endregion

    }
}
