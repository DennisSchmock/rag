/**
 * Timeline.js - JavaScript for the Gasolin Timeline
 *
 * This file contains the JavaScript functionality for the interactive timeline
 * on the Gasolin exhibition website.
 */

// Initialize timeline when the DOM is fully loaded
document.addEventListener("DOMContentLoaded", function () {
  initializeTimeline();
});

/**
 * Initialize the timeline functionality
 */
function initializeTimeline() {
  // Add animation to timeline events when they come into view
  animateTimelineEvents();

  // Highlight the current year in the navigation
  highlightCurrentYearInNavigation();
}

/**
 * Add animation to timeline events when they come into view
 */
function animateTimelineEvents() {
  // Get all timeline events
  const timelineEvents = document.querySelectorAll(".timeline-event-content");

  // Create an Intersection Observer
  const observer = new IntersectionObserver(
    (entries) => {
      entries.forEach((entry) => {
        // Add animation class when the event comes into view
        if (entry.isIntersecting) {
          entry.target.classList.add("animate__animated");

          // Add different animation based on whether it's on the left or right
          if (entry.target.classList.contains("left")) {
            entry.target.classList.add("animate__fadeInLeft");
          } else {
            entry.target.classList.add("animate__fadeInRight");
          }

          // Stop observing the event after it's been animated
          observer.unobserve(entry.target);
        }
      });
    },
    {
      threshold: 0.2, // Trigger when 20% of the element is visible
    }
  );

  // Observe each timeline event
  timelineEvents.forEach((event) => {
    observer.observe(event);
  });
}

/**
 * Highlight the current year in the navigation based on scroll position
 */
function highlightCurrentYearInNavigation() {
  // Get all year sections
  const yearSections = document.querySelectorAll(".timeline-year");
  const yearNavButtons = document.querySelectorAll(".year-nav-btn");

  // Create an Intersection Observer
  const observer = new IntersectionObserver(
    (entries) => {
      entries.forEach((entry) => {
        // If the year section is in view
        if (entry.isIntersecting) {
          // Get the year ID
          const yearId = entry.target.id;

          // Remove active class from all navigation buttons
          yearNavButtons.forEach((btn) => {
            btn.classList.remove("active", "btn-secondary");
            btn.classList.add("btn-outline-secondary");
          });

          // Add active class to the current year's navigation button
          const activeButton = document.querySelector(
            `.year-nav-btn[href="#${yearId}"]`
          );
          if (activeButton) {
            activeButton.classList.remove("btn-outline-secondary");
            activeButton.classList.add("active", "btn-secondary");
          }
        }
      });
    },
    {
      threshold: 0.5, // Trigger when 50% of the element is visible
    }
  );

  // Observe each year section
  yearSections.forEach((section) => {
    observer.observe(section);
  });
}

/**
 * Filter timeline events by type
 *
 * @param {string} type - The event type to filter by, or 'all' for all events
 */
function filterTimelineEvents(type) {
  const timelineEvents = document.querySelectorAll(".timeline-event");

  timelineEvents.forEach((event) => {
    if (type === "all" || event.getAttribute("data-event-type") === type) {
      event.style.display = "block";
    } else {
      event.style.display = "none";
    }
  });
}

/**
 * Open media in a modal
 *
 * @param {string} url - The URL of the media file
 * @param {string} type - The type of media (video, audio)
 * @param {string} title - The title of the media
 */
function openMediaModal(url, type, title) {
  // Set the modal title
  document.getElementById("mediaModalLabel").textContent = title;

  // Create the appropriate media element
  let mediaHtml = "";
  if (type === "video") {
    mediaHtml = `<video class="w-100" controls autoplay><source src="${url}" type="video/mp4">Your browser does not support the video tag.</video>`;
  } else if (type === "audio") {
    mediaHtml = `<audio class="w-100" controls autoplay><source src="${url}" type="audio/mpeg">Your browser does not support the audio tag.</audio>`;
  }

  // Set the media container content
  document.getElementById("mediaContainer").innerHTML = mediaHtml;

  // Show the modal
  $("#mediaModal").modal("show");
}

/**
 * Scroll to a specific year in the timeline
 *
 * @param {string} yearId - The ID of the year element to scroll to
 */
function scrollToYear(yearId) {
  const yearElement = document.getElementById(yearId);

  if (yearElement) {
    // Scroll to the year element with a smooth animation
    window.scrollTo({
      top: yearElement.offsetTop - 100,
      behavior: "smooth",
    });
  }
}
