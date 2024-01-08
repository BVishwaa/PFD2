
AOS.init({
  duration: 1000,
  
});

function googleTranslateElementInit(){
    new google.translate.TranslateElement(
        "google_translate_element"
    );
}

// Store
//localStorage.setItem("language", "google_translate_element");

// Retrieve
//document.getElementById("google_translate_element").innerHTML = localStorage.getItem("language");

document.getElementById("languageBtn").addEventListener("click", displayDate);

function displayDate() {
  document.getElementById("google_translate_element").innerHTML;
}