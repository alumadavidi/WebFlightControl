var flightListInTable = [];

function updateTable() {
    var flightsFromServer = [];
    var flightsUrl = "api/Flights";
    $.ajax({
        type: "GET",
        url: flightsUrl,
        dataType: 'json',
        data: {
            relative_to: "2020-12-26T23:56:21Z5"
        }, success: function (data) {
            data.forEach(function (flight) {
                flightsFromServer.push(flight.flight_Id);
                if (flightListInTable.indexOf(flight.flight_Id) < 0) {
                    addFlightToView(flight);
                } else {
                    var latitude = flight.latitude;
                    var longitude = flight.longitude;
                    updateMrkerLatlng(latitude, longitude, flight.flight_Id);
                    
                }
            });
            flightListInTable.forEach(function (flightId) {
                if (!flightsFromServer.includes(flightId)) {
                    delFlightFromView(flightId);
                }
            });
            flightsFromServer = [];
        }
    });
}
var setTimer = setInterval(updateTable, 1000);

function addFlightToView(flight) {
    var table
    if (flight.is_external) {
        $("#exflightstbl").append("<tr id=" + '"' + flight.flight_Id + '"' + 'onclick="rowClick(' + flight.flight_Id + ')"' + "><td>" + flight.flight_Id + "</td>" + "<td>" +
            flight.company_name + "</td></tr>");
    } else {
        $("#myflightstbl").append("<tr id=" + '"' + flight.flight_Id + '"' + 'onclick="rowClick(' + flight.flight_Id + ')"' + "><td>" + flight.flight_Id + "</td>" + "<td>" +
            flight.company_name + "</td>" + "<td>" +
            '<button id="delButton" onclick="delFlight(' + flight.flight_Id + ')"><i class="fa fa-trash"></i></button>'
            + "</td></tr>");
    }
    var latitude = flight.latitude;
    var longitude = flight.longitude;
    addIconToMap(latitude, longitude, flight.flight_Id);
    flightListInTable.push(flight.flight_Id);
}

function delFlightFromList(flightId) {
    flightListInTable.splice(flightListInTable.indexOf(flightId), 1);
    var element = document.getElementById(flightId);
    element.parentNode.removeChild(element);
}

function highlightElements(flight) {
    var row = document.getElementById(flight)
    row.style.border = "medium solid #000";
}
function unHighlightElements(flight) {
    var row = document.getElementById(flight)
    row.style.border = "none";
}

function delFlight(flight) {
    $.ajax({
        type: "DELETE",
        url: "api/Flights/" + flight.id,
        dataType: 'json',
        success: function (data) {
            delFlightFromView(flight.id);
        }
    });
}
function delFlightFromView(flightId) {
    deleteMarker(flightId);
    delFlightFromList(flightId);
}

