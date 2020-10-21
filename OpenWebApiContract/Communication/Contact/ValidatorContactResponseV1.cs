using System.Runtime.Serialization;
using OpenWebApiContract.Classes;

namespace OpenWebApiContract.Communication.Contact
{
    public class ValidatorContactResponseV1
    {
        #region Identifier
        /// <summary>
        /// Id of the Contact
        /// </summary>
        [DataMember(Name = "id", IsRequired = false)]
        public int Id { get; set; }
        #endregion

        #region body
        /// <summary>
        /// Contact body
        /// </summary>
        [DataMember(Name = "contact", IsRequired = false)]
        public ContactV1 Contact { get; set; }
        #endregion

    }
}
