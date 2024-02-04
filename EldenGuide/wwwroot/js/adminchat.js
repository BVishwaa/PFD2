"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
var currentUser = "admin";
var currentRoom = "";


document.getElementById("sendButton").disabled = true;
document.getElementById("messagesList").hidden = true;

connection.on("ReceiveMessage", function (user, message) {
    var messagesList = document.getElementById("messagesList");
    var li = document.createElement("li");
    var messageText = document.createElement("span");

    if (user === currentUser) {
        li.className = "user-message";
        messageText.textContent = `You: ${message}`;
    } else {
        li.className = "other-message";
        messageText.textContent = `User: ${message}`;
    }

    li.appendChild(messageText);
    messagesList.appendChild(li);
});


connection.on("AvailableRooms", function (rooms) {
    console.log("AvailableRooms received: ", rooms);
    var roomList = document.getElementById("roomList");

    roomList.innerHTML = "";

    if (Array.isArray(rooms)) {
        rooms.forEach(function (room) {
            if (room.isAdminJoined == false) {
                console.log("Appending room: ", room);
                var roomItem = document.createElement("div");
                roomItem.textContent = room.roomName; 
                roomItem.classList.add("room-item");
                roomItem.addEventListener("click", function () {
                    console.log("Clicked on room: ", room.roomName);
                    joinRoom(room.roomName);
                    currentRoom = room.roomName;
                    var messagesList = document.getElementById("messagesList");
                    messagesList.innerHTML = "";
                });

                roomList.appendChild(roomItem);
                console.log("Appended room: ", room.roomName);
            }
        });
    } else {
        console.log("Rooms is not an array: ", rooms);
    }
});

connection.start().then(function () {
    
    console.log("Joining the 'Admins' group...");
    connection.invoke("AdminJoin", "Admins").catch(function (err) {
        console.error("Error joining the 'Admins' group:", err.toString());
    });
    connection.invoke("GetRooms").catch(function (err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    console.error("Error starting the connection:", err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = document.getElementById("messageInput").value.trim();
    if (message) { 
        connection.invoke("SendMessage", currentUser, message, currentRoom).catch(function (err) {
            return console.error(err.toString());
        });
        document.getElementById("messageInput").value = ''; // Clear the input after sending
    }
    event.preventDefault();
});

function joinRoom(roomName) {
    document.getElementById("sendButton").disabled = false;
    document.getElementById("messagesList").hidden = false;
    console.log('Joined');
    connection.invoke("AdminJoinRoom", roomName).catch(function (err) {
        return console.error(err.toString());
    });
} 