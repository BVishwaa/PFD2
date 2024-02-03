"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
var currentUser = '';

document.getElementById("sendButton").disabled = true;
document.getElementById("messagesList").hidden = true;

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


connection.on("AvailableRooms", function (rooms) {
    console.log("AvailableRooms received: ", rooms);
    var roomList = document.getElementById("roomList");

    // Clear the existing room list
    roomList.innerHTML = "";

    // Check if rooms is an array
    if (Array.isArray(rooms)) {
        rooms.forEach(function (room) {
            console.log("Appending room: ", room);
            var roomItem = document.createElement("div");
            roomItem.textContent = room.roomName; // Assuming RoomName is the property to display
            roomItem.classList.add("room-item");

            // Add a click event to join the room
            roomItem.addEventListener("click", function () {
                console.log("Clicked on room: ", room.roomName);
                joinRoom(room.roomName);
            });

            roomList.appendChild(roomItem);
            console.log("Appended room: ", room.roomName);
        });
    } else {
        console.log("Rooms is not an array: ", rooms);
    }
});

connection.start().then(function () {
    // When the connection is established, request the list of available rooms
    console.log("Connection established. Requesting available rooms...");
    connection.invoke("GetAvailableRooms").catch(function (err) {
        console.error("Error requesting available rooms:", err.toString());
    });

    // Join the "Admins" group
    console.log("Joining the 'Admins' group...");
    connection.invoke("AdminJoin", "Admins").catch(function (err) {
        console.error("Error joining the 'Admins' group:", err.toString());
    });
}).catch(function (err) {
    console.error("Error starting the connection:", err.toString());
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

function joinRoom(roomName) {
    connection.invoke("AdminJoinRoom", roomName).catch(function (err) {
        document.getElementById("sendButton").disabled = false;
        document.getElementById("messagesList").hidden = false;
        console.log('Joined');
        return console.error(err.toString());
    });
}