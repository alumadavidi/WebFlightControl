function sendAlert(message) {
    document.getElementById("alert-message").innerHTML = message;
    $("#alert-message").show();
    
    setTimeout(function () {
        $("#alert-message").hide();
    }, 5000);
}

function checkFileValidness(file) {
    if (file.type != "application/json") {
        sendAlert("Oops..the file is not valid, try to drag a json file");
    }
}