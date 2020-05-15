var list = []
function updateTable() {
    var flightsUrl = "api/Flights";
    $.ajax({
        type: "GET",
        url: flightsUrl,
        dataType: 'json',
        data: {
            relative_to: "2020-12-26T23:56:21Z5"
        }, success: function (data) {

            data.forEach(function (flight) {
                if (list.indexOf(flight.flight_Id) < 0) {
                    $("#myflightstbl").append("<tr id=" + flight.flight_Id + "><td>" + flight.flight_Id + "</td>" + "<td>" +
                        flight.company_name + "</td>" + "<td>" +
                        '<button onclick="delFlight(' + flight.flight_Id + ')">X</button>'
                        + "</td></tr>");

                    var latitude = flight.latitude;
                    var longitude = flight.longitude;
                    addIconToMap(latitude, longitude, flight);
                    list.push(flight.flight_Id);
                }

            });
        }
    });
    console.log("table updated");
}
var setTimer = setInterval(updateTable, 1000);

function delFlightFromList(flight) {
    console.log(flight.id);
    list.splice(list.indexOf(flight.id), 1);
    var element = document.getElementById(flight.id);
    element.parentNode.removeChild(element);
}

