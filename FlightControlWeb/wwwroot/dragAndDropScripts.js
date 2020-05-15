function allowDrop(event) {
    event.preventDefault();
    var img = document.getElementById("draganddropimage");
    if (window.getComputedStyle(img).visibility === "hidden") {
        img.style.visibility = "visible";
    }
    $("#flightlist").hide();
    $("#draganddropimage").show();
}

function dragLeave(event) {
    event.preventDefault();
    $("#draganddropimage").hide();
    $("#flightlist").show();
}

function drop(event) {
    event.preventDefault();
    $("#draganddropimage").hide();
    $("#flightlist").show();
    console.log(event.dataTransfer.files);
    //if (event.dataTransfer.items[0] == 'file') {
        var file = event.dataTransfer.files[0];
        var xhr = new XMLHttpRequest();
        var flightUrl = "api/FlightPlan"
        xhr.open("POST", flightUrl, true);
        xhr.setRequestHeader("Content-Type", "application/json");
        xhr.send(file);
    //}
}
function delFlight(flight) {
    $.ajax({
        type: "DELETE",
        url: "api/Flights/" + flight.id,
        dataType: 'json',
        success: function (data) {
        }
    });
    delFlightFromList(flight);
    deleteMarker(flight)
}

