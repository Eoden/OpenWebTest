namespace OpenWebData.Data.Skill
{
    public class Skill
    {
        #region Identifier
        /// <summary>
        /// Id of the Skill
        /// </summary>
        public int Id { get; set; }
        #endregion

        #region Information
        /// <summary>
        /// Name of the Skill
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Level of Skill from 0 to 10
        /// </summary>
        public int Level { get; set; }
        #endregion
    }
}
