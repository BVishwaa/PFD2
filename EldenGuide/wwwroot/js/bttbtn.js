// Wait for the HTML document to be fully loaded and parsed
document.addEventListener("DOMContentLoaded", function() {
    let mybutton = document.getElementById("bttBtn"); // Get a reference to the button with the ID "bttBtn"

    // Initially hide the button
    mybutton.style.display = "none";

    // Attach a function to the window's scroll event
    window.onscroll = function() {
        // Call the scrollFunction when the user scrolls
        scrollFunction();
    };

    // Define the scrollFunction
    function scrollFunction() {
        // Check if the user has scrolled more than 20 pixels vertically
        if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
            mybutton.style.display = "block";
        } else {
            // If not scrolled (or scrolled back to the top), hide the button
            mybutton.style.display = "none";
        }
    }

    // When the user clicks on the button, scroll to the top of the document
    mybutton.addEventListener("click", function () {
        topFunction();
    });
    function topFunction() {
        document.body.scrollTop = 0;
        document.documentElement.scrollTop = 0;
    }
});
