//all filghts that shown in the table
let flightListInTable = [];
//the function get data from server and update the table
function updateTable() {
    let currentdate = new Date();
    let curDate = currentdate.getFullYear() + "-"
        + (currentdate.getMonth() + 1) + "-"
        + currentdate.getDate() + "T"
        + currentdate.getHours() + ":"
        + currentdate.getMinutes() + ":"
        + currentdate.getSeconds() + "Z";
    //List of flights from server
    let flightsFromServer = [];
    let flightsUrl = "api/Flights";
    $.ajax({
        type: "GET",
        url: flightsUrl,
        dataType: 'json',
        data: {
            relative_to: curDate + "&sync_all"
        }, success: function (flights) {
            flights.forEach(function (flight) {
                //add flight to list from server
                flightsFromServer.push(flight.flight_Id);
                //if flight dont exist in table add it
                if (flightListInTable.indexOf(flight.flight_Id) < 0) {
                    addFlightToView(flight);
                } else { //update latlng of plane
                    let latitude = flight.latitude;
                    let longitude = flight.longitude;
                    updateMrkerLatlng(latitude, longitude, flight.flight_Id);
                }
            });
            //if there is flight in table that no loner exist in server - delete it
            flightListInTable.forEach(function (flightId) {
                if (!flightsFromServer.includes(flightId)) {
                    delFlightFromView(flightId);
                }
            });
            flightsFromServer = [];
        }, error: function () {
            sendAlert("Oops..can't get flights from server");
        }
    });
}
//timer to update table
let setTimer = setInterval(updateTable, 1000);

//the function add new flight from server to view any roe in dable has the same id as it's flight
function addFlightToView(flight) {
    //external flight - no delete button
    if (flight.is_external) {
        $("#exflightstbl").append("<tr id=" + '"' + flight.flight_Id + '"' +
            'onclick="rowClick(event, ' + flight.flight_Id + ')"' +
            "><td>" + flight.flight_Id + "</td>" + "<td>" +
            flight.company_name + "</td></tr>");
    } else { // my flights - delete button added
        $("#myflightstbl").append("<tr id=" + '"' + flight.flight_Id + '"' +
            'onclick="rowClick(event, ' + flight.flight_Id + ')"' +
            "><td>" + flight.flight_Id + "</td>" + "<td>" +
            flight.company_name + "</td>" + "<td>" +
            '<button id="delButton" onclick="delFlight(event,' +
            flight.flight_Id + ')"><i id="trash" class="fa fa-trash"></i></button>'
            + "</td></tr>");
    }
    //add icon to map at latlng of flight
    let latitude = flight.latitude;
    let longitude = flight.longitude;
    addIconToMap(latitude, longitude, flight.flight_Id);
    //add flight to table flights list
    flightListInTable.push(flight.flight_Id);
}
//delete flight from list of table flights and from table
function delFlightFromList(flightId) {
    flightListInTable.splice(flightListInTable.indexOf(flightId), 1);
    let element = document.getElementById(flightId);
    element.parentNode.removeChild(element);
}
//highlight elemet when marker or row clicked
function highlightElements(flight) {
    let row = document.getElementById(flight)
    row.style.border = "medium solid #000";
}
//return to the original form
function unHighlightElements(flight) {
    let row = document.getElementById(flight)
    row.style.border = "none";
}
//send request to delete fight feom server
function delFlight(event, flight) {
    $.ajax({
        type: "DELETE",
        url: "api/Flights/" + flight.id,
        dataType: 'json',
        success: function () {
            delFlightFromView(flight.id);
            event.cancelBubble = true;
        },
        complete: function (xhr) {
            //on error send alert
            if (xhr.status == 404) {
                sendAlert("Oops..this flight don't exist, can't be deleted");
            }
        }
    });
}
//delete row and marker of flight from view
function delFlightFromView(flightId) {
    deleteMarker(flightId);
    delFlightFromList(flightId);
}

