using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RagnarokGasolin.Models
{
    /// <summary>
    /// Represents a significant event in the timeline of Gasolin
    /// </summary>
    public class TimelineEvent
    {
        /// <summary>
        /// Unique identifier for the timeline event
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Title of the event
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Date when the event occurred
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// Description of the event
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// URL to an image representing the event
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Type of event (e.g., "Formation", "AlbumRelease", "MemberChange", "Dissolution")
        /// </summary>
        [Required(ErrorMessage = "Event type is required")]
        public string Type { get; set; }

        /// <summary>
        /// A list of media items related to the event
        /// </summary>
        public List<MediaItem> RelatedMedia { get; set; } = new List<MediaItem>();

        /// <summary>
        /// Additional information about the event
        /// </summary>
        public string AdditionalInfo { get; set; }

        /// <summary>
        /// The location of the event (if applicable)
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Validates that the event date is within the band's existence (1969-1978)
        /// </summary>
        /// <returns>True if the date is valid, false otherwise</returns>
        public bool ValidateDate()
        {
            // Gasolin was active from 1969 to 1978
            var bandStartDate = new DateTime(1969, 1, 1);
            var bandEndDate = new DateTime(1978, 12, 31);

            return Date >= bandStartDate && Date <= bandEndDate;
        }

        /// <summary>
        /// Gets a formatted date string
        /// </summary>
        public string GetFormattedDate()
        {
            return Date.ToString("d. MMMM yyyy");
        }

        /// <summary>
        /// Gets the year of the event
        /// </summary>
        public int GetYear()
        {
            return Date.Year;
        }
    }

    /// <summary>
    /// Represents the type of timeline event
    /// </summary>
    public enum EventType
    {
        Formation,
        AlbumRelease,
        Concert,
        MemberChange,
        Award,
        Dissolution,
        Other
    }
}