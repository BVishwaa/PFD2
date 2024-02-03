"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

// Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    console.log("Received" + message)
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    console.log("found")
    li.textContent = `${user} says ${message}`;
    console.log("append")
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    console.log("button click")
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    console.log("sent:"+message)
    connection.invoke("SendMessage", user, message).catch(function (err) {
        console.log("fail")
        return console.error(err.toString());
    });

    console.log("invoked")
    event.preventDefault();
});
