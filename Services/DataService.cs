using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RagnarokGasolin.Models;

namespace RagnarokGasolin.Services
{
    /// <summary>
    /// Service for accessing data about Gasolin band members, timeline events, and media items
    /// </summary>
    public class DataService : IDataService
    {
        private readonly ILogger<DataService> _logger;
        private readonly List<BandMember> _bandMembers;
        private readonly List<TimelineEvent> _timelineEvents;
        private readonly List<MediaItem> _mediaItems;
        private readonly List<ExhibitItem> _exhibitItems;

        public DataService(ILogger<DataService> logger)
        {
            _logger = logger;

            // Initialize band members
            _bandMembers = new List<BandMember>
            {
                new BandMember
                {
                    Id = 1,
                    Name = "Kim Larsen",
                    Role = "Vokal, guitar",
                    BirthDate = new DateTime(1945, 10, 23),
                    DeathDate = new DateTime(2018, 9, 30),
                    ImageUrl = "/images/band-members/kim-larsen.jpg",
                    Biography = "Kim Melius Flyvholm Larsen var en dansk sanger, guitarist og sangskriver. Han var medlem af Gasolin' fra 1969 til 1978. Efter Gasolin' fortsatte han en succesfuld solokarriere.",
                    Contributions = "Kim Larsen var en af de primære sangskrivere i Gasolin' og bidrog med sin karakteristiske vokal til mange af bandets største hits.",
                    ActiveYears = "1969-1978",
                    Instruments = new List<string> { "Vokal", "Guitar", "Mundharmonika" }
                },
                new BandMember
                {
                    Id = 2,
                    Name = "Franz Beckerlee",
                    Role = "Lead guitar",
                    BirthDate = new DateTime(1942, 7, 3),
                    ImageUrl = "/images/band-members/franz-beckerlee.jpg",
                    Biography = "Franz Beckerlee er en dansk guitarist og kunstner. Han var med til at grundlægge Gasolin' i 1969 og var bandets lead guitarist indtil opløsningen i 1978.",
                    Contributions = "Franz Beckerlee var kendt for sin energiske guitarspil og var med til at skabe Gasolins karakteristiske rocklyd.",
                    ActiveYears = "1969-1978",
                    Instruments = new List<string> { "Guitar", "Mundharmonika", "Keyboard" }
                },
                new BandMember
                {
                    Id = 3,
                    Name = "Wili Jønsson",
                    Role = "Bas, vokal",
                    BirthDate = new DateTime(1942, 10, 15),
                    ImageUrl = "/images/band-members/wili-jonsson.jpg",
                    Biography = "Wili Jønsson er en dansk bassist og sanger. Han var medlem af Gasolin' fra 1969 til 1978 og har senere spillet i flere andre bands.",
                    Contributions = "Wili Jønsson var Gasolins bassist og bidrog med baggrundsvokal. Han var også involveret i sangskrivningen på flere af bandets numre.",
                    ActiveYears = "1969-1978",
                    Instruments = new List<string> { "Bas", "Vokal", "Keyboard" }
                },
                new BandMember
                {
                    Id = 4,
                    Name = "Søren Berlev",
                    Role = "Trommer",
                    BirthDate = new DateTime(1946, 4, 12),
                    ImageUrl = "/images/band-members/soren-berlev.jpg",
                    Biography = "Søren Berlev er en dansk trommeslager. Han blev medlem af Gasolin' i 1970 og var med indtil bandets opløsning i 1978.",
                    Contributions = "Søren Berlev var Gasolins trommeslager og kendt for sin solide og kraftfulde spillestil, der var med til at definere bandets sound.",
                    ActiveYears = "1970-1978",
                    Instruments = new List<string> { "Trommer", "Percussion" }
                }
            };

            // Initialize timeline events
            _timelineEvents = new List<TimelineEvent>
            {
                new TimelineEvent
                {
                    Id = 1,
                    Title = "Gasolin' dannes",
                    Date = new DateTime(1969, 1, 1),
                    Description = "Kim Larsen, Franz Beckerlee og Wili Jønsson danner Gasolin'.",
                    ImageUrl = "/images/timeline/gasolin-formation.jpg",
                    Type = "Formation"
                },
                new TimelineEvent
                {
                    Id = 2,
                    Title = "Søren Berlev bliver medlem",
                    Date = new DateTime(1970, 1, 1),
                    Description = "Søren Berlev slutter sig til bandet som trommeslager.",
                    ImageUrl = "/images/timeline/soren-joins.jpg",
                    Type = "MemberChange"
                },
                new TimelineEvent
                {
                    Id = 3,
                    Title = "Første album: 'Gasolin''",
                    Date = new DateTime(1971, 10, 1),
                    Description = "Bandet udgiver deres debutalbum 'Gasolin''.",
                    ImageUrl = "/images/timeline/first-album.jpg",
                    Type = "AlbumRelease"
                },
                new TimelineEvent
                {
                    Id = 4,
                    Title = "Gasolin' 2",
                    Date = new DateTime(1972, 10, 1),
                    Description = "Udgivelse af bandets andet album 'Gasolin' 2'.",
                    ImageUrl = "/images/timeline/gasolin-2.jpg",
                    Type = "AlbumRelease"
                },
                new TimelineEvent
                {
                    Id = 5,
                    Title = "Gasolin' opløses",
                    Date = new DateTime(1978, 8, 28),
                    Description = "Efter ni år sammen beslutter bandet at gå hver til sit.",
                    ImageUrl = "/images/timeline/gasolin-breakup.jpg",
                    Type = "Dissolution"
                }
            };

            // Initialize media items
            _mediaItems = new List<MediaItem>
            {
                new MediaItem
                {
                    Id = 1,
                    Title = "Kvinde Min",
                    Description = "En af Gasolin's mest populære sange, udgivet på albummet 'Gas 5' i 1975.",
                    Type = "Audio",
                    Url = "/media/audio/kvinde-min.mp3",
                    ThumbnailUrl = "/images/media/kvinde-min-thumb.jpg",
                    RecordedDate = new DateTime(1975, 1, 1),
                    Duration = "3:25",
                    Source = "Album: Gas 5"
                },
                new MediaItem
                {
                    Id = 2,
                    Title = "Gasolin' Live på Roskilde Festival 1976",
                    Description = "Optagelse af Gasolin's optræden på Roskilde Festival i 1976.",
                    Type = "Video",
                    Url = "/media/video/gasolin-roskilde-1976.mp4",
                    ThumbnailUrl = "/images/media/roskilde-1976-thumb.jpg",
                    RecordedDate = new DateTime(1976, 7, 2),
                    Duration = "15:30",
                    Source = "Roskilde Festival Archives"
                },
                new MediaItem
                {
                    Id = 3,
                    Title = "Sjældent pressefoto fra 1972",
                    Description = "Et sjældent pressefoto af bandet taget i forbindelse med udgivelsen af 'Gasolin' 2'.",
                    Type = "Image",
                    Url = "/images/media/rare-press-photo-1972.jpg",
                    ThumbnailUrl = "/images/media/rare-press-photo-1972-thumb.jpg",
                    RecordedDate = new DateTime(1972, 10, 15),
                    Source = "Politikens Arkiv"
                },
                new MediaItem
                {
                    Id = 4,
                    Title = "Interview med Kim Larsen om Gasolin'",
                    Description = "Et interview fra 1990'erne, hvor Kim Larsen fortæller om tiden i Gasolin'.",
                    Type = "Video",
                    Url = "/media/video/kim-larsen-interview.mp4",
                    ThumbnailUrl = "/images/media/kim-interview-thumb.jpg",
                    RecordedDate = new DateTime(1995, 5, 10),
                    Duration = "8:45",
                    Source = "DR Arkiv"
                },
                new MediaItem
                {
                    Id = 5,
                    Title = "Rabalderstræde",
                    Description = "En af bandets mest elskede sange, udgivet på albummet 'Gasolin' 3' i 1973.",
                    Type = "Audio",
                    Url = "/media/audio/rabalderstraede.mp3",
                    ThumbnailUrl = "/images/media/rabalderstraede-thumb.jpg",
                    RecordedDate = new DateTime(1973, 1, 1),
                    Duration = "4:10",
                    Source = "Album: Gasolin' 3"
                }
            };

            // Initialize exhibit items
            _exhibitItems = new List<ExhibitItem>
            {
                new ExhibitItem
                {
                    Id = 1,
                    Name = "Kim Larsens guitar",
                    Description = "Den akustiske guitar, som Kim Larsen brugte under indspilningen af 'Gasolin' 3'.",
                    ImageUrl = "/images/exhibition/kim-guitar.jpg",
                    Type = "Instrument",
                    Location = "Øvelokale-kulisse, væggen",
                    QRCodeUrl = "/qrcode/exhibit/1",
                    AcquisitionDate = new DateTime(2010, 5, 15),
                    OriginalOwner = "Kim Larsens familie",
                    HistoricalContext = "Denne guitar blev brugt til at komponere flere af Gasolins mest kendte sange, herunder 'Rabalderstræde'."
                },
                new ExhibitItem
                {
                    Id = 2,
                    Name = "Original koncertplakat fra 1976",
                    Description = "En sjælden plakat, der annoncerer Gasolins koncert i KB Hallen den 15. november 1976.",
                    ImageUrl = "/images/exhibition/concert-poster-1976.jpg",
                    Type = "Memorabilia",
                    Location = "Øvelokale-kulisse, opslagstavle",
                    QRCodeUrl = "/qrcode/exhibit/2",
                    AcquisitionDate = new DateTime(2015, 3, 10),
                    OriginalOwner = "Privat samler",
                    HistoricalContext = "Koncerten i KB Hallen var en af bandets største i Danmark og blev udsolgt på rekordtid."
                },
                new ExhibitItem
                {
                    Id = 3,
                    Name = "Håndskrevne tekster til 'Kvinde Min'",
                    Description = "Kim Larsens originale håndskrevne tekster til hittet 'Kvinde Min' med rettelser og noter.",
                    ImageUrl = "/images/exhibition/kvinde-min-lyrics.jpg",
                    Type = "Document",
                    Location = "Øvelokale-kulisse, skrivebord",
                    QRCodeUrl = "/qrcode/exhibit/3",
                    AcquisitionDate = new DateTime(2012, 9, 20),
                    OriginalOwner = "Privat samling",
                    HistoricalContext = "Disse tekster viser den kreative proces bag en af Gasolins mest elskede sange."
                },
                new ExhibitItem
                {
                    Id = 4,
                    Name = "Franz Beckerlees elguitar",
                    Description = "Den ikoniske Gibson Les Paul, som Franz Beckerlee spillede på under Gasolins sidste koncert.",
                    ImageUrl = "/images/exhibition/franz-guitar.jpg",
                    Type = "Instrument",
                    Location = "Øvelokale-kulisse, guitarstativ",
                    QRCodeUrl = "/qrcode/exhibit/4",
                    AcquisitionDate = new DateTime(2018, 11, 5),
                    OriginalOwner = "Franz Beckerlee",
                    HistoricalContext = "Denne guitar var med til at skabe Gasolins karakteristiske rocklyd og blev brugt på alle bandets albums."
                },
                new ExhibitItem
                {
                    Id = 5,
                    Name = "Gasolin' Gold Record Award",
                    Description = "Guldplade for albummet 'Gas 5', der solgte over 50.000 eksemplarer i Danmark.",
                    ImageUrl = "/images/exhibition/gold-record.jpg",
                    Type = "Award",
                    Location = "Øvelokale-kulisse, hylde",
                    QRCodeUrl = "/qrcode/exhibit/5",
                    AcquisitionDate = new DateTime(2014, 2, 28),
                    OriginalOwner = "CBS Records",
                    HistoricalContext = "Gas 5 var et af bandets mest succesfulde albums og indeholdt hits som 'Kvinde Min' og 'Masser af Succes'."
                }
            };
        }

        /// <summary>
        /// Gets all band members
        /// </summary>
        public async Task<List<BandMember>> GetBandMembersAsync()
        {
            _logger.LogInformation("Retrieving band members at {Time}", DateTime.Now);
            return await Task.FromResult(_bandMembers);
        }

        /// <summary>
        /// Gets a band member by ID
        /// </summary>
        public async Task<BandMember> GetBandMemberByIdAsync(int id)
        {
            _logger.LogInformation("Retrieving band member with ID {Id} at {Time}", id, DateTime.Now);
            return await Task.FromResult(_bandMembers.FirstOrDefault(m => m.Id == id));
        }

        /// <summary>
        /// Gets all timeline events, ordered by date
        /// </summary>
        public async Task<List<TimelineEvent>> GetTimelineEventsAsync()
        {
            _logger.LogInformation("Retrieving timeline events at {Time}", DateTime.Now);
            return await Task.FromResult(_timelineEvents);
        }

        /// <summary>
        /// Gets all media items
        /// </summary>
        public async Task<List<MediaItem>> GetMediaItemsAsync()
        {
            _logger.LogInformation("Retrieving media items at {Time}", DateTime.Now);
            return await Task.FromResult(_mediaItems);
        }

        /// <summary>
        /// Gets a media item by ID
        /// </summary>
        public async Task<MediaItem> GetMediaItemByIdAsync(int id)
        {
            _logger.LogInformation("Retrieving media item with ID {Id} at {Time}", id, DateTime.Now);
            return await Task.FromResult(_mediaItems.FirstOrDefault(m => m.Id == id));
        }

        /// <summary>
        /// Gets all exhibit items
        /// </summary>
        public async Task<List<ExhibitItem>> GetExhibitItemsAsync()
        {
            _logger.LogInformation("Retrieving exhibit items at {Time}", DateTime.Now);
            return await Task.FromResult(_exhibitItems);
        }

        /// <summary>
        /// Gets an exhibit item by ID
        /// </summary>
        public async Task<ExhibitItem> GetExhibitItemByIdAsync(int id)
        {
            _logger.LogInformation("Retrieving exhibit item with ID {Id} at {Time}", id, DateTime.Now);
            return await Task.FromResult(_exhibitItems.FirstOrDefault(e => e.Id == id));
        }
    }
}