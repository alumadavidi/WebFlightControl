
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
    $("#draganddropimage").hide();
    $("#flightlist").show();
}

function drop(event) {
    event.preventDefault();
    $("#draganddropimage").hide();
    console.log(event);
    var reader = new FileReader();
    reader.onloadend = function () {
        var data = JSON.parse(this.result);
        callback(data);
    };

    conreader.readAsText(event.dataTransfer.files[0]);
    $("#flightlist").show();

}

function deleteRowcc() {
    console.log("kk");
    var rowid = "1234567";
    var row = document.getElementById(rowid);
    var table = row.parentNode;
    while (table && table.tagName != 'TABLE')
        table = table.parentNode;
    if (!table)
        return;
    table.deleteRow(row.rowIndex);
}

function delFlight() {
    document.getElementById("demo").innerHTML = "Hello World";
    console.log("kk");
    var rowid = "1234567";
    var row = document.getElementById(rowid);
    var table = row.parentNode;
    while (table && table.tagName != 'TABLE') {
        console.log(table.tagName);
        table = table.parentNode;
    }
    if (!table)
        return;
    table.deleteRow(row.rowIndex);
    var flightsUrl = "api/Flights";

    $.ajax({
        type: "DELETE",
        url: "api/ApiWithActions/" + rowid,
        dataType: 'json',
         success: function (data) {
            $.ajax({
                type: "GET",
                url: flightsUrl,
                dataType: 'json',
                data: {
                    relative_to: "2020-12-26T23:56:21Z5"
                }, success: function (data) {
                    
                        console.log(data);
                    
                }
            });
        }
    });
}