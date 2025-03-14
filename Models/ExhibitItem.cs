using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RagnarokGasolin.Models
{
    /// <summary>
    /// Represents a physical exhibit item in the Gasolin exhibition
    /// </summary>
    public class ExhibitItem
    {
        /// <summary>
        /// Unique identifier for the exhibit item
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the exhibit item
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [StringLength(200, ErrorMessage = "Name cannot be longer than 200 characters")]
        public string Name { get; set; }

        /// <summary>
        /// Description of the exhibit item
        /// </summary>
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        /// <summary>
        /// URL to an image of the exhibit item
        /// </summary>
        [Required(ErrorMessage = "Image URL is required")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// Type of exhibit item (e.g., "Instrument", "Memorabilia", "Document", "Award")
        /// </summary>
        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; }

        /// <summary>
        /// Physical location of the item in the exhibition
        /// </summary>
        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

        /// <summary>
        /// URL to the QR code for the exhibit item
        /// </summary>
        [Required(ErrorMessage = "QRCodeUrl is required")]
        public string QRCodeUrl { get; set; }

        /// <summary>
        /// Date when the item was acquired for the exhibition
        /// </summary>
        public DateTime AcquisitionDate { get; set; }

        /// <summary>
        /// Original owner of the item
        /// </summary>
        public string OriginalOwner { get; set; }

        /// <summary>
        /// Historical context or significance of the item
        /// </summary>
        public string HistoricalContext { get; set; }

        /// <summary>
        /// A list of media items related to the exhibit item
        /// </summary>
        public List<MediaItem> RelatedMedia { get; set; } = new List<MediaItem>();

        /// <summary>
        /// A list of band members associated with the exhibit item
        /// </summary>
        public List<int> RelatedBandMemberIds { get; set; } = new List<int>();

        /// <summary>
        /// Gets a formatted acquisition date string
        /// </summary>
        public string GetFormattedAcquisitionDate()
        {
            return AcquisitionDate.ToString("d. MMMM yyyy");
        }

        /// <summary>
        /// Gets the QR code URL for the exhibit item
        /// </summary>
        /// <param name="baseUrl">The base URL of the application</param>
        /// <returns>The URL to be encoded in the QR code</returns>
        public string GetQRCodeUrl(string baseUrl)
        {
            return $"{baseUrl.TrimEnd('/')}/exhibit/{Id}";
        }

        /// <summary>
        /// Validates that the year acquired is within a valid range
        /// </summary>
        /// <returns>True if the year is valid, false otherwise</returns>
        public bool ValidateYearAcquired()
        {
            // Gasolin was active from 1969 to 1978
            const int bandStartYear = 1969;
            const int bandEndYear = 1978;

            return AcquisitionDate.Year >= bandStartYear && AcquisitionDate.Year <= bandEndYear;
        }
    }

    /// <summary>
    /// Represents the category of an exhibit item
    /// </summary>
    public enum ExhibitCategory
    {
        Instrument,
        Memorabilia,
        Document,
        Clothing,
        Award,
        Other
    }
}