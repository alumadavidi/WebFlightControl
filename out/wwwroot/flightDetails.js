//the functio get flight details from server and update it in view
function showFlightDetails(flightId, marker) {
    document.getElementById("details").style.visibility = "visible";
    $.ajax({
        type: "GET",
        url: "api/FlightPlan/" + flightId,
        dataType: 'json',
        data: {
        }, success: function (flightPlan) {
            //if user didnt clicked meanwhile on other marker
            if (getClickedMarker() == marker) {
                let segments = flightPlan.segments
                let init = flightPlan.initial_location;
                drowSegLines(init, segments);
                let lastSegment = segments[segments.length - 1];
                let endTime = getEndTime(segments, init.date_time);
                let stringEndTime = endTime.getFullYear() + "-"
                    + (endTime.getMonth() + 1) + "-"
                    + endTime.getDate() + "T"
                    + endTime.getHours() + ":"
                    + endTime.getMinutes() + ":"
                    + endTime.getSeconds() + "Z";
                $("#details").append("<tr><td>" + flightPlan.company_name + "</td>" +
                    "<td>" + flightPlan.passengers + "</td>" + "<td>(" + init.latitude + "," +
                    init.longitude + ")</td>" + "<td>(" + lastSegment.latitude + "," +
                    lastSegment.longitude + ")</td>" + "<td>" + init.date_time + "</td>" +
                    "<td>" + stringEndTime + "</td></tr>");
            }
        }, error: function () {
            //on error send alert
            sendAlert("Oops..can't get flight " + flightId + " from server");
        }
    });
}

//the function delete all body from details table and hide it
function cleanAndHideDataTable() {
    let detaildTable = document.getElementById("details");
    detaildTable.style.visibility = "hidden";
    let rowCount = $('#details >tbody >tr').length;
    if (rowCount > 0) {
        detaildTable.deleteRow(1);
    }
}

//the function calculate the end time of flight
function getEndTime(segments, initTime) {
    let count = 0;
    segments.forEach(function (segment) {
        count += segment.timespan_seconds;
    });
    let dateInit = stringToDate(initTime);
    let endTime = new Date(dateInit.getTime() + 1000 * count);
    return endTime;
}

//convert string to date object
function stringToDate(dateStr) {
    let parts = dateStr.slice(0, -1).split('T');
    let date = parts[0].split('-');
    let time = parts[1].split(':');
    let timeObject = new Date(date[0], date[1] - 1, date[2], time[0], time[1], time[2]);
    return timeObject
}

