namespace OpenWebData.Data.Contact
{
    public class Contact
    {
        #region Identifier
        /// <summary>
        /// Id of the Contact
        /// </summary>
        public int Id { get; set; }
        #endregion

        #region Information
        /// <summary>
        /// Firstname of the Contact
        /// </summary>
        public string Firstname { get; set; }

        /// <summary>
        /// Lastname of the Contact
        /// </summary>
        public string Lastname { get; set; }

        /// <summary>
        /// Address of the Contact
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Email of the Contact
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Mobile phone number of the Contact
        /// </summary>
        public string MobilePhoneNumber { get; set; }
        #endregion
    }
}
