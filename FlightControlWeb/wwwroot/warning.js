//send alert to user with message
function sendAlert(message) {
    document.getElementById("alert-message").innerHTML = message;
    $("#alert-message").show();
    //timer to show the alert
    setTimeout(function () {
        $("#alert-message").hide();
    }, 5000);
}
//check if file is a json flie
function checkFileValidness(file) {
    if (file.type != "application/json") {
        sendAlert("Oops..the file is not valid, try to drag a json file");
    }
}