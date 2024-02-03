document.addEventListener('DOMContentLoaded', function () {
    var phrases = ["Use me to translate!", "Discover new languages!", "Let's explore together!"];
    var currentIndex = 0;
    var textContainer = document.querySelector('.gif-text');

    setInterval(function () {
        currentIndex = (currentIndex + 1) % phrases.length; // Cycle through the array
        textContainer.textContent = phrases[currentIndex]; // Update the text
    }, 8000); // Change text every 8 seconds
});