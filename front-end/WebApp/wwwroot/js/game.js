"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/gameHub").build();


// We can assign user-supplied strings to an element's textContent because it
// is not interpreted as markup. If you're assigning in any other way, you 
// should be aware of possible script injection concerns.
//connection.on("TurnAnswer", function (message) {
//    var x = document.getElementById("1");
//    console.log(x);
//    x.style.backgroundColor = "red";
//    var s = x.getElementById("answer-points");
//    s.value = message;
//});

