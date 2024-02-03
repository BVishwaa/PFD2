let products = {
    data: [{
        productName: "POSB",
        category: "Banking",
        image: "Images/For-Logos/POSB_DigiBank Logo.png"
    },
    {
        productName: "OCBC",
        category: "Banking",
        image: "Images/For-Logos/OCBCDigitalLogo.png"
    },
    {
        productName: "DBS Paylah",
        category: "Banking",
        image: "Images/For-Logos/DBSPaylah.png"
    },
    {
        productName: "Health Hub",
        category: "Health",
        image: "Images/For-Logos/Health Hub Logo.png"
    },
    {
        productName: "Healthy 365",
        category: "Health",
        image: "Images/For-Logos/Healthy 365 Logo.jpg"
    },
    {
        productName: "Health Buddy",
        category: "Health",
        image: "Images/For-Logos/Health Buddy.png"
    },
    {
        productName: "Whatsapp",
        category: "Messaging",
        image: "Images/For-Logos/Whatsapp Logo.png"
    },
    {
        productName: "WeChat",
        category: "Messaging",
        image: "Images/For-Logos/WeChat Symbol.png"
    },
    {
        productName: "Telegram",
        category: "Messaging",
        image: "Images/For-Logos/Telegram Logo.png"
    },
    {
        productName: "Deliveroo",
        category: "Food",
        image: "Images/For-Logos/Deliveroo.jpg"
    },
    {
        productName: "SG Buses",
        category: "Transport",
        image: "Images/For-Logos/SGBuses Logo.png"
    },

    ],
};

function filterProduct(value) {
    let buttons = document.querySelectorAll(".button_value");
    buttons.forEach((button) => {
        if (value.toUpperCase() == button.innerText.toUpperCase()) {
            button.classList.add("active");
        }
        else {
            button.classList.remove("active");
        }
    });

    let elements = document.querySelectorAll(".card");
    elements.forEach((element) => {
        if (value == "all") {
            element.classList.remove("hide");
        } else {
            if (element.classList.contains(value)) {
                element.classList.remove("hide");
            } else {
                element.classList.add("hide");
            }
        }
    });

}



window.onload = () => {
    filterProduct("all");
};



