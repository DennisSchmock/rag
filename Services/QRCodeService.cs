using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace RagnarokGasolin.Services
{
    /// <summary>
    /// Service for generating QR codes for exhibit items
    /// </summary>
    public class QRCodeService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<QRCodeService> _logger;
        private readonly IDataService _dataService;

        /// <summary>
        /// Constructor for QRCodeService
        /// </summary>
        public QRCodeService(
            IWebHostEnvironment environment,
            ILogger<QRCodeService> logger,
            IDataService dataService)
        {
            _environment = environment;
            _logger = logger;
            _dataService = dataService;
        }

        /// <summary>
        /// Generates a QR code for an exhibit item
        /// </summary>
        public async Task<string> GenerateQRCodeAsync(int exhibitId, int pixelSize = 200)
        {
            try
            {
                // Get the exhibit item
                var exhibitItem = await _dataService.GetExhibitItemByIdAsync(exhibitId);
                if (exhibitItem == null)
                {
                    _logger.LogWarning("Exhibit item with ID {Id} not found", exhibitId);
                    return null;
                }

                // Create the QR code data
                string qrCodeContent = $"/QRCode/{exhibitId}";

                // Create the directory if it doesn't exist
                string qrCodeDirectory = Path.Combine(_environment.WebRootPath, "images", "qrcodes");
                if (!Directory.Exists(qrCodeDirectory))
                {
                    Directory.CreateDirectory(qrCodeDirectory);
                }

                // Save the QR code image
                string fileName = $"exhibit_{exhibitId}.png";
                string filePath = Path.Combine(qrCodeDirectory, fileName);

                // Generate a placeholder image for now
                using (Bitmap bitmap = new Bitmap(pixelSize, pixelSize))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.Clear(Color.White);
                        using (Font font = new Font("Arial", 10))
                        {
                            g.DrawString($"QR Code for Exhibit {exhibitId}", font, Brushes.Black, new PointF(10, pixelSize / 2));
                        }
                    }
                    bitmap.Save(filePath, ImageFormat.Png);
                }

                // Return the URL to the QR code image
                return $"/images/qrcodes/{fileName}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating QR code for exhibit ID {Id}", exhibitId);
                return null;
            }
        }

        /// <summary>
        /// Generates a QR code image for the specified URL
        /// </summary>
        public byte[] GenerateQRCodeForUrl(string url, int pixelSize = 20)
        {
            try
            {
                // Generate a placeholder image for now
                using (Bitmap bitmap = new Bitmap(pixelSize, pixelSize))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.Clear(Color.White);
                        using (Font font = new Font("Arial", 8))
                        {
                            g.DrawString("QR", font, Brushes.Black, new PointF(5, 5));
                        }
                    }
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bitmap.Save(ms, ImageFormat.Png);
                        return ms.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating QR code for URL {Url}", url);
                return null;
            }
        }
    }
}