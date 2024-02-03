"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
var currentUser = ''; 


// Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var messagesList = document.getElementById("messagesList");
    var li = document.createElement("li");
    var messageText = document.createElement("span"); // Create a span for the message text

    // Check if the message is from the current user
    if (user === currentUser) {
        li.className = "user-message";
        messageText.textContent = `You: ${message}`;
    } else {
        li.className = "other-message";
        messageText.textContent = `Admin: ${message}`;
    }

    li.appendChild(messageText);
    messagesList.appendChild(li);
});

connection.start().then(function () {
    connection.invoke("CreateRoom").catch(function (err) {
        return console.error(err.toString());
    });
    console.log("created")  
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value.trim();
    currentUser = user; // Set the current user
    var message = document.getElementById("messageInput").value.trim();

    if (message) { // Only send if there's a message
        connection.invoke("SendMessage", user, message).catch(function (err) {
            return console.error(err.toString());
        });
        document.getElementById("messageInput").value = ''; // Clear the input after sending
    }
    event.preventDefault();
});
