namespace SwClub.Common.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum ActionEvent
    {
        /// <summary>
        /// Start event.
        /// </summary>
        [Display(Name = "START")]
        Start = 0,

        /// <summary>
        /// Stop event.
        /// </summary>
        [Display(Name = "STOP")]
        Stop,

        /// <summary>
        /// Create data.
        /// </summary>
        [Display(Name = "CREATE")]
        Create,

        /// <summary>
        /// Read data
        /// </summary>
        [Display(Name = "READ")]
        Read,

        /// <summary>
        /// Update data.
        /// </summary>
        [Display(Name = "UPDATE")]
        Update,

        /// <summary>
        /// Delete data.
        /// </summary>
        [Display(Name = "DELETE")]
        Delete,
    }
}
