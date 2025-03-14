using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RagnarokGasolin.Models
{
    /// <summary>
    /// Represents a member of the Gasolin band
    /// </summary>
    public class BandMember
    {
        /// <summary>
        /// Unique identifier for the band member
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Full name of the band member
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Primary role in the band (e.g., "Vocalist", "Guitarist")
        /// </summary>
        [Required]
        public string Role { get; set; }

        /// <summary>
        /// Date of birth
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Date of death (if applicable)
        /// </summary>
        public DateTime? DeathDate { get; set; }

        /// <summary>
        /// URL to the band member's image
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Biographical information about the band member
        /// </summary>
        public string Biography { get; set; }

        /// <summary>
        /// Description of the member's contributions to the band
        /// </summary>
        public string Contributions { get; set; }

        /// <summary>
        /// Years active with the band (e.g., "1969-1978")
        /// </summary>
        public string ActiveYears { get; set; }

        /// <summary>
        /// List of instruments played by the band member
        /// </summary>
        public List<string> Instruments { get; set; } = new List<string>();

        /// <summary>
        /// Formats the birth date as a string
        /// </summary>
        public string GetFormattedBirthDate()
        {
            return BirthDate.ToString("d. MMMM yyyy");
        }

        /// <summary>
        /// Formats the death date as a string, if applicable
        /// </summary>
        public string GetFormattedDeathDate()
        {
            return DeathDate.HasValue ? DeathDate.Value.ToString("d. MMMM yyyy") : "Lever stadig";
        }

        /// <summary>
        /// Validates that the active years are within the band's existence (1969-1978)
        /// </summary>
        /// <returns>True if the active years are valid, false otherwise</returns>
        public bool ValidateActiveYears()
        {
            // Gasolin was active from 1969 to 1978
            const int bandStartYear = 1969;
            const int bandEndYear = 1978;

            // Parse the active years (e.g., "1969-1978")
            var parts = ActiveYears.Split('-');
            if (parts.Length != 2)
            {
                return false;
            }

            if (!int.TryParse(parts[0], out int startYear) || !int.TryParse(parts[1], out int endYear))
            {
                return false;
            }

            // Check that the years are within the band's existence
            return startYear >= bandStartYear && endYear <= bandEndYear && startYear <= endYear;
        }
    }
}