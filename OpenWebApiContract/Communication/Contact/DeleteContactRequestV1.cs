using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MediatR;
using OpenWebApiContract.Classes;

namespace OpenWebApiContract.Communication.Contact
{
    public class DeleteContactRequestV1 : IRequest<ContactV1>
    {
        #region Identifier
        /// <summary>
        /// Id of the Contact
        /// </summary>
        [Required]
        [DataMember(Name = "id", IsRequired = true)]
        public int Id { get; set; }
        #endregion
    }
}
