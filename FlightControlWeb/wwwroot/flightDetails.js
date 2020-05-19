function showFlightDetails(flightId) {
    document.getElementById("details").style.visibility = "visible";
    $.ajax({
        type: "GET",
        url: "api/FlightPlan/" + flightId,
        dataType: 'json',
        data: {
        }, success: function (flightPlan) {
            segments = flightPlan.segments
            drowSegLines(segments);
            var init = flightPlan.initial_location;
            var lastSegment = segments[segments.length - 1];
            var endTime = getEndTime(segments, init.date_time);
            $("#details").append("<tr><td>" + flightPlan.company_name + "</td>" + "<td>" +
                flightPlan.passengers + "</td>" + "<td>(" + init.latitude + "," +
                init.longitude + ")</td>" + "<td>(" + lastSegment.latitude + "," +
                lastSegment.longitude + ")</td>" + "<td>" + init.date_time + "</td></tr>");

        }
    });
}

function cleanAndHideDataTable() {
    var detaildTable = document.getElementById("details");
    detaildTable.style.visibility = "hidden";
    detaildTable.deleteRow(1);
}

function getEndTime(segments, initTime) {
    var count = 0;
    segments.forEach(function (segment) {
        count += segment.timespan_seconds;
    });
    console.log(count);
    console.log(initTime)
    var date = new Date(initTime);
    console.log(date);

    var d2 = new Date(date);
    //d2.setSeconds(date.getSecondss() + count);
    console.log(d2);
}

