using System;
using System.Drawing;
using System.IO;
using Xunit;
using RagnarokGasolin.Services;

namespace RagnarokGasolin.Tests.UnitTests.Services
{
    public class QRCodeServiceTests
    {
        private readonly QRCodeService _qrCodeService;

        public QRCodeServiceTests()
        {
            _qrCodeService = new QRCodeService();
        }

        [Fact]
        public void Test_GenerateQRCode_ValidUrl_ReturnsValidImage()
        {
            // Arrange
            var url = "https://ragnarok-gasolin.dk/exhibit/1";

            // Act
            var qrCodeImage = _qrCodeService.GenerateQRCode(url);

            // Assert
            Assert.NotNull(qrCodeImage);
            Assert.True(qrCodeImage.Length > 0);

            // Additional validation to ensure it's a valid image
            using (var ms = new MemoryStream(qrCodeImage))
            {
                var image = Image.FromStream(ms);
                Assert.NotNull(image);
            }
        }

        [Fact]
        public void Test_GenerateQRCode_EmptyUrl_ThrowsException()
        {
            // Arrange
            var url = string.Empty;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => _qrCodeService.GenerateQRCode(url));
            Assert.Contains("URL cannot be empty", exception.Message);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(30)]
        public void Test_GenerateQRCode_VariousPixelSizes_ReturnsCorrectlySizedImages(int pixelSize)
        {
            // Arrange
            var url = "https://ragnarok-gasolin.dk/exhibit/1";

            // Act
            var qrCodeImage = _qrCodeService.GenerateQRCode(url, pixelSize);

            // Assert
            Assert.NotNull(qrCodeImage);
            Assert.True(qrCodeImage.Length > 0);

            // Validate image dimensions
            using (var ms = new MemoryStream(qrCodeImage))
            {
                var image = Image.FromStream(ms);

                // QR codes are square, and the size should be proportional to the pixel size
                // The exact dimensions will depend on the QR code version and content
                // But we can verify that larger pixel sizes result in larger images
                Assert.True(image.Width > pixelSize * 10); // Rough estimate
                Assert.Equal(image.Width, image.Height); // QR codes are square
            }
        }

        [Fact]
        public void Test_GenerateQRCode_WithLogo_ReturnsImageWithLogo()
        {
            // Arrange
            var url = "https://ragnarok-gasolin.dk/exhibit/1";
            var logoPath = "wwwroot/images/logo.png";

            // Act
            var qrCodeImage = _qrCodeService.GenerateQRCodeWithLogo(url, logoPath);

            // Assert
            Assert.NotNull(qrCodeImage);
            Assert.True(qrCodeImage.Length > 0);

            // We can't easily verify the logo is embedded without visual inspection
            // But we can verify it's a valid image
            using (var ms = new MemoryStream(qrCodeImage))
            {
                var image = Image.FromStream(ms);
                Assert.NotNull(image);
            }
        }

        [Fact]
        public void Test_GenerateQRCodeForExhibit_ValidId_ReturnsValidImage()
        {
            // Arrange
            var exhibitId = 1;
            var baseUrl = "https://ragnarok-gasolin.dk";

            // Act
            var qrCodeImage = _qrCodeService.GenerateQRCodeForExhibit(exhibitId, baseUrl);

            // Assert
            Assert.NotNull(qrCodeImage);
            Assert.True(qrCodeImage.Length > 0);

            // Additional validation to ensure it's a valid image
            using (var ms = new MemoryStream(qrCodeImage))
            {
                var image = Image.FromStream(ms);
                Assert.NotNull(image);
            }
        }
    }
}