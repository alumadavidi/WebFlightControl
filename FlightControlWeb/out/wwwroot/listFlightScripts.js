let flightListInTable = [];
function updateTable() {
    let currentdate = new Date();
    let curDate = currentdate.getFullYear() + "-"
        + (currentdate.getMonth() + 1) + "-"
        + currentdate.getDate() + "T"
        + currentdate.getHours() + ":"
        + currentdate.getMinutes() + ":"
        + currentdate.getSeconds() + "Z";
    let flightsFromServer = [];
    let flightsUrl = "api/Flights";
    $.ajax({
        type: "GET",
        url: flightsUrl,
        dataType: 'json',
        data: {
            relative_to: curDate + "&sync_all"
        }, success: function (data) {
            data.forEach(function (flight) {
                flightsFromServer.push(flight.flight_Id);
                if (flightListInTable.indexOf(flight.flight_Id) < 0) {
                    addFlightToView(flight);
                } else {
                    let latitude = flight.latitude;
                    let longitude = flight.longitude;
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
let setTimer = setInterval(updateTable, 1000);

function addFlightToView(flight) {
    
    if (flight.is_external) {
        $("#exflightstbl").append("<tr id=" + '"' + flight.flight_Id + '"' + 'onclick="rowClick(event, '  + flight.flight_Id + ')"' + "><td>" + flight.flight_Id + "</td>" + "<td>" +
            flight.company_name + "</td></tr>");
    } else {
        $("#myflightstbl").append("<tr id=" + '"' + flight.flight_Id + '"' + 'onclick="rowClick(event, ' + flight.flight_Id + ')"' + "><td>" + flight.flight_Id + "</td>" + "<td>" +
            flight.company_name + "</td>" + "<td>" +
            '<button id="delButton" onclick="delFlight(event,' + flight.flight_Id + ')"><i id="trash" class="fa fa-trash"></i></button>'
            + "</td></tr>");
    }
    let latitude = flight.latitude;
    let longitude = flight.longitude;
    addIconToMap(latitude, longitude, flight.flight_Id);
    flightListInTable.push(flight.flight_Id);
}

function delFlightFromList(flightId) {
    flightListInTable.splice(flightListInTable.indexOf(flightId), 1);
    let element = document.getElementById(flightId);
    element.parentNode.removeChild(element);
}

function highlightElements(flight) {
    let row = document.getElementById(flight)
    row.style.border = "medium solid #000";
}
function unHighlightElements(flight) {
    let row = document.getElementById(flight)
    row.style.border = "none";
}

function delFlight(event, flight) {
    $.ajax({
        type: "DELETE",
        url: "api/Flights/" + flight.id,
        dataType: 'json',
        success: function (data) {
            delFlightFromView(flight.id);
            event.cancelBubble = true;
        }, 
        complete: function (xhr) {
            if (xhr.status == 404) {
                sendAlert("Oops..this flight don't exist, can't be deleted");
            }
        } 
    });
}
function delFlightFromView(flightId) {
    deleteMarker(flightId);
    delFlightFromList(flightId);
}

