using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MediatR;
using OpenWebApiContract.Classes;

namespace OpenWebApiContract.Communication.Contact
{
    [DataContract(Name = "Contact", Namespace = Constants.Namespace)]
    public class CreateContactRequestV1 : IRequest<ContactV1>
    {
        #region Information
        /// <summary>
        /// Firstname of the Contact
        /// </summary>
        /// <example>Arthur</example>
        [Required]
        [DataMember(Name = "firstname", IsRequired = true)]
        public string Firstname { get; set; }

        /// <summary>
        /// Lastname of the Contact
        /// </summary>
        /// <example>Picaud</example>
        [Required]
        [DataMember(Name = "lastname", IsRequired = true)]
        public string Lastname { get; set; }

        /// <summary>
        /// Physic address of the Contact
        /// </summary>
        /// <example>154 rue de l'esperance</example>
        [DataMember(Name = "address", IsRequired = false)]
        public string Address { get; set; }

        /// <summary>
        /// Email of the Contact
        /// </summary>
        /// <example>arthur.picaud@supinfo.com</example>
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
        [DataMember(Name = "email", IsRequired = false)]
        public string Email { get; set; }

        /// <summary>
        /// Mobile phone number of the Contact
        /// </summary>
        /// <example>+33 7 89 73 67 21</example>
        [DataMember(Name = "phone", IsRequired = false)]
        public string MobilePhoneNumber { get; set; }

        #endregion
    }
}
