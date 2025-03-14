# Test Plan for Ragnarok Rockmuseum's Gasolin Exhibition Website

## Testing First Approach

Following the project requirement of "Testing First," this document outlines our comprehensive testing strategy before implementation begins. This approach ensures that we build a robust, reliable application that meets all requirements from the start.

## Test Project Structure

```
RagnarokGasolin.Tests/
├── UnitTests/
│   ├── Services/
│   │   ├── QRCodeServiceTests.cs
│   │   └── DataServiceTests.cs
│   ├── Models/
│   │   ├── BandMemberTests.cs
│   │   ├── TimelineEventTests.cs
│   │   └── ExhibitItemTests.cs
│   └── Utilities/
│       └── HelperTests.cs
├── IntegrationTests/
│   ├── PageTests/
│   │   ├── HomePageTests.cs
│   │   ├── BandMemberPageTests.cs
│   │   ├── TimelinePageTests.cs
│   │   └── QRCodePageTests.cs
│   └── ApiTests/
│       └── QRCodeApiTests.cs
└── E2ETests/
    ├── UserJourneyTests.cs
    └── ResponsiveDesignTests.cs
```

## Test Categories

### 1. Unit Tests

#### QR Code Service Tests

- **Test_GenerateQRCode_ValidUrl_ReturnsValidImage**
  - Verify that the QR code service generates a valid image for a given URL
- **Test_GenerateQRCode_EmptyUrl_ThrowsException**
  - Verify that the service throws an appropriate exception when given an empty URL
- **Test_GenerateQRCode_VariousPixelSizes_ReturnsCorrectlySizedImages**
  - Verify that the QR code is generated with the correct pixel size

#### Data Service Tests

- **Test_GetBandMembers_ReturnsAllMembers**
  - Verify that all band members are returned
- **Test_GetBandMemberById_ValidId_ReturnsMember**
  - Verify that a specific band member is returned when requested by ID
- **Test_GetTimelineEvents_ReturnsEventsInChronologicalOrder**
  - Verify that timeline events are returned in chronological order
- **Test_GetMediaItems_FiltersByType_ReturnsCorrectItems**
  - Verify that media items can be filtered by type (video, audio, etc.)

#### Model Tests

- **Test_BandMember_Validation_RequiredFields**
  - Verify that validation works for required fields
- **Test_TimelineEvent_DateRange_Validation**
  - Verify that dates fall within the band's active period (1969-1978)
- **Test_ExhibitItem_QRCodeGeneration_HasValidUrl**
  - Verify that exhibit items generate valid QR code URLs

### 2. Integration Tests

#### Page Tests

- **Test_HomePage_LoadsCorrectly_ContainsIntroduction**
  - Verify that the home page loads and contains the introduction to Gasolin
- **Test_BandMemberPage_LoadsCorrectly_DisplaysMemberInfo**
  - Verify that band member pages load and display the correct information
- **Test_TimelinePage_LoadsCorrectly_DisplaysEvents**
  - Verify that the timeline page loads and displays events in chronological order
- **Test_QRCodePage_LoadsCorrectly_DisplaysRelatedContent**
  - Verify that QR code landing pages load and display the correct related content

#### API Tests

- **Test_QRCodeApi_GeneratesValidQRCode**
  - Verify that the QR code API endpoint returns a valid QR code image
- **Test_QRCodeApi_HandlesInvalidRequests**
  - Verify that the API handles invalid requests gracefully

### 3. UI Tests

#### Responsive Design Tests

- **Test_HomePage_ResponsiveDesign_MobileTabletDesktop**
  - Verify that the home page displays correctly on different screen sizes
- **Test_TimelinePage_ResponsiveDesign_MobileTabletDesktop**
  - Verify that the timeline displays correctly on different screen sizes
- **Test_MediaPlayer_ResponsiveDesign_MobileTabletDesktop**
  - Verify that the media player is usable on different screen sizes

#### Accessibility Tests

- **Test_AllPages_MeetWCAGStandards**
  - Verify that all pages meet WCAG accessibility standards
- **Test_MediaPlayer_HasAccessibleControls**
  - Verify that media player controls are accessible

### 4. End-to-End Tests

#### User Journey Tests

- **Test_UserJourney_ScanQRCode_ViewContent_NavigateToRelated**
  - Simulate a user scanning a QR code, viewing content, and navigating to related content
- **Test_UserJourney_BrowseTimeline_ViewEvent_WatchVideo**
  - Simulate a user browsing the timeline, selecting an event, and watching a related video
- **Test_UserJourney_ViewBandMember_ExploreContributions**
  - Simulate a user viewing a band member's page and exploring their contributions

#### Performance Tests

- **Test_PageLoadTime_UnderAcceptableThreshold**
  - Verify that pages load within an acceptable time frame
- **Test_MediaPlayback_StartsWithinAcceptableTime**
  - Verify that media playback starts within an acceptable time frame

## Test Implementation Examples

### QR Code Service Test Example

```csharp
[Fact]
public void Test_GenerateQRCode_ValidUrl_ReturnsValidImage()
{
    // Arrange
    var qrCodeService = new QRCodeService();
    var url = "https://ragnarok-gasolin.dk/exhibit/1";

    // Act
    var qrCodeImage = qrCodeService.GenerateQRCode(url);

    // Assert
    Assert.NotNull(qrCodeImage);
    Assert.True(qrCodeImage.Length > 0);

    // Additional validation to ensure it's a valid image
    using (var ms = new MemoryStream(qrCodeImage))
    {
        var image = System.Drawing.Image.FromStream(ms);
        Assert.NotNull(image);
    }
}
```

### Band Member Page Test Example

```csharp
[Fact]
public async Task Test_BandMemberPage_LoadsCorrectly_DisplaysMemberInfo()
{
    // Arrange
    var client = _factory.CreateClient();

    // Act
    var response = await client.GetAsync("/BandMembers/KimLarsen");
    response.EnsureSuccessStatusCode();
    var content = await response.Content.ReadAsStringAsync();

    // Assert
    Assert.Contains("Kim Larsen", content);
    Assert.Contains("Vocalist", content);
    Assert.Contains("Biography", content);

    // Check for related content sections
    Assert.Contains("Contributions", content);
    Assert.Contains("Related Media", content);
}
```

### Timeline Responsive Design Test Example

```csharp
[Theory]
[InlineData(480)]  // Mobile
[InlineData(768)]  // Tablet
[InlineData(1200)] // Desktop
public async Task Test_TimelinePage_ResponsiveDesign_MobileTabletDesktop(int screenWidth)
{
    // Arrange
    var options = new BrowserOptions
    {
        ViewportSize = new ViewportSize
        {
            Width = screenWidth,
            Height = 800
        }
    };

    await using var browser = await Playwright.Firefox.LaunchAsync();
    var page = await browser.NewPageAsync(options);

    // Act
    await page.GotoAsync("https://ragnarok-gasolin.dk/Timeline");

    // Assert
    var timelineElement = await page.QuerySelectorAsync(".timeline-container");
    Assert.NotNull(timelineElement);

    // Check that timeline events are visible
    var events = await page.QuerySelectorAllAsync(".timeline-event");
    Assert.True(events.Count > 0);

    // Take a screenshot for visual verification
    await page.ScreenshotAsync(new PageScreenshotOptions
    {
        Path = $"timeline-responsive-{screenWidth}.png"
    });
}
```

## Test Data

We will create mock data for testing purposes, including:

1. **Band Members**

   - Kim Larsen (Vocalist, Guitarist)
   - Franz Beckerlee (Guitarist)
   - Wili Jønsson (Bassist)
   - Søren Berlev (Drummer)

2. **Timeline Events**

   - Band formation (1969)
   - Album releases (1971-1978)
   - Notable concerts
   - Band dissolution (1978)

3. **Exhibition Items**

   - Instruments
   - Album covers
   - Memorabilia
   - Historical photos

4. **Media Items**
   - Concert videos
   - Interviews
   - Music recordings
   - Documentary clips

## Continuous Integration

Tests will be integrated into a CI/CD pipeline to ensure that all tests are run automatically when code is pushed to the repository. This will help catch issues early in the development process.

## Test Coverage Goals

- **Unit Tests**: 90% code coverage
- **Integration Tests**: All major page flows and API endpoints
- **UI Tests**: All pages and components in multiple viewport sizes
- **End-to-End Tests**: All key user journeys

## Conclusion

This test plan provides a comprehensive approach to testing the Gasolin exhibition website, following the "Testing First" methodology. By implementing these tests before development begins, we ensure that the application meets all requirements and provides a robust, user-friendly experience for festival attendees and online visitors.
