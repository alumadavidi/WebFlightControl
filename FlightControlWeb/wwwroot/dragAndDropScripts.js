//when user drag a file over the drag and drop area
function allowDrop(event) {
    event.preventDefault();
    let img = document.getElementById("draganddropimage");
    if (window.getComputedStyle(img).visibility === "hidden") {
        img.style.visibility = "visible";
    }
    $("#flightlist").hide();
    $("#draganddropimage").show();
}

//when the user drop the file into the drag and drop area
function dropInTarget(event) {
    event.preventDefault();
    $("#draganddropimage").hide();
    $("#flightlist").show();
    let file = event.dataTransfer.files[0];
    if (checkFileValidness(file) == true) {
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
}
//when the user drop the file not in the drag and drop area
function drop(event) {
    event.preventDefault();
    $("#draganddropimage").hide();
    $("#flightlist").show();
}





