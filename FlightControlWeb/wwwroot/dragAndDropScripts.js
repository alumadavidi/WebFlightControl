function allowDrop(event) {
    event.preventDefault();
    let img = document.getElementById("draganddropimage");
    if (window.getComputedStyle(img).visibility === "hidden") {
        img.style.visibility = "visible";
    }
    $("#flightlist").hide();
    $("#draganddropimage").show();
}

function dropInTarget(event) {
    event.preventDefault();
    $("#draganddropimage").hide();
    $("#flightlist").show();
    let file = event.dataTransfer.files[0];
    checkFileValidness(file);
    let xhr = new XMLHttpRequest();
    let flightUrl = "api/FlightPlan"
    xhr.open("POST", flightUrl, true);
    xhr.setRequestHeader("Content-Type", "application/json"); 
    xhr.send(file);
    xhr.onload = function () {
        if (xhr.status == 400 || xhr.status == 500) {
            sendAlert("something wrong with the file, check it");

        }
    };
}
function drop(event) {
    event.preventDefault();
    $("#draganddropimage").hide();
    $("#flightlist").show();
}





