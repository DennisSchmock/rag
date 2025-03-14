using System;
using System.ComponentModel.DataAnnotations;

namespace RagnarokGasolin.Models
{
    /// <summary>
    /// Represents a media item related to Gasolin (audio, video, image)
    /// </summary>
    public class MediaItem
    {
        /// <summary>
        /// Unique identifier for the media item
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Title of the media item
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Description of the media item
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Type of media (e.g., "Audio", "Video", "Image")
        /// </summary>
        [Required]
        public string Type { get; set; }

        /// <summary>
        /// URL to the media file
        /// </summary>
        [Required]
        public string Url { get; set; }

        /// <summary>
        /// URL to a thumbnail image for the media item
        /// </summary>
        public string ThumbnailUrl { get; set; }

        /// <summary>
        /// Date when the media was recorded or created
        /// </summary>
        public DateTime RecordedDate { get; set; }

        /// <summary>
        /// Duration of the media (for audio/video)
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        /// Source of the media item
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Additional metadata about the media item
        /// </summary>
        public string Metadata { get; set; }

        /// <summary>
        /// Gets a formatted date string
        /// </summary>
        public string GetFormattedDate()
        {
            return RecordedDate.ToString("d. MMMM yyyy");
        }

        /// <summary>
        /// Gets the year of the media item
        /// </summary>
        public int GetYear()
        {
            return RecordedDate.Year;
        }

        /// <summary>
        /// Gets the formatted duration of the media (e.g., "3:45")
        /// </summary>
        public string FormattedDuration
        {
            get
            {
                if (string.IsNullOrEmpty(Duration))
                {
                    return string.Empty;
                }

                var parts = Duration.Split(':');
                if (parts.Length == 2 && int.TryParse(parts[0], out int minutes) && int.TryParse(parts[1], out int seconds))
                {
                    return $"{minutes}:{seconds:D2}";
                }
                return Duration;
            }
        }

        /// <summary>
        /// Gets the appropriate HTML element for displaying the media based on its type
        /// </summary>
        /// <returns>HTML markup for displaying the media</returns>
        public string GetHtmlElement()
        {
            switch (Type)
            {
                case "Video":
                    return $@"<video controls poster=""{ThumbnailUrl}"">
                        <source src=""{Url}"" type=""video/mp4"">
                        Your browser does not support the video tag.
                    </video>";
                case "Audio":
                    return $@"<audio controls>
                        <source src=""{Url}"" type=""audio/mpeg"">
                        Your browser does not support the audio tag.
                    </audio>";
                case "Image":
                    return $@"<img src=""{Url}"" alt=""{Title}"" class=""media-image"" />";
                default:
                    return $@"<a href=""{Url}"" target=""_blank"">View Media</a>";
            }
        }
    }

    /// <summary>
    /// Represents the type of media
    /// </summary>
    public enum MediaType
    {
        Video,
        Audio,
        Image,
        Document,
        Other
    }
}