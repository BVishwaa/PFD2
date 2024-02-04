"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
var currentUser = 'user'; 
var rID = '';


document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    document.getElementById("message").hidden = true;
    document.getElementById("sendButton").disabled = false;
    var messagesList = document.getElementById("messagesList");
    var li = document.createElement("li");
    var messageText = document.createElement("span"); 

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
    rID = Math.random().toString(36) + (new Date()).getTime().toString(36);
    console.log(rID);
    console.log(currentUser);
    connection.invoke("CreateRoom", rID, currentUser).catch(function (err) {
        return console.error(err.toString());
    });
    console.log("created")  
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = document.getElementById("messageInput").value.trim();

    if (message) {
        connection.invoke("SendMessage", currentUser, message, rID).catch(function (err) {
            return console.error(err.toString());
        });
        document.getElementById("messageInput").value = ''; // Clear the input after sending
    }
    event.preventDefault();
});


function leaveChat() {
    console.log("left");
    connection.invoke("LeaveChat", currentUser).catch(function (err) {
        console.error(err.toString());
    });   
    window.location.href = "/";
}