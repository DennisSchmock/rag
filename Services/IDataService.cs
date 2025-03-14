using System.Collections.Generic;
using System.Threading.Tasks;
using RagnarokGasolin.Models;

namespace RagnarokGasolin.Services
{
    /// <summary>
    /// Interface for accessing data about Gasolin band members, timeline events, and media items
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// Gets all band members
        /// </summary>
        Task<List<BandMember>> GetBandMembersAsync();

        /// <summary>
        /// Gets a band member by ID
        /// </summary>
        Task<BandMember> GetBandMemberByIdAsync(int id);

        /// <summary>
        /// Gets all timeline events
        /// </summary>
        Task<List<TimelineEvent>> GetTimelineEventsAsync();

        /// <summary>
        /// Gets all media items
        /// </summary>
        Task<List<MediaItem>> GetMediaItemsAsync();

        /// <summary>
        /// Gets a media item by ID
        /// </summary>
        Task<MediaItem> GetMediaItemByIdAsync(int id);

        /// <summary>
        /// Gets all exhibit items
        /// </summary>
        Task<List<ExhibitItem>> GetExhibitItemsAsync();

        /// <summary>
        /// Gets an exhibit item by ID
        /// </summary>
        Task<ExhibitItem> GetExhibitItemByIdAsync(int id);
    }
}